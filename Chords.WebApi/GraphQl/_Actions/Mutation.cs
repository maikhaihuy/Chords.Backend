using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chords.DataAccess.EntityFramework;
using Chords.DataAccess.Models;
using Chords.Web.GraphQl.Auth;
using Chords.WebApi.Extensions;
using Chords.WebApi.GraphQl.Artists;
using Chords.WebApi.GraphQl.Auth;
using Chords.WebApi.GraphQl.Genres;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.GraphQl._Actions
{
    public class Mutation
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        
        public Mutation([Service] IHttpContextAccessor contextAccessor,
            IMapper mapper)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

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

        #region Genre

        public Task<Genre> AddGenre(
            AddGenreInput input,
            GenreService genreService
        )
        {
            return genreService.CreateGenre(input);
        }
        
        public Task<Genre> EditGenre(
            EditGenreInput input,
            GenreService genreService
        )
        {
            return genreService.UpdateGenre(input);
        }
        
        public Task<Genre> RemoveGenre(
            string id,
            GenreService genreService
        )
        {
            return genreService.RemoveGenre(id);
        }
        
        #endregion
        
        // #region Artist
        //
        // [UseDbContext(typeof(ChordsDbContext))]
        // public async Task<Artist> AddArtist(
        //     AddArtistInput input,
        //     [ScopedService] ChordsDbContext context,
        //     CancellationToken cancellationToken
        // )
        // {
        //     Artist artist = _mapper.Map<Artist>(input);
        //         
        //     artist.UpdatedBy = GetCurrentUser();
        //     
        //     var artistEntry = context.Artists.Add(artist);
        //     
        //     await context.SaveChangesAsync(cancellationToken);
        //     return artistEntry.Entity;
        // }
        //
        // [UseDbContext(typeof(ChordsDbContext))]
        // public async Task<Artist> EditArtist(
        //     EditArtistInput input,
        //     [ScopedService] ChordsDbContext context,
        //     CancellationToken cancellationToken
        // )
        // {
        //     Artist artist = _mapper.Map<Artist>(input);
        //     
        //     artist.UpdatedBy = GetCurrentUser();
        //     
        //     var artistEntry = context.Artists.Update(artist);
	       //
        //     await context.SaveChangesAsync(cancellationToken);
        //     return artistEntry.Entity;
        // }
        //
        // [UseDbContext(typeof(ChordsDbContext))]
        // public async Task<Artist> RemoveArtist(
        //     string id,
        //     [ScopedService] ChordsDbContext context,
        //     CancellationToken cancellationToken
        // )
        // {
        //     Artist artist = new Artist
        //     {
        //         Id = id,
        //         UpdatedBy = GetCurrentUser()
        //     };
        //     
        //     var artistEntry = context.Remove(artist);
        //
        //     await context.SaveChangesAsync(cancellationToken);
        //     return artistEntry.Entity;
        // }
        //
        // #endregion
    }
}