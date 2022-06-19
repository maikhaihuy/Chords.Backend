using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Accounts
{
    public class EditAccountInput
    {
        public string Avatar { get; set; }
        public string Status { get; set; }
    }

    public class EditAccountInputType : InputObjectType<EditAccountInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<EditAccountInput> descriptor)
        {
            descriptor.Field(b => b.Avatar).Type<StringType>();
            descriptor.Field(b => b.Status).Type<StringType>();
        }
    }
}