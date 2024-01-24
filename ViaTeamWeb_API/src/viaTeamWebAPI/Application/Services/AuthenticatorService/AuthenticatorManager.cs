using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.OtpAuthenticator;
using MimeKit;

namespace Application.Services.AuthenticatorService;

public class AuthenticatorManager : IAuthenticatorService
{
    private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
    private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
    private readonly IMailService _mailService;
    private readonly IOtpAuthenticatorHelper _otpAuthenticatorHelper;
    private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;

    public AuthenticatorManager(
        IEmailAuthenticatorHelper emailAuthenticatorHelper,
        IEmailAuthenticatorRepository emailAuthenticatorRepository,
        IMailService mailService,
        IOtpAuthenticatorHelper otpAuthenticatorHelper,
        IOtpAuthenticatorRepository otpAuthenticatorRepository
    )
    {
        _emailAuthenticatorHelper = emailAuthenticatorHelper;
        _emailAuthenticatorRepository = emailAuthenticatorRepository;
        _mailService = mailService;
        _otpAuthenticatorHelper = otpAuthenticatorHelper;
        _otpAuthenticatorRepository = otpAuthenticatorRepository;
    }

    // Kullanıcı için bir e-posta doğrulama nesnesi oluşturur
    public async Task<EmailAuthenticator> CreateEmailAuthenticator(User user)
    {
        EmailAuthenticator emailAuthenticator =
            new()
            {
                UserId = user.Id,
                ActivationKey = await _emailAuthenticatorHelper.CreateEmailActivationKey(),
                IsVerified = false
            };
        return emailAuthenticator;
    }

    // Kullanıcı için bir OTP (One-Time Password) doğrulama nesnesi oluşturur
    public async Task<OtpAuthenticator> CreateOtpAuthenticator(User user)
    {
        OtpAuthenticator otpAuthenticator =
            new()
            {
                UserId = user.Id,
                SecretKey = await _otpAuthenticatorHelper.GenerateSecretKey(),
                IsVerified = false
            };
        return otpAuthenticator;
    }

    // Bir byte dizisini bir string'e dönüştürür
    public async Task<string> ConvertSecretKeyToString(byte[] secretKey)
    {
        string result = await _otpAuthenticatorHelper.ConvertSecretKeyToString(secretKey);
        return result;
    }

    // Kullanıcıya doğrulama kodu gönderir
    public async Task SendAuthenticatorCode(User user)
    {
        if (user.AuthenticatorType is AuthenticatorType.Email)
            await SendAuthenticatorCodeWithEmail(user);
    }

    // Kullanıcının doğrulama kodunu doğrular
    public async Task VerifyAuthenticatorCode(User user, string authenticatorCode)
    {
        if (user.AuthenticatorType is AuthenticatorType.Email)
            await VerifyAuthenticatorCodeWithEmail(user, authenticatorCode);
        else if (user.AuthenticatorType is AuthenticatorType.Otp)
            await VerifyAuthenticatorCodeWithOtp(user, authenticatorCode);
    }

    // E-posta ile doğrulama kodunu gönderir
    private async Task SendAuthenticatorCodeWithEmail(User user)
    {
        EmailAuthenticator? emailAuthenticator =
            await _emailAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id);
        if (emailAuthenticator is null)
            throw new NotFoundException("Email Authenticator not found.");
        if (!emailAuthenticator.IsVerified)
            throw new BusinessException("Email Authenticator must be is verified.");

        // Yeni bir e-posta doğrulama kodu oluşturulur.
        string authenticatorCode = await _emailAuthenticatorHelper.CreateEmailActivationCode();

        // Oluşturulan kod, e-posta doğrulama bilgisinin ActivationKey özelliğine atanır.
        emailAuthenticator.ActivationKey = authenticatorCode;

        await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

        // E-posta gönderimi için gerekli olan adres bilgileri oluşturulur.
        var toEmailList =
            new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

        // E-posta servisi aracılığıyla kullanıcıya doğrulama kodu içeren e-posta gönderilir.k
        _mailService.SendMail(
            new Mail
            {
                ToList = toEmailList,
                Subject = "Authenticator Code - Viabelli - Sadık Sunbul",
                TextBody = $"Enter your authenticator code: {authenticatorCode}"
            }
        );
    }

    // E-posta ile doğrulama kodunu doğrular
    private async Task VerifyAuthenticatorCodeWithEmail(User user, string authenticatorCode)
    {
        EmailAuthenticator? emailAuthenticator =
            await _emailAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id);
        if (emailAuthenticator is null)
            throw new NotFoundException("Email Authenticator not found.");
        if (emailAuthenticator.ActivationKey != authenticatorCode)
            throw new BusinessException("Authenticator code is invalid.");
        emailAuthenticator.ActivationKey = null;
        await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
    }

    // OTP ile doğrulama kodunu doğrular
    private async Task VerifyAuthenticatorCodeWithOtp(User user, string authenticatorCode)
    {
        OtpAuthenticator? otpAuthenticator =
            await _otpAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id);
        if (otpAuthenticator is null)
            throw new NotFoundException("Otp Authenticator not found.");
        bool result = await _otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, authenticatorCode);
        if (!result)
            throw new BusinessException("Authenticator code is invalid.");
    }
}