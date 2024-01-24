using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.EnableOtpAuthenticator;

public class EnableOtpAuthenticatorCommand : IRequest<EnabledOtpAuthenticatorResponse>
{
    public int UserId { get; set; }

    public class EnableOtpAuthenticatorCommandHandler : IRequestHandler<EnableOtpAuthenticatorCommand, EnabledOtpAuthenticatorResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
        private readonly IUserService _userService;

        public EnableOtpAuthenticatorCommandHandler(
            IUserService userService,
            IOtpAuthenticatorRepository otpAuthenticatorRepository,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userService = userService;
            _otpAuthenticatorRepository = otpAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task<EnabledOtpAuthenticatorResponse> Handle(
            EnableOtpAuthenticatorCommand request,
            CancellationToken cancellationToken
        )
        {
            // Kullanıcının ID'sine göre bilgileri getir.
            User? user = await _userService.GetAsync(predicate: u => u.Id == request.UserId, cancellationToken: cancellationToken);
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            // Eğer kullanıcının zaten bir authenticator'ı varsa, iş kuralı hatası fırlatılır.
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user!);

            // Kullanıcının ID'sine göre var olan OTP authenticator'ı bilgilerini getir.
            OtpAuthenticator? doesExistOtpAuthenticator = await _otpAuthenticatorRepository.GetAsync(
                predicate: o => o.UserId == request.UserId,
                cancellationToken: cancellationToken
            );
            // Eğer var olan bir OTP authenticator varsa ve doğrulanmışsa, iş kuralı hatası fırlatılır.
            await _authBusinessRules.OtpAuthenticatorThatVerifiedShouldNotBeExists(doesExistOtpAuthenticator);
            // Eğer var olan bir OTP authenticator varsa, silinir.
            if (doesExistOtpAuthenticator is not null)
                await _otpAuthenticatorRepository.DeleteAsync(doesExistOtpAuthenticator);

            // Yeni bir OTP authenticator oluşturulur.
            OtpAuthenticator newOtpAuthenticator = await _authenticatorService.CreateOtpAuthenticator(user!);
            // Oluşturulan OTP authenticator veritabanına eklenir.
            OtpAuthenticator addedOtpAuthenticator = await _otpAuthenticatorRepository.AddAsync(newOtpAuthenticator);

            // Etkinleştirilen OTP authenticator'ın bilgilerini içeren bir yanıt nesnesi oluşturulur.
            EnabledOtpAuthenticatorResponse enabledOtpAuthenticatorDto =
                new() { SecretKey = await _authenticatorService.ConvertSecretKeyToString(addedOtpAuthenticator.SecretKey) };
            return enabledOtpAuthenticatorDto;
        }
    }
}
