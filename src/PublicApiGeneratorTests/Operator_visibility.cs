using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Operator_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_show_custom_operators()
        {
            AssertPublicApi<ClassWithOperator>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithOperator
    {
        public ClassWithOperator() { }
        public static PublicApiGeneratorTests.Examples.ClassWithOperator operator +(PublicApiGeneratorTests.Examples.ClassWithOperator first, PublicApiGeneratorTests.Examples.ClassWithOperator second) { }
    }
}
""");
        }
    }

    namespace Examples
    {
        public class ClassWithOperator
        {
            public static ClassWithOperator operator +(ClassWithOperator first, ClassWithOperator second) => second;
        }
    }
}
