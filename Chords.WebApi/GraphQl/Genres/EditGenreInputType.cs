using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Genres
{
    public class EditGenreInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Total { get; set; }
        public string Description { get; set; }
        
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
    }

    public class EditGenreInputType : InputObjectType<EditGenreInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<EditGenreInput> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IdType>();

            descriptor.Field(b => b.Name).Type<StringType>();
            descriptor.Field(b => b.Total).Type<IntType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();

            base.Configure(descriptor);
        }
    }
}