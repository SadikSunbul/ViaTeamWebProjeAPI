using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class BaseController : ControllerBase
{
    protected IMediator Mediator =>
        _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _mediator;

    // IP adresini alma işlemini gerçekleştiren metot.
    protected string getIpAddress()
    {
        // "X-Forwarded-For" başlığı var mı kontrol edilir.
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            // Var ise, bu başlık kullanılır.
            ? Request.Headers["X-Forwarded-For"].ToString()
            // Yok ise, HttpContext.Connection.RemoteIpAddress kullanılarak uzak IP adresi alınır.
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
              // Eğer IP adresi alınamazsa, InvalidOperationException hatası fırlatılır.
              ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");

        return ipAddress;
    }

    protected int getUserIdFromRequest() //todo authentication behavior?
    {
        int userId = HttpContext.User.GetUserId();
        return userId;
    }
}