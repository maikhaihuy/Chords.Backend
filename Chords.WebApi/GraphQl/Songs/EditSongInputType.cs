using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Songs
{
    public class EditSongInput
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
        public ulong Views { get; set; }
        public int Rating { get; set; }
        
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
    }

    public class EditSongInputType : InputObjectType<EditSongInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<EditSongInput> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IdType>();

            descriptor.Field(b => b.Title).Type<StringType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.Slug).Type<StringType>();
            descriptor.Field(b => b.Status).Type<StringType>();
            descriptor.Field(b => b.Views).Type<LongType>();
            descriptor.Field(b => b.Rating).Type<IntType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();

            base.Configure(descriptor);
        }
    }
}