using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class AliasOrdering : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_Order_Class_First()
        {
            AssertPublicApi(typeof(ClassWithAliasedTypes),
"""
namespace PublicApiGeneratorTests.Examples
{
    public static class ClassWithAliasedTypes
    {
        public static void ShouldBeNegative(this float actual, string? customMessage = null) { }
        public static void ShouldBeNegative(this short actual, string? customMessage = null) { }
    }
}
""");
        }
    }

    namespace Examples
    {
        public static class ClassWithAliasedTypes
        {
            public static void ShouldBeNegative(this short actual, string? customMessage = null) { }
            public static void ShouldBeNegative(this float actual, string? customMessage = null) { }
        }
    }
}
