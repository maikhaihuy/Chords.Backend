using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Performances
{
    public class EditPerformanceInput
    {
        public string Id { get; set; }
        
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
}