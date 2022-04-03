using System.Security.Claims;

namespace Chords.WebApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Name);
        }
        
        public static string Id(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Sid);
        }
        
        public static string Email(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        }
    }
}