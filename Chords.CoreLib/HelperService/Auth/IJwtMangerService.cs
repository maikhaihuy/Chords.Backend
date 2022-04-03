using System.Security.Claims;

namespace Chords.CoreLib.HelperService.Auth
{
    public interface IJwtManagerService
    {
        string GenerateAccessToken(ClaimsIdentity claimsIdentity);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
    }
}