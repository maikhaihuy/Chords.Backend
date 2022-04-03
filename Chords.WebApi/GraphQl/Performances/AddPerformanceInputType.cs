using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Performances
{
    public class AddPerformanceInput
    {
        public string KeyTone { get; set; }
        public string Url { get; set; }
    }
    
    public class AddPerformanceInputType : InputObjectType<AddPerformanceInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddPerformanceInput> descriptor)
        {
            descriptor.Field(b => b.KeyTone).Type<StringType>();
            descriptor.Field(b => b.Url).Type<StringType>();

            base.Configure(descriptor);
        }
    }
}