
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Chords.CoreLib.HelperService.Auth;
using Chords.CoreLib.Utils;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.Web.GraphQl.Auth;
using Chords.WebApi.Common;
using Chords.WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.GraphQl.Auth
{
    public class AuthService : BaseService
    {
        private readonly IMapper _mapper;
        private readonly IJwtManagerService _jwtManagerService;

        public AuthService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IDbContextFactory<ChordsDbContext> dbContextFactory,
            IJwtManagerService jwtManagerService) : base(httpContextAccessor, dbContextFactory)
        {
            _mapper = mapper;
            _jwtManagerService = jwtManagerService;
        }

        public async Task<Token> Login(LoginInput loginInput)
        {
            Account account = DbContext.Accounts.FirstOrDefault(_ => _.Email == loginInput.Email);
            if (account == null || !CryptoHelpers.VerifyPassword(loginInput.Password, account.Password))
            {
                throw new Exception("Invalid credentials. Email or password is not correct.");
            }

            ClaimsIdentity claimsIdentity = AuthHelpers.ArchiveCurrentUser(account);
            string accessToken = _jwtManagerService.GenerateAccessToken(claimsIdentity);
            string refreshToken = _jwtManagerService.GenerateRefreshToken();
            Token token = new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = account.Id
            };
            DbContext.Add(token);
            
            account.Tokens.Add(token);
            
            await DbContext.SaveChangesAsync();
            
            return token;
        }

        public async Task<Token> Register(RegisterInput registerInput)
        {
            Account account = DbContext.Accounts.FirstOrDefault(_ => _.Email == registerInput.Email);
            if (account != null)
            {
                throw new Exception("Email already exists.");
            }
            
            account = new Account
            {
                Email = registerInput.Email,
                Password = CryptoHelpers.PasswordHash(registerInput.Password),
                Username = registerInput.Name
            };
            
            DbContext.Add(account);
                
            ClaimsIdentity claimsIdentity = AuthHelpers.ArchiveCurrentUser(account);
            string accessToken = _jwtManagerService.GenerateAccessToken(claimsIdentity);
            string refreshToken = _jwtManagerService.GenerateRefreshToken();
            Token token = new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = account.Id
            };
            DbContext.Add(token);
                
            account.Tokens.Add(token);
                
            await DbContext.SaveChangesAsync();
            
            return token;
        }
    }

}