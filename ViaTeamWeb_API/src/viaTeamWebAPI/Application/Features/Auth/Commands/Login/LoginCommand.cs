using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public LoginCommand()
    {
        UserForLoginDto = null!;
        IpAddress = string.Empty;
    }

    public LoginCommand(UserForLoginDto userForLoginDto, string ipAddress)
    {
        UserForLoginDto = userForLoginDto;
        IpAddress = ipAddress;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public LoginCommandHandler(
            IUserService userService,
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(
                predicate: u => u.Email == request.UserForLoginDto.Email,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user); // Kullanıcının varlığını kontrol
            await _authBusinessRules.UserPasswordShouldBeMatch(user!.Id,
                request.UserForLoginDto.Password); // Kullanıcının şifresini kontrol et 

            LoggedResponse loggedResponse = new();

            // Eğer kullanıcının authenticator tipi None değilse kullanıcı bir authenticator kullanıyorsa gir
            if (user.AuthenticatorType is not AuthenticatorType.None) 
            {
                // Eğer isteğin içinde authenticator kodu yoksa
                if (request.UserForLoginDto.AuthenticatorCode is null)
                {
                    // Authenticator kodu gönder ve gerekli bilgileri ayarla
                    await _authenticatorService.SendAuthenticatorCode(user);
                    loggedResponse.RequiredAuthenticatorType = user.AuthenticatorType;
                    return loggedResponse;
                }

                // Eğer istekte authenticator kodu varsa, kodu doğrula
                await _authenticatorService.VerifyAuthenticatorCode(user,
                    request.UserForLoginDto.AuthenticatorCode);
            }

            // Access Token oluştur
            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

            // Refresh Token oluştur ve veritabanına ekle
            Core.Security.Entities.RefreshToken createdRefreshToken =
                await _authService.CreateRefreshToken(user, request.IpAddress);
            Core.Security.Entities.RefreshToken addedRefreshToken =
                await _authService.AddRefreshToken(createdRefreshToken);
            // Eski Refresh Token'ları sil
            await _authService.DeleteOldRefreshTokens(user.Id);

            // Response nesnesini doldur ve geri döndür
            loggedResponse.AccessToken = createdAccessToken;
            loggedResponse.RefreshToken = addedRefreshToken;
            return loggedResponse;
        }
    }
}