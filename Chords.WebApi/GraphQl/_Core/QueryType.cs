using Chords.WebApi.GraphQl.Artists;
using Chords.WebApi.GraphQl.Genres;
using Chords.WebApi.GraphQl.Performances;
using Chords.WebApi.GraphQl.Songs;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl._Core
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            
            descriptor.Field(_ => _.GetArtists(default))
                .Type<ListType<ArtistType>>()
                .Authorize();
            
            descriptor.Field(_ => _.GetArtist(default, default))
                .Type<ArtistType>()
                .Authorize();
            
            descriptor.Field(_ => _.GetGenres(default))
                .Type<ListType<GenreType>>()
                .Authorize();
            
            descriptor.Field(_ => _.GetGenre(default, default))
                .Type<GenreType>()
                .Authorize();
            
            descriptor.Field(_ => _.GetPerformances(default))
                .Type<ListType<PerformanceType>>()
                .Authorize();
            
            descriptor.Field(_ => _.GetPerformance(default, default))
                .Type<PerformanceType>()
                .Authorize();
            
            descriptor.Field(_ => _.GetSongs(default))
                .Type<ListType<SongType>>()
                .Authorize();
            
            descriptor.Field(_ => _.GetSong(default, default))
                .Type<SongType>()
                .Authorize();
        }
    }
}