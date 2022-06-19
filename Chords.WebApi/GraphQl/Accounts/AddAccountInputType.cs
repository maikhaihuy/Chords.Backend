using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Accounts
{
    public class AddAccountInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }

    public class AddAccountInputValidator : AbstractValidator<AddAccountInput>
    {
        public AddAccountInputValidator()
        {
            RuleFor(input => input.Email)
                .NotEmpty()
                .WithMessage("The email is required.");

            RuleFor(input => input.Password)
                .NotEmpty()
                .WithMessage("The password is required.");
            
            RuleFor(input => input.Username)
                .NotEmpty()
                .WithMessage("The Username is required.");
        }
    }
    
    public class AddAccountInputType : InputObjectType<AddAccountInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddAccountInput> descriptor)
        {
            descriptor.Field(b => b.Email).Type<StringType>();
            descriptor.Field(b => b.Password).Type<StringType>();
            descriptor.Field(b => b.Username).Type<StringType>();
        }
    }
}