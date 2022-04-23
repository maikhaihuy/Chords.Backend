using System;
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
    public class GenreService : BaseService
    {
        private readonly IMapper _mapper;

        public GenreService(IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IDbContextFactory<ChordsDbContext> dbContextFactory) : base(httpContextAccessor, dbContextFactory)
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

        public async Task<Genre> CreateGenre(AddGenreInput addGenreInput)
        {
            Genre genre = _mapper.Map<Genre>(addGenreInput);
            genre.UpdatedBy = GetCurrentUserId();
            
            var entityEntry = await DbContext.AddAsync(genre);

            await DbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<Genre> UpdateGenre(EditGenreInput editGenreInput)
        {
            Genre genre = _mapper.Map<Genre>(editGenreInput);
            genre.UpdatedBy = GetCurrentUserId();
            
            var entityEntry = DbContext.Update(genre);

            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }

        public async Task<Genre> RemoveGenre(object id)
        {
            Genre genre = new Genre
            {
                Id = $"{id}",
                IsDeleted = true,
                UpdatedBy = GetCurrentUserId()
            };

            var entityEntry = DbContext.Remove(genre);
            
            await DbContext.SaveChangesAsync();
            
            return entityEntry.Entity;
        }
    }
}