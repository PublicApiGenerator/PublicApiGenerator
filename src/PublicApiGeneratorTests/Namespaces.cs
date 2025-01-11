using PublicApiGeneratorTests.Examples1;
using PublicApiGeneratorTests.Examples2;

namespace PublicApiGeneratorTests
{
    public class Namespaces : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_declare_one_namespace_for_multiple_classes()
        {
            AssertPublicApi([typeof(Simple1), typeof(Simple2)], """
namespace PublicApiGeneratorTests.Examples1
{
    public class Simple1
    {
        public Simple1() { }
    }
    public class Simple2
    {
        public Simple2() { }
    }
}
""");
        }

        [Fact]
        public void Should_declare_new_namespace_for_classes_in_different_namespaces()
        {
            AssertPublicApi([typeof(Simple1), typeof(OtherSimple1)], """
namespace PublicApiGeneratorTests.Examples1
{
    public class Simple1
    {
        public Simple1() { }
    }
}
namespace PublicApiGeneratorTests.Examples2
{
    public class OtherSimple1
    {
        public OtherSimple1() { }
    }
}
""");
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
