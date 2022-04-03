namespace Chords.CoreLib.Common
{
    public class Constants
    {
        public static class JwtConfigRoot
        {
            public const string JwtKey = "Jwt:Key";
            public const string JwtIssuer = "Jwt:Issuer";
            public const string JwtAudience = "Jwt:Audience";
            public const string JwtAccessTokenExpiryDuration = "Jwt:AccessTokenExpiryDuration";
            public const string JwtRefreshTokenExpiryDuration = "Jwt:RefreshTokenExpiryDuration";
        }
    }
}