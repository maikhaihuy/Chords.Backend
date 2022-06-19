using System.ComponentModel.DataAnnotations;
using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Performances
{
    public class EditPerformanceInput
    {
        [Required]
        public string Id { get; set; } = null!;
        
        public string KeyTone { get; set; }
        public string Url { get; set; }
    }

    public class EditPerformanceInputType : InputObjectType<EditPerformanceInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<EditPerformanceInput> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IdType>();
            descriptor.Field(b => b.KeyTone).Type<StringType>();
            descriptor.Field(b => b.Url).Type<StringType>();

            base.Configure(descriptor);
        }
    }
    
    public class EditPerformanceInputValidator : AbstractValidator<EditPerformanceInput>
    {
        public EditPerformanceInputValidator()
        {
            RuleFor(input => input.Id)
                .NotEmpty();
        }
    }
}