using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Extension_blocks : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_extension_methods()
        {
            AssertPublicApi(typeof(MyExtensions), """
namespace PublicApiGeneratorTests.Examples
{
    public static class MyExtensions
    {
        extension(int)
        {
            public static string StaticExtensionProperty { get; }
            public static bool StaticExtensionMethod(int value1, float value2) { }
        }
        extension<TElementType>(System.Collections.Generic.IEnumerable<TElementType> genericAnchor)
            where TElementType :  class, System.IComparable
        {
            public bool IsEmpty() { }
        }
        extension<T>(T genericAnchor)
        where T :  notnull
        {
            public bool DoSomething3() { }
        }
        extension<TKey, TValue>(System.Collections.Generic.Dictionary<TKey, TValue> genericAnchor2)
            where TKey :  notnull
            where TValue :  class, System.IComparable
        {
            public bool IsEmpty() { }
        }
        extension<X>(System.Collections.Generic.List<X> l)
            where X :  notnull
        {
            public X Second(X item) { }
        }
        extension(System.DateTime thisDateTimeAnchor)
        {
            public bool DoSomething2(float first, float second) { }
        }
        extension(string thisStringAnchor)
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

            extension<X>(List<X> l)
            {
                public X Second(X item) => l[1];
            }

            extension<TElementType>(IEnumerable<TElementType> genericAnchor) where TElementType : class, IComparable
            {
                public bool IsEmpty() => throw null;
            }

            extension<TKey, TValue>(Dictionary<TKey, TValue> genericAnchor2)
                where TKey : notnull
                where TValue : class, IComparable
            {
                public bool IsEmpty() => throw null;
            }

            extension(int)
            {
                public static string StaticExtensionProperty => "prop";
                public static bool StaticExtensionMethod(int value1, float value2) => true;
            }

            extension<T>(T genericAnchor)
            {
                public bool DoSomething3() => true;
            }
        }
    }
}
