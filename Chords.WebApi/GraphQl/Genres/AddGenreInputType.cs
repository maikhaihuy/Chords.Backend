using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Genres
{
    public class AddGenreInput
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
    }

    public class AddGenreInputType: InputObjectType<AddGenreInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddGenreInput> descriptor)
        {
            descriptor.Field(b => b.Name).Type<StringType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();

            base.Configure(descriptor);
        }
    }
    
    public class AddGenreInputValidator : AbstractValidator<AddGenreInput>
    {
        public AddGenreInputValidator()
        {
            RuleFor(input => input.Name)
                .NotEmpty();
        }
    }
}