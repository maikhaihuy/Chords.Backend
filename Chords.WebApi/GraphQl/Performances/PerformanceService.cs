using System.Collections.Generic;
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
    public class PerformanceService : BaseService<Performance>
    {
        private readonly IMapper _mapper;
        
        public PerformanceService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(mapper, httpContextAccessor, dbContextFactory)
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

        public async Task<ILookup<string, Performance>> GetPerformancesBySongIds(IReadOnlyList<object> songIds)
        {
            var songsIncludePerformances = await DbContext.Songs
                .Where(_ => songIds.Contains(_.Id))
                .Include(_ => _.Performances)
                .ToListAsync();
            var performances = songsIncludePerformances
                .SelectMany(_ => _.Performances.Select(__ => new {SongId = _.Id, Performance = __ }))
                .ToLookup(pair => pair.SongId, pair => pair.Performance);
            return performances;
        }
        
        public async Task<Performance> CreatePerformance(AddPerformanceInput addPerformanceInput)
        {
            Performance genre = await PreCreate(addPerformanceInput);
            
            var entityEntry = await DbContext.AddAsync(genre);

            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Performance> UpdatePerformance(EditPerformanceInput editPerformanceInput)
        {
            Performance genre = await PreUpdate(editPerformanceInput);
            
            var entityEntry = DbContext.Update(genre);

            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }

        public async Task<Performance> RemovePerformance(object id)
        {
            Performance genre = await PreRemove(id);

            var entityEntry = DbContext.Remove(genre);
            
            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }
    }
}