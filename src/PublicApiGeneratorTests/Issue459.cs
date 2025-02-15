namespace PublicApiGeneratorTests;

public sealed class Issue459 : ApiGeneratorTestsBase
{
    [Fact]
    public void Issue459_Should_Work()
    {
        AssertPublicApi(typeof(Issue459Extensions), """
namespace PublicApiGeneratorTests
{
    public static class Issue459Extensions
    {
        public static T[] WhereNotNull1<T>(this T?[] source)
            where T :  struct { }
        public static T?[] WhereNotNull2<T>(this T[] source)
            where T :  struct { }
    }
}
""", opt => opt.IncludeAssemblyAttributes = false);
    }
}

public static class Issue459Extensions
{
    public static T[] WhereNotNull1<T>(this T?[] source)
        where T : struct => throw null;

    public static T?[] WhereNotNull2<T>(this T[] source)
        where T : struct => throw null;
}
