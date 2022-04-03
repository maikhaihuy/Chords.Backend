
using FluentValidation;
using HotChocolate.Types;

namespace Chords.Web.GraphQl.Auth
{
    public class RegisterInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }

    public class RegisterInputValidator : AbstractValidator<RegisterInput>
    {
        public RegisterInputValidator()
        {
            RuleFor(input => input.Email)
                .NotEmpty()
                .WithMessage("The email is required.");

            RuleFor(input => input.Password)
                .NotEmpty()
                .WithMessage("The password is required.");
            
            RuleFor(input => input.Name)
                .NotEmpty()
                .WithMessage("The name is required.");
        }
    }
    
    public class RegisterInputType : InputObjectType<RegisterInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<RegisterInput> descriptor)
        {
            descriptor.Field(b => b.Email).Type<StringType>();
            descriptor.Field(b => b.Password).Type<StringType>();
            descriptor.Field(b => b.Name).Type<StringType>();
        }
    }
}
