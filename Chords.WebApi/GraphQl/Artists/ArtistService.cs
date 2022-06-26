using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.GraphQl.Artists
{
    public class ArtistService : BaseService<Artist>
    {
        public ArtistService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(mapper, httpContextAccessor, dbContextFactory)
        {
        }
        
        public Task<IQueryable<Artist>> GetArtists()
        {
            return Task.FromResult(DbContext.Artists.AsQueryable());
        }
        
        public Task<Artist> GetArtist(object id)
        {
            return Task.FromResult(DbContext.Artists.Find(id));
        }

        public Task<IQueryable<Artist>> GetArtistsByIds(IReadOnlyList<object> ids)
        {
            return Task.FromResult(DbContext.Artists.Where(_ => ids.Contains(_.Id)));
        }

        public Task<IQueryable<Artist>> GetArtistsByPerformanceIds(IReadOnlyList<object> performanceIds)
        {
            var artistsQueryable = DbContext.Performances.Where(_ => performanceIds.Contains(_.Id))
                .Include(_ => _.Singers)
                .SelectMany(_ => _.Singers)
                .Distinct();
            
            return Task.FromResult(artistsQueryable);
        }

        public async Task<ILookup<string, Artist>> GetAuthorsBySongIds(IReadOnlyList<object> songIds)
        {
            var songsIncludeAuthors = await DbContext.Songs
                .Where(_ => songIds.Contains(_.Id))
                .Include(_ => _.Authors)
                .ToListAsync();
            var authors = songsIncludeAuthors
                .SelectMany(_ => _.Authors.Select(__ => new {SongId = _.Id, Author = __ }))
                .ToLookup(pair => pair.SongId, pair => pair.Author);
            return authors;
        }

        public async Task<Artist> CreateArtist(AddArtistInput addArtistInput)
        {
            Artist artist = await PreCreate(addArtistInput);
            
            var entityEntry = await DbContext.AddAsync(artist);

            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Artist> UpdateArtist(EditArtistInput editArtistInput)
        {
            Artist artist = await PreUpdate(editArtistInput);

            var entityEntry = DbContext.Update(artist);

            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }

        public async Task<Artist> RemoveArtist(object id)
        {
            Artist artist = await PreRemove(id);

            var entityEntry = DbContext.Remove(artist);
            
            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }
    }
}