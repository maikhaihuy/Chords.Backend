using System.Collections.Generic;
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
    public class SongService : BaseService<Song>
    {
        private readonly IMapper _mapper;
        
        public SongService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(mapper, httpContextAccessor, dbContextFactory)
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
        
        public Task<IQueryable<Song>> GetSongByIds(IReadOnlyList<object> ids)
        {
            return Task.FromResult(DbContext.Songs.Where(_ => ids.Contains(_.Id)));
        }

        public async Task<Song> CreateSong(AddSongInput addSongInput)
        {
            Song genre = await PreCreate(addSongInput);
            
            var entityEntry = await DbContext.AddAsync(genre);

            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Song> UpdateSong(EditSongInput editSongInput)
        {
            Song genre = await PreUpdate(editSongInput);
            
            var entityEntry = DbContext.Update(genre);

            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }

        public async Task<Song> RemoveSong(object id)
        {
            Song genre = await PreRemove(id);

            var entityEntry = DbContext.Remove(genre);
            
            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }
    }
}