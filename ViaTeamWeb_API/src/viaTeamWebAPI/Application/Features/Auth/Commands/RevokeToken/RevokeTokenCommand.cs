using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using AutoMapper;
using MediatR;

namespace Application.Features.Auth.Commands.RevokeToken;

public class RevokeTokenCommand : IRequest<RevokedTokenResponse>
{
    public string Token { get; set; }
    public string IpAddress { get; set; }

    public RevokeTokenCommand()
    {
        Token = string.Empty;
        IpAddress = string.Empty;
    }

    public RevokeTokenCommand(string token, string ipAddress)
    {
        Token = token;
        IpAddress = ipAddress;
    }

    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, RevokedTokenResponse>
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMapper _mapper;

        public RevokeTokenCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules,
            IMapper mapper)
        {
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mapper = mapper;
        }

        public async Task<RevokedTokenResponse> Handle(RevokeTokenCommand request,
            CancellationToken cancellationToken)
        {
            // Verilen Token değeri ile ilgili Refresh Token bilgilerini getir.
            Core.Security.Entities.RefreshToken? refreshToken =
                await _authService.GetRefreshTokenByToken(request.Token);
            await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);
            // Refresh Token'ın aktif olup olmadığı kontrol edilir.
            await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken!);
            // Refresh Token'ın iptal edilmesi için ilgili servis metodu çağrılır.
            await _authService.RevokeRefreshToken(token: refreshToken!, request.IpAddress,
                reason: "Revoked without replacement");
            // İptal edilen Refresh Token'a ait bilgileri içeren bir yanıt nesnesi oluşturulur.
            RevokedTokenResponse revokedTokenResponse = _mapper.Map<RevokedTokenResponse>(refreshToken);
            return revokedTokenResponse;
        }
    }
}