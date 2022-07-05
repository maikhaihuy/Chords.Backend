using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Artists
{
    public class AddArtistInput
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Avatar { get; set; }
        
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }
    }

    public class AddArtistInputType : InputObjectType<AddArtistInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddArtistInput> descriptor)
        {
            descriptor.Field(b => b.Name).Type<StringType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.Avatar).Type<StringType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();
            
            base.Configure(descriptor);
        }
    }

    public class AddArtistInputValidator : AbstractValidator<AddArtistInput>
    {
        public AddArtistInputValidator()
        {
            RuleFor(input => input.Name)
                .NotEmpty();
        }
    }
}