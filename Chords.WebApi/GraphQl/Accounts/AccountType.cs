using System;
using HotChocolate.Types;
using AccountEntity = Chords.DataAccess.Models.Account;

namespace Chords.WebApi.GraphQl.Accounts
{
    public class AccountType : ObjectType<AccountEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<AccountEntity> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IdType>();
            descriptor.Field(b => b.CreatedBy).Type<StringType>();
            descriptor.Field(b => b.UpdatedBy).Type<StringType>();
            descriptor.Field(b => b.CreatedAt).Type<DateTimeType>();
            descriptor.Field(b => b.UpdatedAt).Type<DateTimeType>();
            descriptor.Field(b => b.Username).Type<StringType>();
            descriptor.Field(b => b.Status).Type<StringType>();
            descriptor.Field(b => b.Email).Type<StringType>();
            descriptor.Field(b => b.Avatar).Type<StringType>();
        }
    }
}