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
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ChordsDbContext DbContext;
        
        protected string CurrentUserId => GetCurrentUserId();

        protected BaseService([Service] IHttpContextAccessor httpContextAccessor, IDbContextFactory<ChordsDbContext> dbContextFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            DbContext = dbContextFactory.CreateDbContext();
        }

        protected string GetCurrentUserId()
        {
            string userId = "Anonymous";
            if (_httpContextAccessor.HttpContext != null)
                userId = _httpContextAccessor.HttpContext.User.Id();
            return userId;
        }

        public ValueTask DisposeAsync()
        {
            return DbContext.DisposeAsync();
        }
    }
}