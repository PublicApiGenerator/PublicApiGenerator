using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class ConstraintsOrdering : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_Order_Class_First()
        {
            AssertPublicApi(typeof(ClassWithNullableSignatureOfDifferentConstraint),
"""
namespace PublicApiGeneratorTests.Examples
{
    public static class ClassWithNullableSignatureOfDifferentConstraint
    {
        public static T ShouldNotBeNull<T>([System.Diagnostics.CodeAnalysis.NotNull] this T? actual, string? customMessage = null)
            where T :  class { }
        public static T ShouldNotBeNull<T>([System.Diagnostics.CodeAnalysis.NotNull] this T? actual, string? customMessage = null)
            where T :  struct { }
    }
}
""");
        }
    }

    namespace Examples
    {
        public static class ClassWithNullableSignatureOfDifferentConstraint
        {
            public static T ShouldNotBeNull<T>([System.Diagnostics.CodeAnalysis.NotNull] this T? actual, string? customMessage = null)
                where T : struct => throw null;

            public static T ShouldNotBeNull<T>([System.Diagnostics.CodeAnalysis.NotNull] this T? actual, string? customMessage = null)
                where T : class => throw null;
        }
    }
}
