using System;
using System.Security.Claims;
using Chords.DataAccess.Models;
using Chords.WebApi.Extensions;

namespace Chords.WebApi.Helpers
{
    public class AuthHelpers
    {
        public static ClaimsIdentity ArchiveCurrentUser(Account account)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Sid, account.Id),
                new Claim(ClaimTypes.Email, account.Email)
            };
            
            return new ClaimsIdentity(claims);
        }
        
        public static Account ExtractCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null) return null;

            string name = claimsPrincipal.UserName();
            string id = claimsPrincipal.Id();
            string email = claimsPrincipal.Email();

            return new Account
            {
                Username = name,
                Id = id,
                Email = email
            };
        }
    }
}