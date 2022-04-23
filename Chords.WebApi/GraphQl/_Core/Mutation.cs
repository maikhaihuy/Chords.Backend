using System.Threading;
using System.Threading.Tasks;
using Chords.DataAccess.Models;
using Chords.Web.GraphQl.Auth;
using Chords.WebApi.GraphQl.Artists;
using Chords.WebApi.GraphQl.Auth;
using Chords.WebApi.GraphQl.Genres;
using Chords.WebApi.GraphQl.Performances;
using Chords.WebApi.GraphQl.Songs;

namespace Chords.WebApi.GraphQl._Core
{
    public class Mutation
    {
        #region Auth
        
        public async Task<Token> Login(
            LoginInput input,
            AuthService authService,
            CancellationToken cancellationToken)
        {
            Token token = await authService.Login(input);
            return token;
        }
        
        public async Task<Token> Register(
            RegisterInput input,
            AuthService authService,
            CancellationToken cancellationToken)
        {
            Token token = await authService.Register(input);
            return token;
        }
        
        #endregion

        #region Artist

        public Task<Artist> AddArtist(
            AddArtistInput input,
            ArtistService artistService,
            CancellationToken cancellationToken
        ) => artistService.CreateArtist(input);

        public Task<Artist> EditArtist(
            EditArtistInput input,
            ArtistService artistService,
            CancellationToken cancellationToken
        ) => artistService.UpdateArtist(input);

        public Task<Artist> RemoveArtist(
            string id,
            ArtistService artistService,
            CancellationToken cancellationToken
        ) => artistService.RemoveArtist(id);

        #endregion
        
        #region Genre

        public Task<Genre> AddGenre(
            AddGenreInput input,
            GenreService genreService,
            CancellationToken cancellationToken
        ) => genreService.CreateGenre(input);

        public Task<Genre> EditGenre(
            EditGenreInput input,
            GenreService genreService,
            CancellationToken cancellationToken
        ) => genreService.UpdateGenre(input);
        
        public Task<Genre> RemoveGenre(
            string id,
            GenreService genreService,
            CancellationToken cancellationToken
        ) => genreService.RemoveGenre(id);
        
        #endregion
        
        #region Performances

        public Task<Performance> AddPerformance(
            AddPerformanceInput input,
            PerformanceService artistService,
            CancellationToken cancellationToken
        ) => artistService.CreatePerformance(input);

        public Task<Performance> EditPerformance(
            EditPerformanceInput input,
            PerformanceService artistService,
            CancellationToken cancellationToken
        ) => artistService.UpdatePerformance(input);

        public Task<Performance> RemovePerformance(
            string id,
            PerformanceService artistService,
            CancellationToken cancellationToken
        ) => artistService.RemovePerformance(id);

        #endregion
        
        #region Songs

        public Task<Song> AddSong(
            AddSongInput input,
            SongService artistService,
            CancellationToken cancellationToken
        ) => artistService.CreateSong(input);

        public Task<Song> EditSong(
            EditSongInput input,
            SongService artistService,
            CancellationToken cancellationToken
        ) => artistService.UpdateSong(input);

        public Task<Song> RemoveSong(
            string id,
            SongService artistService,
            CancellationToken cancellationToken
        ) => artistService.RemoveSong(id);

        #endregion
    }
}