using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyEmailAuthenticator;

public class VerifyEmailAuthenticatorCommand : IRequest
{
    public string ActivationKey { get; set; }

    public VerifyEmailAuthenticatorCommand()
    {
        ActivationKey = string.Empty;
    }

    public VerifyEmailAuthenticatorCommand(string activationKey)
    {
        ActivationKey = activationKey;
    }

    public class VerifyEmailAuthenticatorCommandHandler : IRequestHandler<VerifyEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;

        public VerifyEmailAuthenticatorCommandHandler(
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            AuthBusinessRules authBusinessRules
        )
        {
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task Handle(VerifyEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            // Belirli bir aktivasyon anahtarı ile eşleşen e-posta authenticator'ını getir.
            EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(
                predicate: e => e.ActivationKey == request.ActivationKey,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.EmailAuthenticatorShouldBeExists(emailAuthenticator);
            // Eğer e-posta authenticator'ın aktivasyon anahtarı mevcut değilse, iş kuralı hatası fırlatılır.
            await _authBusinessRules.EmailAuthenticatorActivationKeyShouldBeExists(emailAuthenticator!);
            // E-posta authenticator'ın aktivasyon anahtarı temizlenir ve doğrulama durumu "true" olarak ayarlanır.
            emailAuthenticator!.ActivationKey = null;
            emailAuthenticator.IsVerified = true;
            // E-posta authenticator'ın bilgileri veritabanında güncellenir.
            await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
        }
    }
}