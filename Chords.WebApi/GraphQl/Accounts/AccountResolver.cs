using System.Threading.Tasks;
using Chords.CoreLib.DataAccess.Models;
using Chords.DataAccess.Models;
using HotChocolate;

namespace Chords.WebApi.GraphQl.Accounts
{
    public class AccountResolver
    {
        public Task<Account> GetCreator<T>([Parent] T parent, AccountBatchDataLoader dataLoader)
        {
            if (!(parent is IAuditFields auditFields) || string.IsNullOrEmpty(auditFields.CreatedBy))
                return Task.FromResult<Account>(null);

            return dataLoader.LoadAsync(auditFields.CreatedBy);
        }
        
        public Task<Account> GetUpdater<T>([Parent] T parent, AccountBatchDataLoader dataLoader)
        {
            if (!(parent is IAuditFields auditFields) || string.IsNullOrEmpty(auditFields.UpdatedBy))
                return Task.FromResult<Account>(null);

            return dataLoader.LoadAsync(auditFields.UpdatedBy);
        }
    }
}