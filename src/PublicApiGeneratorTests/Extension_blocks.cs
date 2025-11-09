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
        public static bool IsEmpty<T>(this System.Collections.Generic.IEnumerable<T> target)
            where T :  notnull { }
        public static bool StaticExtensionMethod(this string value) { }
        public static int WordCount(this string str) { }
        public static int get_LineCount(this string str) { }
        public static string get_StaticExtensionProperty() { }
    }
}
""");
        }
    }

    namespace Examples
    {
        public static class MyExtensions
        {
            extension(string str)
            {
                // instance extension property
                public int LineCount => str.Count(c => c == '\n');
                // instance extension method
                public int WordCount() => str.Split([' ', '.', '?'], StringSplitOptions.RemoveEmptyEntries).Length;
            }

            extension<T>(IEnumerable<T> target)
            {
                public bool IsEmpty() => !target.Any();
            }

            extension(string)
            {
                public static string StaticExtensionProperty => "prop";
                public static bool StaticExtensionMethod(string value) => true;
            }
        }
    }
}
