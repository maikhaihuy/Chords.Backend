using System.Threading.Tasks;
using Chords.DataAccess.Models;
using Chords.WebApi.GraphQl.Genres;
using HotChocolate;

namespace Chords.WebApi.GraphQl.Songs
{
    public class SongResolver
    {
        public Task<Genre> GetGenre([Parent] Song parent, GenreBatchDataLoader dataLoader)
        {
            return dataLoader.LoadAsync(parent.GenreId);
        }
    }
}