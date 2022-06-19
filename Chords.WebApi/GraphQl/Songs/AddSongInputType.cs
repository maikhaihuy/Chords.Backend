using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Songs
{
    public class AddSongInput
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
    }

    public class AddSongInputType : InputObjectType<AddSongInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddSongInput> descriptor)
        {
            descriptor.Field(b => b.Title).Type<StringType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();

            base.Configure(descriptor);
        }
    }
    
    public class AddSongInputValidator : AbstractValidator<AddSongInput>
    {
        public AddSongInputValidator()
        {
            RuleFor(input => input.Title)
                .NotEmpty();
        }
    }
}