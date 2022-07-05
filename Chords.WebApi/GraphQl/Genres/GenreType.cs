using Chords.DataAccess.Models;
using Chords.WebApi.Common;
using Chords.WebApi.GraphQl.Accounts;
using HotChocolate.Types;
using GenreEntity = Chords.DataAccess.Models.Genre;

namespace Chords.WebApi.GraphQl.Genres
{
    public class GenreType : ObjectType<GenreEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<GenreEntity> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IdType>();
            descriptor.Field(b => b.CreatedBy).Type<StringType>();
            descriptor.Field(b => b.UpdatedBy).Type<StringType>();
            descriptor.Field(b => b.CreatedAt).Type<DateTimeType>();
            descriptor.Field(b => b.UpdatedAt).Type<DateTimeType>();

            descriptor.Field(b => b.Name).Type<StringType>();
            descriptor.Field(b => b.Total).Type<IntType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();
        
            // public ICollection<Song> Songs { get; set; }
            descriptor.Field(FieldNameConstants.Creator)
                .ResolveWith<AccountResolver>(resolver => resolver.GetCreator<GenreEntity>(default, default));
            descriptor.Field(FieldNameConstants.Updater)
                .ResolveWith<AccountResolver>(resolver => resolver.GetUpdater<GenreEntity>(default, default));
        }
    }
}