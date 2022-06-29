using Chords.WebApi.Common;
using Chords.WebApi.GraphQl.Accounts;
using HotChocolate.Types;
using PerformanceEntity = Chords.DataAccess.Models.Performance;

namespace Chords.WebApi.GraphQl.Performances
{
    public class PerformanceType : ObjectType<PerformanceEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<PerformanceEntity> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IdType>();
            descriptor.Field(b => b.CreatedBy).Type<StringType>();
            descriptor.Field(b => b.UpdatedBy).Type<StringType>();
            descriptor.Field(b => b.CreatedAt).Type<DateTimeType>();
            descriptor.Field(b => b.UpdatedAt).Type<DateTimeType>();

            descriptor.Field(b => b.KeyTone).Type<StringType>();
            descriptor.Field(b => b.Url).Type<StringType>();
            // public ICollection<Artist> Singers { get; set; }
            
            descriptor.Field("song")
                .ResolveWith<PerformanceResolver>(resolver => resolver.GetSong(default, default));
            descriptor.Field(b => b.Singers)
                .ResolveWith<PerformanceResolver>(resolver => resolver.GetSingers(default, default));
            
            descriptor.Field(FieldNameConstants.Creator)
                .ResolveWith<AccountResolver>(resolver => resolver.GetCreator<PerformanceEntity>(default, default));
            descriptor.Field(FieldNameConstants.Updater)
                .ResolveWith<AccountResolver>(resolver => resolver.GetUpdater<PerformanceEntity>(default, default));
        }
    }
}