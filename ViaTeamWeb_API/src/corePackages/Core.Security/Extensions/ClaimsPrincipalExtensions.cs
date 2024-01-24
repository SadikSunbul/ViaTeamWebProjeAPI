using System.Security.Claims;

namespace Core.Security.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        return result;
    }

    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal?.Claims(ClaimTypes.Role);

    public static int GetUserId(this ClaimsPrincipal claimsPrincipal) =>
        Convert.ToInt32(claimsPrincipal?.Claims(ClaimTypes.NameIdentifier)?.FirstOrDefault());
    
       // public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
       // {
       // var userIdClaim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
       //
       // if (int.TryParse(userIdClaim, out int userId))
       // {
       // return userId;
       // }
       //
       // // Hata durumunda -1 veya başka bir değer döndürebilirsiniz.
       // return -1;
       // }
       
     
}
