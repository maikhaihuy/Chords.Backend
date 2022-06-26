using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chords.DataAccess.Models;
using GreenDonut;

namespace Chords.WebApi.GraphQl.Artists
{
    public class ArtistBatchDataLoader : BatchDataLoader<string, Artist>
    {
        private readonly ArtistService _artistService;
        public ArtistBatchDataLoader(ArtistService artistService, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _artistService = artistService;
        }

        protected override async Task<IReadOnlyDictionary<string, Artist>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var result = await _artistService.GetArtistsByIds(keys);
            return result.ToDictionary(_ => _.Id);
        }
    }

    // public class ArtistGroupDataLoader : GroupedDataLoader<string, Artist>
    // {
    //     private ArtistService _artistService;
    //     public ArtistGroupDataLoader(ArtistService artistService, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    //     {
    //         _artistService = artistService;
    //     }
    //
    //     protected override async Task<ILookup<string, Artist>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
    //     {
    //         var result = await _artistService.GetArtistsByPerformanceIds(keys);
    //         return result.ToLookup(_ => _.Performances.);
    //     }
    // }

    public class AuthorForSongGroupDataLoader : GroupedDataLoader<string, Artist>
    {
        private ArtistService _artistService;
        public AuthorForSongGroupDataLoader(ArtistService artistService, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _artistService = artistService;
        }

        protected override Task<ILookup<string, Artist>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            return _artistService.GetAuthorsBySongIds(keys);
        }
    }
}