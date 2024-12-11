using PublicApiGenerator;

namespace PublicApiGeneratorTests;

public class ExcludeTypes : ApiGeneratorTestsBase
{
    [Fact]
    public void Should_exclude_types()
    {
        AssertPublicApi(typeof(ApiGeneratorOptions).Assembly,
@"namespace PublicApiGenerator
{
    public static class ApiGenerator
    {
        public static string GeneratePublicApi(this System.Reflection.Assembly assembly, PublicApiGenerator.ApiGeneratorOptions? options = null) { }
        public static string GeneratePublicApi(this System.Type type, PublicApiGenerator.ApiGeneratorOptions? options = null) { }
        public static string GeneratePublicApi(this System.Type[] types, PublicApiGenerator.ApiGeneratorOptions? options = null) { }
    }
}", new() { IncludeAssemblyAttributes = false, ExcludeTypes = [typeof(ApiGeneratorOptions)] });
    }
}
