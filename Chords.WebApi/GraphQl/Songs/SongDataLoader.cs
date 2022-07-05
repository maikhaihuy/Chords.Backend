using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chords.DataAccess.Models;
using GreenDonut;

namespace Chords.WebApi.GraphQl.Songs
{
    public class SongBatchDataLoader : BatchDataLoader<string, Song>
    {
        private readonly SongService _songService;
        public SongBatchDataLoader(SongService songService, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _songService = songService;
        }

        protected override async Task<IReadOnlyDictionary<string, Song>> LoadBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            var result = await _songService.GetSongByIds(keys);
            return result.ToDictionary(_ => _.Id);
        }
    }
    
    // public class AuthorForSongGroupDataLoader : GroupedDataLoader<string, Artist>
    // {
    //     private SongService _songService;
    //     public AuthorForSongGroupDataLoader(SongService songService, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
    //     {
    //         _songService = songService;
    //     }
    //
    //     protected override async Task<ILookup<string, Artist>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
    //     {
    //         var songs = await _songService.GetAuthorsByIds(keys);
    //         
    //     }
    // }
}