using Chords.WebApi.Common;
using Chords.WebApi.GraphQl.Accounts;
using Chords.WebApi.GraphQl.Genres;
using HotChocolate.Types;
using SongEntity = Chords.DataAccess.Models.Song;

namespace Chords.WebApi.GraphQl.Songs
{
    public class SongType : ObjectType<SongEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<SongEntity> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IdType>();
            descriptor.Field(b => b.CreatedBy).Type<StringType>();
            descriptor.Field(b => b.UpdatedBy).Type<StringType>();
            descriptor.Field(b => b.CreatedAt).Type<DateTimeType>();
            descriptor.Field(b => b.UpdatedAt).Type<DateTimeType>();

            descriptor.Field(b => b.Title).Type<StringType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.Slug).Type<StringType>();
            descriptor.Field(b => b.Status).Type<StringType>();
            descriptor.Field(b => b.Views).Type<LongType>();
            descriptor.Field(b => b.Rating).Type<IntType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();
            // public ICollection<Artist> Authors { get; set; }
            // public ICollection<Performance> Performances { get; set; }
            // public Genre Genre { get; set; }
            descriptor.Field(b => b.Genre)
                .ResolveWith<SongResolver>(resolver => resolver.GetGenre(default, default))
                .Type<GenreType>();
            descriptor.Field(FieldNameConstants.Creator)
                .ResolveWith<AccountResolver>(resolver => resolver.GetCreator<SongEntity>(default, default));
            descriptor.Field(FieldNameConstants.Updater)
                .ResolveWith<AccountResolver>(resolver => resolver.GetUpdater<SongEntity>(default, default));
        }
    }
}