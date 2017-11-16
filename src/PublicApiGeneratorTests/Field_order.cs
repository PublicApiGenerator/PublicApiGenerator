using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Field_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_fields_in_alphabetical_order()
        {
            AssertPublicApi<FieldOrderExample>(
@"namespace ApiApproverTests.Examples
{
    public class FieldOrderExample
    {
        public int AA_Field;
        public string YY_Field;
        public int ZZ_Field;
        public FieldOrderExample() { }
    }
}");
        }
    }

    // ReSharper disable InconsistentNaming
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public class FieldOrderExample
        {
            public int ZZ_Field;
            public string YY_Field;
            public int AA_Field;
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore InconsistentNaming
}