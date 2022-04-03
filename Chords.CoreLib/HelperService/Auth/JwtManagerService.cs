using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens ;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Chords.CoreLib.HelperService.Auth
{
    public class JwtManagerService : IJwtManagerService
    {
        private readonly IJwtManagerConfiguration _configuration;
        public JwtManagerService(IJwtManagerConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string GenerateAccessToken(ClaimsIdentity claimsIdentity)
        {
            var issuedAt = DateTime.Now;
            var expires = DateTime.Now.AddMinutes(_configuration.JwtAccessTokenExpiryDuration);
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.JwtIssuer,
                Audience = _configuration.JwtAudience,
                Subject = claimsIdentity,
                IssuedAt = issuedAt,
                Expires = expires,
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = securityKey,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}