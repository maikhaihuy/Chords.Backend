using System.ComponentModel.DataAnnotations;
using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Songs
{
    public class EditSongInput
    {
        [Required]
        public string Id { get; set; } = null!;
        public string Title { get; set; }
        public string Description { get; set; }
        
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
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();

            base.Configure(descriptor);
        }
    }
    
    public class EditSongInputValidator : AbstractValidator<EditSongInput>
    {
        public EditSongInputValidator()
        {
            RuleFor(input => input.Id)
                .NotEmpty();
        }
    }
}