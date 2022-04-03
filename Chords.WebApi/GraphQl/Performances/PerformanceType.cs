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
            descriptor.Field(b => b.Url).Type<IntType>();
            // public ICollection<Artist> Singers { get; set; }
        }
    }
}