using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Artists
{
    public class EditArtistInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
    }

    public class EditArtistInputType : InputObjectType<EditArtistInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<EditArtistInput> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IntType>();
            descriptor.Field(b => b.Name).Type<StringType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.Avatar).Type<StringType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();
            
            base.Configure(descriptor);
        }
    }
}