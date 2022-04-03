using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Chords.CoreLib.HelperService.Auth;
using Chords.CoreLib.Utils;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.Web.GraphQl.Auth;
using Chords.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.GraphQl.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IJwtManagerService _jwtManagerService;
        private readonly IDbContextFactory<ChordsDbContext> _dbContextFactory;
        
        public AuthService(IMapper mapper, IDbContextFactory<ChordsDbContext> dbContextFactory,
            IJwtManagerService jwtManagerService)
        {
            _mapper = mapper;
            _jwtManagerService = jwtManagerService;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Token> Login(LoginInput loginInput)
        {
            try
            {
                await using var dbContext = _dbContextFactory.CreateDbContext();
                
                Account account = dbContext.Accounts.FirstOrDefault(_ => _.Email == loginInput.Email);
                if (account == null || CryptoHelpers.VerifyPassword(loginInput.Password, account.Password))
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
                
                account.Tokens.Add(token);
                
                await dbContext.SaveChangesAsync();
                
                return token;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<Token> Register(RegisterInput registerInput)
        {
            try
            {
                await using var dbContext = _dbContextFactory.CreateDbContext();
                
                Account account = dbContext.Accounts.FirstOrDefault(_ => _.Email == registerInput.Email);
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

                dbContext.Add(account);
                
                ClaimsIdentity claimsIdentity = AuthHelpers.ArchiveCurrentUser(account);
                string accessToken = _jwtManagerService.GenerateAccessToken(claimsIdentity);
                string refreshToken = _jwtManagerService.GenerateRefreshToken();
                Token token = new Token
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    UserId = account.Id
                };
                dbContext.Add(token);
                
                account.Tokens.Add(token);
                
                await dbContext.SaveChangesAsync();
            
                return token;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        
        
    }

}