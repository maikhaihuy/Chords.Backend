using System.Linq;
using System.Threading.Tasks;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.WebApi.Common;
using Chords.WebApi.GraphQl.Artists;
using Chords.WebApi.GraphQl.Genres;
using Chords.WebApi.GraphQl.Performances;
using Chords.WebApi.GraphQl.Songs;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;


namespace Chords.WebApi.GraphQl._Core
{
    public class Query 
    {
        #region Artist

        [UseOffsetPaging(MaxPageSize = GraphQlConstants.MaxPageSize,
            DefaultPageSize = GraphQlConstants.PageSize,
            IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public Task<IQueryable<Artist>> GetArtists(ArtistService artistService) => artistService.GetArtists();
        
        public Task<Artist> GetArtist(ArtistService artistService, string id) => artistService.GetArtist(id);

        #endregion

        #region Genre

        [UseOffsetPaging(MaxPageSize = GraphQlConstants.MaxPageSize,
            DefaultPageSize = GraphQlConstants.PageSize,
            IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public Task<IQueryable<Genre>> GetGenres(GenreService genreService) => genreService.GetGenres();

        public Task<Genre> GetGenre(GenreService genreService, string id) => genreService.GetGenre(id);

        #endregion

        #region Performance

        [UseOffsetPaging(MaxPageSize = GraphQlConstants.MaxPageSize,
            DefaultPageSize = GraphQlConstants.PageSize,
            IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public Task<IQueryable<Performance>> GetPerformances(PerformanceService performanceService) => performanceService.GetPerformances();

        public Task<Performance> GetPerformance(PerformanceService performanceService, string id) => performanceService.GetPerformance(id);
        
        #endregion
        
        #region Song

        [UseOffsetPaging(MaxPageSize = GraphQlConstants.MaxPageSize,
            DefaultPageSize = GraphQlConstants.PageSize,
            IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public Task<IQueryable<Song>> GetSongs(SongService songService) => songService.GetSongs();

        public Task<Song> GetSong(SongService songService, string id) => songService.GetSong(id);

        #endregion
    }
}