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
    public class ArtistService : BaseService
    {
        private readonly IMapper _mapper;
        
        public ArtistService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(httpContextAccessor, dbContextFactory)
        {
            _mapper = mapper;
        }
        
        public Task<IQueryable<Artist>> GetArtists()
        {
            return Task.FromResult(DbContext.Artists.AsQueryable());
        }
        
        public Task<Artist> GetArtist(object id)
        {
            return Task.FromResult(DbContext.Artists.Find(id));
        }

        public async Task<Artist> CreateArtist(AddArtistInput addArtistInput)
        {
            Artist genre = _mapper.Map<Artist>(addArtistInput);
            genre.UpdatedBy = CurrentUserId;
            
            var entityEntry = await DbContext.AddAsync(genre);

            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Artist> UpdateArtist(EditArtistInput editArtistInput)
        {
            Artist genre = _mapper.Map<Artist>(editArtistInput);
            genre.UpdatedBy = CurrentUserId;
            
            var entityEntry = DbContext.Update(genre);

            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }

        public async Task<Artist> RemoveArtist(object id)
        {
            Artist genre = new Artist
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