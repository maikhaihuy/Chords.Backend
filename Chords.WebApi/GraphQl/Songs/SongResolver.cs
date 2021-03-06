using System.Collections.Generic;
using System.Threading.Tasks;
using Chords.DataAccess.Models;
using Chords.WebApi.GraphQl.Artists;
using Chords.WebApi.GraphQl.Genres;
using Chords.WebApi.GraphQl.Performances;
using HotChocolate;

namespace Chords.WebApi.GraphQl.Songs
{
    public class SongResolver
    {
        public Task<Genre> GetGenre([Parent] Song parent, GenreBatchDataLoader dataLoader)
        {
            return dataLoader.LoadAsync(parent.GenreId);
        }

        public Task<Artist[]> GetAuthors([Parent] Song parent, AuthorForSongGroupDataLoader groupDataLoader)
        {
            return groupDataLoader.LoadAsync(parent.Id);
        }
        
        public Task<Performance[]> GetPerformances([Parent] Song parent, PerformanceForSongGroupDataLoader groupDataLoader)
        {
            return groupDataLoader.LoadAsync(parent.Id);
        }
    }
}