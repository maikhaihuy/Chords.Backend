using HotChocolate.Types;

namespace Chords.WebApi.GraphQl._Core
{
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            
            descriptor.Field(_ => _.Login(default, default, default));
            descriptor.Field(_ => _.Register(default, default, default));

            descriptor.Field(_ => _.AddAccount(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.EditAccount(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.RemoveAccount(default, default, default))
                .Authorize()
                ;
            
            descriptor.Field(_ => _.AddArtist(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.EditArtist(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.RemoveArtist(default, default, default))
                .Authorize()
                ;
            
            descriptor.Field(_ => _.AddGenre(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.EditGenre(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.RemoveGenre(default, default, default))
                .Authorize()
                ;
            
            descriptor.Field(_ => _.AddPerformance(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.EditPerformance(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.RemovePerformance(default, default, default))
                .Authorize()
                ;
            
            descriptor.Field(_ => _.AddSong(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.EditSong(default, default, default))
                .Authorize()
                ;
            descriptor.Field(_ => _.RemoveSong(default, default, default))
                .Authorize()
                ;
        }
    }
}