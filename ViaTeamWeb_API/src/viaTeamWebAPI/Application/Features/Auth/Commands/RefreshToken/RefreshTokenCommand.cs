using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<RefreshedTokensResponse>
{
    public string RefreshToken { get; set; }
    public string IpAddress { get; set; }

    public RefreshTokenCommand()
    {
        RefreshToken = string.Empty;
        IpAddress = string.Empty;
    }

    public RefreshTokenCommand(string refreshToken, string ipAddress)
    {
        RefreshToken = refreshToken;
        IpAddress = ipAddress;
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshedTokensResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly AuthBusinessRules _authBusinessRules;

        public RefreshTokenCommandHandler(IAuthService authService, IUserService userService,
            AuthBusinessRules authBusinessRules)
        {
            _authService = authService;
            _userService = userService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RefreshedTokensResponse> Handle(RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            // Verilen Refresh Token değeri ile ilgili Refresh Token bilgilerini getir.
            Core.Security.Entities.RefreshToken? refreshToken =
                await _authService.GetRefreshTokenByToken(request.RefreshToken);
            // Eğer Refresh Token bulunamazsa veya null ise, iş kuralı hatası fırlatılır.
            await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);

            // Eğer Refresh Token'ın revoke (iptal) tarihi belirlenmişse, bu token'ın kullanılamaz olduğu kontrol edilir.
            if (refreshToken!.Revoked != null)
                // Eğer token kullanılamazsa, aynı ailenin bir alt token'ının da iptal edilmesi gerekmektedir.
                await _authService.RevokeDescendantRefreshTokens(
                    refreshToken,
                    request.IpAddress,
                    reason: $"Attempted reuse of revoked ancestor token: {refreshToken.Token}"
                );
            // Refresh Token'ın aktif olup olmadığı kontrol edilir.
            await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

            // Refresh Token'a bağlı kullanıcının bilgileri getirilir.
            User? user = await _userService.GetAsync(predicate: u => u.Id == refreshToken.UserId,
                cancellationToken: cancellationToken);
            // Eğer kullanıcı bulunamazsa veya null ise, iş kuralı hatası fırlatılır.
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);

            // Yeni bir Refresh Token oluşturulur ve eski Refresh Token yerine kullanılır.
            Core.Security.Entities.RefreshToken newRefreshToken = await _authService.RotateRefreshToken(
                user: user!,
                refreshToken,
                request.IpAddress
            );

            // Yeni Refresh Token veritabanına eklenir.
            Core.Security.Entities.RefreshToken addedRefreshToken =
                await _authService.AddRefreshToken(newRefreshToken);
            // Kullanıcının yeni Access Token'ı oluşturulur.
            await _authService.DeleteOldRefreshTokens(refreshToken.UserId);
            // Kullanıcının yeni Access Token'ı oluşturulur.
            AccessToken createdAccessToken = await _authService.CreateAccessToken(user!);

            // Yenilenmiş Tokenlara ait bir yanıt nesnesi oluşturulur.
            RefreshedTokensResponse refreshedTokensResponse = new()
            {
                AccessToken = createdAccessToken, RefreshToken = addedRefreshToken
            };
            return refreshedTokensResponse;
        }
    }
}