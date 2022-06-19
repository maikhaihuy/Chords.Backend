using System;
using System.Threading.Tasks;
using AutoMapper;
using Chords.CoreLib.DataAccess.Models;
using Chords.CoreLib.Utils;
using Chords.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.Common
{
    public class BaseService<T> : BaseService where T : class, IBaseModel
    {
        private readonly IMapper _mapper;
        protected BaseService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(httpContextAccessor, dbContextFactory)
        {
            _mapper = mapper;
        }

        private async Task<T> FindAsync(object id)
        {
            T? t = await DbContext.FindAsync<T>(id);
            if (t == null) throw new ArgumentException($"[{typeof(T).Name}] [{id}] is not exists.");
            return t;
        }

        private T MergeInput<TInput>(TInput input)
        {
            T tInput = _mapper.Map<T>(input);

            if (tInput is IAuditFields auditFields)
            {
                auditFields.UpdatedBy = CurrentUserId;
            }

            return tInput;
        }
        
        protected async Task<T> PreCreate<TInput>(TInput input) => MergeInput(input);
        
        protected async Task<T> PreUpdate<TInput>(TInput input)
        {
            T tInput = MergeInput(input);

            T tDb = await FindAsync(tInput);
            ReflectionHelpers.MergeFieldsChanged(tInput, tDb);
            
            return tDb;
        }

        protected async Task<T> PreRemove(object id)
        {
            T t = await FindAsync(id);
            
            if (t is IAuditFields auditFields)
            {
                auditFields.UpdatedBy = CurrentUserId;
            }

            return t;
        }
    }
}