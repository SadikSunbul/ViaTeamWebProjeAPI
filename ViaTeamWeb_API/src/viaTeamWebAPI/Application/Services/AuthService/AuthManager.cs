using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly TokenOptions _tokenOptions;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public AuthManager(
        IUserOperationClaimRepository userOperationClaimRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenHelper tokenHelper,
        IConfiguration configuration,
        AuthBusinessRules authBusinessRules
    )
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;
        _authBusinessRules = authBusinessRules;

        const string tokenOptionsConfigurationSection = "TokenOptions";
        _tokenOptions =
            configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
            ?? throw new NullReferenceException(
                $"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration");
    }

    // Kullanıcının yetkilerine dayanarak bir erişim token'ı oluşturur
    public async Task<AccessToken> CreateAccessToken(User user)
    {
        IList<OperationClaim> operationClaims =
            await _userOperationClaimRepository.GetOperationClaimsByUserIdAsync(user.Id);
        AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
        return accessToken;
    }

    // Bir yenileme token'ını ekler
    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    // Belirli bir kullanıcının eski yenileme token'larını siler
    public async Task DeleteOldRefreshTokens(int userId)
    {
        List<RefreshToken> refreshTokens =
            await _refreshTokenRepository.GetOldRefreshTokensAsync(userId, _tokenOptions.RefreshTokenTTL);
        await _refreshTokenRepository.DeleteRangeAsync(refreshTokens);
    }

    // Belirli bir yenileme token'ını token değerine göre alır
    public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetAsync(predicate: r => r.Token == token);
        return refreshToken;
    }

    // Bir yenileme token'ını iptal eder (revoke)
    public async Task RevokeRefreshToken(RefreshToken refreshToken, string ipAddress, string? reason = null,
        string? replacedByToken = null)
    {
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReasonRevoked = reason;
        refreshToken.ReplacedByToken = replacedByToken;
        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    // Bir kullanıcının yenileme token'ını değiştirir
    public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
    {
        RefreshToken newRefreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        await RevokeRefreshToken(refreshToken, ipAddress, reason: "Replaced by new token",
            newRefreshToken.Token);
        return newRefreshToken;
    }

    // Bir yenileme token'ının çocuklarını iptal eder (revoke)
    public async Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress,
        string reason)
    {
        RefreshToken? childToken =
            await _refreshTokenRepository.GetAsync(predicate: r => r.Token == refreshToken.ReplacedByToken);

        if (childToken?.Revoked != null && childToken.Expires <= DateTime.UtcNow)
            await RevokeRefreshToken(childToken, ipAddress, reason);
        else
            await RevokeDescendantRefreshTokens(refreshToken: childToken!, ipAddress, reason);
    }

    // Bir kullanıcının yeni bir yenileme token'ı oluşturur
    public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        return Task.FromResult(refreshToken);
    }
}