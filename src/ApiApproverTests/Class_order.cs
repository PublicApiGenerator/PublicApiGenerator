using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Class_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_classes_in_alphabetical_order()
        {
            var assemblyDefinition = FixtureData.GetAssemblyDefinitionForTypes(typeof(BB_Class), typeof(AA_Class));
            AssertPublicApi(assemblyDefinition,
@"namespace ApiApproverTests.Examples
{
    public class AA_Class { }
}
namespace ApiApproverTests.Examples
{
    public class BB_Class { }
}");
        }
    }

    // ReSharper disable InconsistentNaming
    namespace Examples
    {
        public class BB_Class
        {
        }

        public class AA_Class
        {
        }
    }
    // ReSharper restore InconsistentNaming
}