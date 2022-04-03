using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Auth
{
    public class LoginInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginInputValidator : AbstractValidator<LoginInput>
    {
        public LoginInputValidator()
        {
            RuleFor(input => input.Email)
                .NotEmpty()
                .WithMessage("The email is required.");

            RuleFor(input => input.Password)
                .NotEmpty()
                .WithMessage("The password is required.");
        }
    }

    public class LoginInputType : InputObjectType<LoginInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<LoginInput> descriptor)
        {
            descriptor.Field(b => b.Email).Type<StringType>();
            descriptor.Field(b => b.Password).Type<StringType>();
        }
    }

}