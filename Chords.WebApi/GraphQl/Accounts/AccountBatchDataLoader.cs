using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chords.DataAccess.Models;
using GreenDonut;
using HotChocolate;

namespace Chords.WebApi.GraphQl.Accounts
{
    public class AccountBatchDataLoader : BatchDataLoader<string, Account>
    {
        private readonly AccountService _accountService;
        public AccountBatchDataLoader(AccountService accountService, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _accountService = accountService;
        }

        protected override async Task<IReadOnlyDictionary<string, Account>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var result = await _accountService.GetAccountByIds(keys);
            return result.ToDictionary(_ => _.Id);
        }
    }
}