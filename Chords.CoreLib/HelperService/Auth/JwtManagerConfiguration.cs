using Chords.CoreLib.Common;
using Microsoft.Extensions.Configuration;

namespace Chords.CoreLib.HelperService.Auth
{
    public class JwtManagerConfiguration : IJwtManagerConfiguration
    {
        private readonly IConfiguration _configuration;
        public JwtManagerConfiguration(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string JwtKey => _configuration[$"{Constants.JwtConfigRoot.JwtKey}"];
        public string JwtIssuer => _configuration[$"{Constants.JwtConfigRoot.JwtIssuer}"];
        public string JwtAudience => _configuration[$"{Constants.JwtConfigRoot.JwtAudience}"];
        public double JwtAccessTokenExpiryDuration => double.Parse(_configuration[$"{Constants.JwtConfigRoot.JwtAccessTokenExpiryDuration}"]);
        public double JwtRefreshTokenExpiryDuration => double.Parse(_configuration[$"{Constants.JwtConfigRoot.JwtRefreshTokenExpiryDuration}"]);
        
        public IConfigurationSection GetConfigurationSection(string key)
        {
            return this._configuration.GetSection(key);
        }
    }
}