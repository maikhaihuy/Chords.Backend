using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chords.DataAccess.Models;
using GreenDonut;

namespace Chords.WebApi.GraphQl.Genres
{
    public class GenreBatchDataLoader : BatchDataLoader<string, Genre>
    {
        private GenreService _genreService;
        public GenreBatchDataLoader(GenreService genreService, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _genreService = genreService;
        }

        protected override async Task<IReadOnlyDictionary<string, Genre>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var result = await _genreService.GetGenresByIds(keys);
            return result.ToDictionary(_ => _.Id);
        }
    }
}