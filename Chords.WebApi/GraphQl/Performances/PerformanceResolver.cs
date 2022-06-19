using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chords.DataAccess.Models;
using Chords.WebApi.GraphQl.Artists;
using Chords.WebApi.GraphQl.Songs;
using HotChocolate;

namespace Chords.WebApi.GraphQl.Performances
{
    public class PerformanceResolver
    {
        public Task<Song> GetSong([Parent] Performance parent, SongBatchDataLoader dataLoader)
        {
            return dataLoader.LoadAsync(parent.SongId);
        }

        // public Task<Artist[]> GetSingers([Parent] Performance parent, ArtistGroupDataLoader dataLoader)
        // {
        //     return dataLoader.LoadAsync(parent.Id);
        // }
    }
}