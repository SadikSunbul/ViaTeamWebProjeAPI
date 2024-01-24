using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Core.Security.Entities;
using Core.Security.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyOtpAuthenticator;

public class VerifyOtpAuthenticatorCommand : IRequest
{
    public int UserId { get; set; }
    public string ActivationCode { get; set; }

    public VerifyOtpAuthenticatorCommand()
    {
        ActivationCode = string.Empty;
    }

    public VerifyOtpAuthenticatorCommand(int userId, string activationCode)
    {
        UserId = userId;
        ActivationCode = activationCode;
    }

    public class VerifyOtpAuthenticatorCommandHandler : IRequestHandler<VerifyOtpAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
        private readonly IUserService _userService;

        public VerifyOtpAuthenticatorCommandHandler(
            IOtpAuthenticatorRepository otpAuthenticatorRepository,
            AuthBusinessRules authBusinessRules,
            IUserService userService,
            IAuthenticatorService authenticatorService
        )
        {
            _otpAuthenticatorRepository = otpAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
            _userService = userService;
            _authenticatorService = authenticatorService;
        }

        public async Task Handle(VerifyOtpAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            // Kullanıcının ID'sine göre var olan OTP authenticator'ı getir.
            OtpAuthenticator? otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(
                predicate: e => e.UserId == request.UserId,
                cancellationToken: cancellationToken
            );
            // Eğer var olan bir OTP authenticator bulunamazsa veya null ise, iş kuralı hatası fırlatılır.
            await _authBusinessRules.OtpAuthenticatorShouldBeExists(otpAuthenticator);

            // Kullanıcıyı ID'sine göre getir.
            User? user = await _userService.GetAsync(predicate: u => u.Id == request.UserId, cancellationToken: cancellationToken);
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);

            // OTP authenticator'ı doğrulandı olarak işaretle.
            otpAuthenticator!.IsVerified = true;
            // Kullanıcının kimlik doğrulama tipini OTP olarak ayarla.
            user!.AuthenticatorType = AuthenticatorType.Otp;

            // Kullanıcının kimlik doğrulama kodunu kontrol et.
            await _authenticatorService.VerifyAuthenticatorCode(user, request.ActivationCode);

            // OTP authenticator'ı güncelle.
            await _otpAuthenticatorRepository.UpdateAsync(otpAuthenticator);
            // Kullanıcı bilgilerini güncelle.
            await _userService.UpdateAsync(user);
        }
    }
}
