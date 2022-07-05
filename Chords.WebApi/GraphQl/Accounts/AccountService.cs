using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chords.CoreLib.Utils;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.GraphQl.Accounts
{
    public class AccountService : BaseService<Account>
    {
        private readonly IMapper _mapper;
        
        public AccountService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(mapper, httpContextAccessor, dbContextFactory)
        {
            _mapper = mapper;
        }
        
        public Task<IQueryable<Account>> GetAccounts()
        {
            return Task.FromResult(DbContext.Accounts.AsQueryable());
        }
        
        public Task<Account> GetAccount(object id)
        {
            return Task.FromResult(DbContext.Accounts.Find(id));
        }
        
        public Task<IQueryable<Account>> GetAccountByIds(IReadOnlyList<object> ids)
        {
            return Task.FromResult(DbContext.Accounts.Where(_ => ids.Contains(_.Id)));
        }

        public async Task<Account> CreateAccount(AddAccountInput addAccountInput)
        {
            Account account = await PreCreate(addAccountInput);
            account.Password = CryptoHelpers.PasswordHash(addAccountInput.Password);
            
            var entityEntry = await DbContext.AddAsync(account);

            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Account> UpdateAccount(EditAccountInput editAccountInput)
        {
            Account genre = await PreUpdate(editAccountInput);
            
            var entityEntry = DbContext.Update(genre);

            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }

        public async Task<Account> RemoveAccount(object id)
        {
            Account genre = await PreRemove(id);

            var entityEntry = DbContext.Remove(genre);
            
            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }
    }
}