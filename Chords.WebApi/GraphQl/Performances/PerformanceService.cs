using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.GraphQl.Performances
{
    public class PerformanceService : BaseService
    {
        private readonly IMapper _mapper;
        
        public PerformanceService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(httpContextAccessor, dbContextFactory)
        {
            _mapper = mapper;
        }
        
        public Task<IQueryable<Performance>> GetPerformances()
        {
            return Task.FromResult(DbContext.Performances.AsQueryable());
        }
        
        public Task<Performance> GetPerformance(object id)
        {
            return Task.FromResult(DbContext.Performances.Find(id));
        }

        public async Task<Performance> CreatePerformance(AddPerformanceInput addPerformanceInput)
        {
            Performance genre = _mapper.Map<Performance>(addPerformanceInput);
            genre.UpdatedBy = CurrentUserId;
            
            var entityEntry = await DbContext.AddAsync(genre);

            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Performance> UpdatePerformance(EditPerformanceInput editPerformanceInput)
        {
            Performance genre = _mapper.Map<Performance>(editPerformanceInput);
            genre.UpdatedBy = CurrentUserId;
            
            var entityEntry = DbContext.Update(genre);

            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }

        public async Task<Performance> RemovePerformance(object id)
        {
            Performance genre = new Performance
            {
                Id = $"{id}",
                IsDeleted = true,
                UpdatedBy = CurrentUserId
            };

            var entityEntry = DbContext.Remove(genre);
            
            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }
    }
}