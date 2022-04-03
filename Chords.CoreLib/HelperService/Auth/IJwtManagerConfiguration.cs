using Microsoft.Extensions.Configuration;

namespace Chords.CoreLib.HelperService.Auth
{
    public interface IJwtManagerConfiguration
    {
        IConfigurationSection GetConfigurationSection(string key);
        
        public string JwtKey { get; }
        public string JwtIssuer { get; }
        public string JwtAudience { get; }
        public double JwtAccessTokenExpiryDuration { get; }
        public double JwtRefreshTokenExpiryDuration { get; }
    }
}