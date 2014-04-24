using ApiApproverTests.Examples1;
using ApiApproverTests.Examples2;
using Xunit;

namespace ApiApproverTests
{
    public class Namespaces : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_declare_one_namespace_for_multiple_classes()
        {
            AssertPublicApi(new[] {typeof(Simple1), typeof(Simple2)},
@"namespace ApiApproverTests.Examples1
{
    public class Simple1 { }
    public class Simple2 { }
}");
        }

        [Fact]
        public void Should_declare_new_namespace_for_classes_in_different_namespaces()
        {
            AssertPublicApi(new[]{typeof(Simple1), typeof(OtherSimple1)},
@"namespace ApiApproverTests.Examples1
{
    public class Simple1 { }
}
namespace ApiApproverTests.Examples2
{
    public class OtherSimple1 { }
}");
        }
    }

    namespace Examples1
    {
        public class Simple1
        {
        }

        public class Simple2
        {
        }
    }

    namespace Examples2
    {
        public class OtherSimple1
        {
        }
    }
}