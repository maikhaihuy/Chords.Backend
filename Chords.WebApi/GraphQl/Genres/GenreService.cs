using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.GraphQl.Genres
{
    public class GenreService : BaseService<Genre>
    {
        private readonly IMapper _mapper;

        public GenreService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(mapper, httpContextAccessor, dbContextFactory)
        {
            _mapper = mapper;
        }
        
        public Task<IQueryable<Genre>> GetGenres()
        {
            return Task.FromResult(DbContext.Genres.AsQueryable());
        }
        
        public Task<Genre> GetGenre(object id)
        {
            return Task.FromResult(DbContext.Genres.Find(id));
        }

        public Task<IQueryable<Genre>> GetGenresByIds(IReadOnlyList<object> ids)
        {
            return Task.FromResult(DbContext.Genres.Where(_ => ids.Contains(_.Id)));
        }
        
        public async Task<Genre> CreateGenre(AddGenreInput addGenreInput)
        {
            Genre genre = await PreCreate(addGenreInput);
            
            var entityEntry = await DbContext.AddAsync(genre);

            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Genre> UpdateGenre(EditGenreInput editGenreInput)
        {
            Genre genre = await PreUpdate(editGenreInput);
            
            var entityEntry = DbContext.Update(genre);

            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }

        public async Task<Genre> RemoveGenre(object id)
        {
            Genre genre = await PreRemove(id);

            var entityEntry = DbContext.Remove(genre);
            
            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }
    }
}