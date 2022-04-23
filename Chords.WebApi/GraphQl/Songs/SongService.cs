using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.GraphQl.Songs
{
    public class SongService : BaseService
    {
        private readonly IMapper _mapper;
        
        public SongService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(httpContextAccessor, dbContextFactory)
        {
        }
        
        public Task<IQueryable<Song>> GetSongs()
        {
            return Task.FromResult(DbContext.Songs.AsQueryable());
        }
        
        public Task<Song> GetSong(object id)
        {
            return Task.FromResult(DbContext.Songs.Find(id));
        }

        public async Task<Song> CreateSong(AddSongInput addSongInput)
        {
            Song genre = _mapper.Map<Song>(addSongInput);
            genre.UpdatedBy = CurrentUserId;
            
            var entityEntry = await DbContext.AddAsync(genre);

            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Song> UpdateSong(EditSongInput editSongInput)
        {
            Song genre = _mapper.Map<Song>(editSongInput);
            genre.UpdatedBy = CurrentUserId;
            
            var entityEntry = DbContext.Update(genre);

            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }

        public async Task<Song> RemoveSong(object id)
        {
            Song genre = new Song
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