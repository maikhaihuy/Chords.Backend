using AutoMapper;
using Chords.WebApi.GraphQl.Artists;
using Chords.WebApi.GraphQl.Genres;
using Chords.WebApi.GraphQl.Performances;
using Chords.WebApi.GraphQl.Songs;
using Chords.DataAccess.Models;

namespace Chords.WebApi.Configurations
{
    public class GraphQlMapping : Profile
    {
        public GraphQlMapping()
        {
            ArtistMapping();
            GenreMapping();
            PerformanceMapping();
            SongMapping();
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