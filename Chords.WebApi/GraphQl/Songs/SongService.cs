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
            Song song = await PreCreate(addSongInput);
            
            // adding artist song
            if (addSongInput.AuthorIds is {Length: > 0})
            {
                var artistIds = addSongInput.AuthorIds.Distinct().ToList();
                List<Artist> artists = await DbContext.Artists.Where(_ => artistIds.Contains(_.Id)).ToListAsync();
                if (artists.Count > 0) song.Authors = artists;
            }

            var entityEntry = await DbContext.AddAsync(song);
            
            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Song> UpdateSong(EditSongInput editSongInput)
        {
            Song song = await PreUpdate(editSongInput);
            
            // update artist song
            if (editSongInput.AuthorIds is {Length: > 0})
            {
                var artistIds = editSongInput.AuthorIds.Distinct().ToList();
                List<Artist> artists = await DbContext.Artists.Where(_ => artistIds.Contains(_.Id)).ToListAsync();
                if (artists.Count > 0) song.Authors = artists;
            }
            
            var entityEntry = DbContext.Update(song);

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