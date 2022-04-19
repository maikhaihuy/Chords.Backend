using HotChocolate.Types;

namespace Chords.WebApi.GraphQl._Actions
{
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            
            descriptor.Field(_ => _.Login(default, default, default));
            descriptor.Field(_ => _.Register(default, default, default));

            descriptor.Field(_ => _.AddGenre(default, default))
                .Authorize();
            descriptor.Field(_ => _.EditGenre(default, default))
                .Authorize();
            descriptor.Field(_ => _.RemoveGenre(default, default))
                .Authorize();
        }
    }
}