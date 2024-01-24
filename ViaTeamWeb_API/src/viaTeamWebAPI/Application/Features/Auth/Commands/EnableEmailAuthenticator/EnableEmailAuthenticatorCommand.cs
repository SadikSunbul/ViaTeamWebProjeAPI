using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Core.Mailing;
using Core.Security.Entities;
using Core.Security.Enums;
using MediatR;
using MimeKit;
using System.Web;

namespace Application.Features.Auth.Commands.EnableEmailAuthenticator;

public class EnableEmailAuthenticatorCommand : IRequest
{
    public int UserId { get; set; }
    public string VerifyEmailUrlPrefix { get; set; }

    public EnableEmailAuthenticatorCommand()
    {
        VerifyEmailUrlPrefix = string.Empty;
    }

    public EnableEmailAuthenticatorCommand(int userId, string verifyEmailUrlPrefix)
    {
        UserId = userId;
        VerifyEmailUrlPrefix = verifyEmailUrlPrefix;
    }

    public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IMailService _mailService;
        private readonly IUserService _userService;

        public EnableEmailAuthenticatorCommandHandler(
            IUserService userService,
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            IMailService mailService,
            AuthBusinessRules authBusinessRules,
            IAuthenticatorService authenticatorService
        )
        {
            _userService = userService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            // Kullanıcının ID'sine göre bilgileri getir.
            User? user = await _userService.GetAsync(predicate: u => u.Id == request.UserId,
                cancellationToken: cancellationToken);
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            // Eğer kullanıcının zaten bir authenticator'ı varsa, iş kuralı hatası fırlatılır.
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user!);

            // Kullanıcının authenticator tipi e-posta olarak ayarlanır.
            user!.AuthenticatorType = AuthenticatorType.Email;
            // Kullanıcı bilgileri güncellenir.
            await _userService.UpdateAsync(user);

            // Kullanıcının e-posta authenticator'ı oluşturulur.
            EmailAuthenticator emailAuthenticator = await _authenticatorService
                .CreateEmailAuthenticator(user);
            // Oluşturulan e-posta authenticator'ı veritabanına eklenir.
            EmailAuthenticator addedEmailAuthenticator =
                await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);

            // E-posta gönderim işlemi için alıcı e-posta adresi oluşturulur.
            var toEmailList =
                new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

            // E-posta gönderim işlemi gerçekleştirilir.
            _mailService.SendMail(
                new Mail
                {
                    ToList = toEmailList,
                    Subject = "Verify Your Email - NArchitecture",
                    TextBody =
                        $"Click on the link to verify your email: {request.VerifyEmailUrlPrefix}?ActivationKey={HttpUtility.UrlEncode(addedEmailAuthenticator.ActivationKey)}"
                }
            );
        }
    }
}