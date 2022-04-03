using Chords.WebApi.GraphQl.Artists;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl._Actions
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(_ => _.GetArtists(default))
                .Type<ListType<ArtistType>>();
        }
    }
}