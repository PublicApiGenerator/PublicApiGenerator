using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Class_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Public_class_is_visible()
        {
            AssertPublicApi<PublicClass>(
@"namespace ApiApproverTests.Examples
{
    public class PublicClass { }
}");
        }

        [Fact]
        public void Internal_class_is_not_visible()
        {
            AssertPublicApi<InternalClass>(string.Empty);
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class PublicClass
        {
        }

        internal class InternalClass
        {
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
}