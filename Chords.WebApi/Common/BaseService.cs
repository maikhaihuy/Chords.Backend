using System;
using System.Threading.Tasks;
using Chords.DataAccess.EntityFramework;
using Chords.WebApi.Extensions;
using HotChocolate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.Common
{
    public class BaseService : IAsyncDisposable
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ChordsDbContext _dbContext;

        protected BaseService([Service] IHttpContextAccessor contextAccessor, IDbContextFactory<ChordsDbContext> dbContextFactory)
        {
            _contextAccessor = contextAccessor;
            _dbContext = dbContextFactory.CreateDbContext();
        }

        protected string GetCurrentUserId() => _contextAccessor.HttpContext?.User.Id();

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}