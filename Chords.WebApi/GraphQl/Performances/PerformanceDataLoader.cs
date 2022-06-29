using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chords.DataAccess.Models;
using GreenDonut;

namespace Chords.WebApi.GraphQl.Performances
{
    public class PerformanceDataLoader
    {
        
    }
    
    public class PerformanceForSongGroupDataLoader : GroupedDataLoader<string, Performance>
    {
        private PerformanceService _performanceService;
        public PerformanceForSongGroupDataLoader(PerformanceService performanceService, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _performanceService = performanceService;
        }

        protected override Task<ILookup<string, Performance>> LoadGroupedBatchAsync(IReadOnlyList<string> keys, CancellationToken cancellationToken)
        {
            return _performanceService.GetPerformancesBySongIds(keys);
        }
    }
}