using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Extension_blocks : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_extension_methods()
        {
            // Note the class static reverse order hack
            AssertPublicApi(typeof(MyExtensions), """
namespace PublicApiGeneratorTests.Examples
{
    public static class MyExtensions
    {
        public static double DoSomething(this string thisStringAnchor, bool arg) { }
        public static bool DoSomething2(this System.DateTime thisDateTimeAnchor, float first, float second) { }
        public static bool IsEmpty<T>(this System.Collections.Generic.IEnumerable<T> genericAnchor)
            where T :  notnull { }
        public static bool StaticExtensionMethod(int value1, float value2) { }
        public static int get_LineCount(string thisStringAnchor) { }
        public static string get_StaticExtensionProperty() { }
        extension(System.Int32)
        {
            public static string StaticExtensionProperty { get; }
            public static bool StaticExtensionMethod(int value1, float value2) { }
        }
        extension(System.Collections.Generic.IEnumerable`1<T> genericAnchor)
        {
            public bool IsEmpty() { }
        }
        extension(System.DateTime thisDateTimeAnchor)
        {
            public bool DoSomething2(float first, float second) { }
        }
        extension(System.String thisStringAnchor)
        {
            public int LineCount { get; }
            public double DoSomething(bool arg) { }
        }
    }
}
""");
        }
    }

    namespace Examples
    {
        public static class MyExtensions
        {
            extension(string thisStringAnchor)
            {
                // instance extension property
                public int LineCount => 0;
                // instance extension method
                public double DoSomething(bool arg) => throw null;
            }

            extension(DateTime thisDateTimeAnchor)
            {
                // instance extension method
                public bool DoSomething2(float first, float second) => throw null;
            }

            extension<T>(IEnumerable<T> genericAnchor)
            {
                public bool IsEmpty() => throw null;
            }

            extension(int)
            {
                public static string StaticExtensionProperty => "prop";
                public static bool StaticExtensionMethod(int value1, float value2) => true;
            }
        }
    }
}
