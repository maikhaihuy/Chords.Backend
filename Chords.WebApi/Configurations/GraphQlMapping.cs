using AutoMapper;
using Chords.WebApi.GraphQl.Artists;
using Chords.WebApi.GraphQl.Genres;
using Chords.WebApi.GraphQl.Performances;
using Chords.WebApi.GraphQl.Songs;
using Chords.DataAccess.Models;
using Chords.WebApi.GraphQl.Accounts;

namespace Chords.WebApi.Configurations
{
    public class GraphQlMapping : Profile
    {
        public GraphQlMapping()
        {
            AccountMapping();
            ArtistMapping();
            GenreMapping();
            PerformanceMapping();
            SongMapping();
        }

        public void AccountMapping()
        {
            CreateMap<AddAccountInput, Account>();
            CreateMap<EditAccountInput, Account>();
        }

        public void ArtistMapping()
        {
            CreateMap<AddArtistInput, Artist>();
            CreateMap<EditArtistInput, Artist>();
        }

        public void GenreMapping()
        {
            CreateMap<AddGenreInput, Genre>();
            CreateMap<EditGenreInput, Genre>();
        }

        public void PerformanceMapping()
        {
            CreateMap<AddPerformanceInput, Performance>();
            CreateMap<EditPerformanceInput, Performance>();
        }

        public void SongMapping()
        {
            CreateMap<AddSongInput, Song>();
            CreateMap<EditSongInput, Song>();
        }
    }
}