using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.EnableOtpAuthenticator;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.RevokeToken;
using Application.Features.Auth.Commands.VerifyEmailAuthenticator;
using Application.Features.Auth.Commands.VerifyOtpAuthenticator;
using Core.Application.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly WebApiConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        const string configurationSection = "WebAPIConfiguration";
        _configuration =
            configuration.GetSection(configurationSection).Get<WebApiConfiguration>()
            ?? throw new NullReferenceException(
                $"\"{configurationSection}\" section cannot found in configuration.");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IpAddress = getIpAddress() };
        LoggedResponse result = await Mediator.Send(loginCommand);

        if (result.RefreshToken is not null)
            setRefreshTokenToCookie(result.RefreshToken);

        return Ok(result.ToHttpResponse());
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand =
            new() { UserForRegisterDto = userForRegisterDto, IpAddress = getIpAddress() };
        RegisteredResponse result = await Mediator.Send(registerCommand);
        setRefreshTokenToCookie(result.RefreshToken);
        return Created(uri: "", result.AccessToken);
    }

    [HttpGet("RefreshToken")]
    public async Task<IActionResult> RefreshToken()  //çerezlerle çalışır
    {
        RefreshTokenCommand refreshTokenCommand =
            new() { RefreshToken = getRefreshTokenFromCookies(), IpAddress = getIpAddress() };
        RefreshedTokensResponse result = await Mediator.Send(refreshTokenCommand);
        setRefreshTokenToCookie(result.RefreshToken);
        return Created(uri: "", result.AccessToken);
    }

    [HttpPut("RevokeToken")]
    public async Task<IActionResult> RevokeToken(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] string? refreshToken)
    {
        RevokeTokenCommand revokeTokenCommand = new()
        {
            Token = refreshToken ?? getRefreshTokenFromCookies(), IpAddress = getIpAddress()
        };
        RevokedTokenResponse result = await Mediator.Send(revokeTokenCommand);
        return Ok(result);
    }

    //bir kullanıcının e-posta tabanlı authenticator'ını etkinleştirmek için kullanılır
    [HttpGet("EnableEmailAuthenticator")]
    public async Task<IActionResult> EnableEmailAuthenticator()
    {
        EnableEmailAuthenticatorCommand enableEmailAuthenticatorCommand =
            new()
            {
                UserId = getUserIdFromRequest(),
                VerifyEmailUrlPrefix = $"{_configuration.ApiDomain}/Auth/VerifyEmailAuthenticator"
            };
        await Mediator.Send(enableEmailAuthenticatorCommand);

        return Ok();
    }

    //kullanıcının OTP authenticator'ını etkinleştirmek için gerçekleştirilir
    [HttpGet("EnableOtpAuthenticator")]
    public async Task<IActionResult> EnableOtpAuthenticator()
    {
        EnableOtpAuthenticatorCommand enableOtpAuthenticatorCommand =
            new() { UserId = getUserIdFromRequest() };
        EnabledOtpAuthenticatorResponse result = await Mediator.Send(enableOtpAuthenticatorCommand);

        return Ok(result);
    }

    //kullanıcının e-posta authenticator'ını doğrulamak için gerçekleştirilir
    [HttpGet("VerifyEmailAuthenticator")]
    public async Task<IActionResult> VerifyEmailAuthenticator(
        [FromQuery] VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand)
    {
        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return Ok();
    }

    //OTP (One-Time Password) kimlik doğrulama işlemi sırasında gerçekleşen bir komut
    [HttpPost("VerifyOtpAuthenticator")]
    public async Task<IActionResult> VerifyOtpAuthenticator([FromBody] string authenticatorCode)
    {
        VerifyOtpAuthenticatorCommand verifyEmailAuthenticatorCommand =
            new() { UserId = getUserIdFromRequest(), ActivationCode = authenticatorCode };

        await Mediator.Send(verifyEmailAuthenticatorCommand);
        return Ok();
    }

    private string getRefreshTokenFromCookies() =>
        Request.Cookies["refreshToken"] ??
        throw new ArgumentException("Refresh token is not found in request cookies.");
    //Bu metot, HTTP isteği içerisinden "refreshToken" adlı bir çerezin alınmasını sağlar. İşleyişini şu şekilde açıklayabilirim:
    // 
    // Request.Cookies["refreshToken"] ifadesi, HTTP isteği içindeki çerezlerden "refreshToken" adlı çerezi almaya çalışır.
    // Eğer bu çerez bulunamazsa, ?? null koalesans operatörü kullanılarak throw new ArgumentException("Refresh token is not found in request cookies.") ifadesi çalıştırılır. Yani, bir istisna fırlatılır ve kullanıcıya "Refresh token bulunamadı" şeklinde bir hata mesajı gönderilir.

    private void setRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append(key: "refreshToken", refreshToken.Token, cookieOptions);
    }
    //Bu metot, bir RefreshToken nesnesini kullanarak tarayıcı çerezine bu tokeni ekler. İşleyişini şu şekilde açıklayabilirim:
    // 
    // CookieOptions nesnesi oluşturularak çerezin bazı özellikleri belirlenir. HttpOnly özelliği true olarak ayarlanarak, tarayıcı tarafından çerezin JavaScript tarafından erişilememesi sağlanır.
    // Expires özelliği ile çerezin ne zaman geçerliliğini yitireceği belirlenir. Bu örnekte, çerezin 7 gün boyunca geçerli olmasını sağlamak için DateTime.UtcNow.AddDays(7) ifadesi kullanılır.
    // Response.Cookies.Append metoduyla çerez, "refreshToken" adıyla ve RefreshToken nesnesinden alınan token değeriyle tarayıcıya eklenir. Bu sayede tarayıcı, ilerleyen isteklerde bu çerezi kullanabilir.
    // 
}