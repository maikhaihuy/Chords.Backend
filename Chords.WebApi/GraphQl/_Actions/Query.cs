using System.Linq;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.WebApi.Common;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;


namespace Chords.WebApi.GraphQl
{
    public class Query 
    {
        #region Artist
        
        [UseDbContext(typeof(ChordsDbContext))]
        [UseOffsetPaging(MaxPageSize = GraphQlConstants.MaxPageSize,
            DefaultPageSize = GraphQlConstants.PageSize,
            IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Artist> GetArtists([ScopedService] ChordsDbContext dbContext)
        {
            return dbContext.Artists;
        }

        [UseDbContext(typeof(ChordsDbContext))]
        public Artist GetArtist([ScopedService] ChordsDbContext dbContext, string id)
        {
            return dbContext.Artists.FirstOrDefault(c => c.Id == id);
        }

        #endregion

        #region Genre
        
        [UseDbContext(typeof(ChordsDbContext))]
        [UseOffsetPaging(MaxPageSize = GraphQlConstants.MaxPageSize,
            DefaultPageSize = GraphQlConstants.PageSize,
            IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Genre> GetGenres([ScopedService] ChordsDbContext dbContext)
        {
            return dbContext.Genres;
        }

        [UseDbContext(typeof(ChordsDbContext))]
        public Genre GetGenre([ScopedService] ChordsDbContext dbContext, string id)
        {
            return dbContext.Genres.FirstOrDefault(c => c.Id == id);
        }

        #endregion

        #region Performance
        
        [UseDbContext(typeof(ChordsDbContext))]
        [UseOffsetPaging(MaxPageSize = GraphQlConstants.MaxPageSize,
            DefaultPageSize = GraphQlConstants.PageSize,
            IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Performance> GetPerformances([ScopedService] ChordsDbContext dbContext)
        {
            return dbContext.Performances;
        }

        [UseDbContext(typeof(ChordsDbContext))]
        public Performance GetPerformance([ScopedService] ChordsDbContext dbContext, string id)
        {
            return dbContext.Performances.FirstOrDefault(c => c.Id == id);
        }

        #endregion

        #region Song
        
        [UseDbContext(typeof(ChordsDbContext))]
        [UseOffsetPaging(MaxPageSize = GraphQlConstants.MaxPageSize,
            DefaultPageSize = GraphQlConstants.PageSize,
            IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Song> GetSongs([ScopedService] ChordsDbContext dbContext)
        {
            return dbContext.Songs;
        }

        [UseDbContext(typeof(ChordsDbContext))]
        public Song GetSong([ScopedService] ChordsDbContext dbContext, string id)
        {
            return dbContext.Songs.FirstOrDefault(c => c.Id == id);
        }

        #endregion
    }
}