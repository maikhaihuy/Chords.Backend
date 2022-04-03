using HotChocolate.Types;
using ArtistEntity = Chords.DataAccess.Models.Artist;

namespace Chords.WebApi.GraphQl.Artists
{
    public class ArtistType : ObjectType<ArtistEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<ArtistEntity> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IdType>();
            descriptor.Field(b => b.CreatedBy).Type<StringType>();
            descriptor.Field(b => b.UpdatedBy).Type<StringType>();
            descriptor.Field(b => b.CreatedAt).Type<DateTimeType>();
            descriptor.Field(b => b.UpdatedAt).Type<DateTimeType>();

            descriptor.Field(b => b.Name).Type<StringType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.Avatar).Type<StringType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();
            
            // public ICollection<Song> Songs { get; set; }
            // public ICollection<Performance> Performances { get; set; }
        }
    }
}