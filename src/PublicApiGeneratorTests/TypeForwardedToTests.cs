using System.Reflection;

namespace PublicApiGeneratorTests;

public class TypeForwardedToTests : ApiGeneratorTestsBase
{
    [Fact]
    public void Should_output_forwarded_types()
    {
#if NETCOREAPP
        AssertPublicApi(typeof(InitialAssembly.SomeClass).Assembly, """
namespace InitialAssembly
{
    public class SomeClass
    {
        public SomeClass() { }
    }
}
namespace OtherAssembly
{
    public class ForwardedClass
    {
        public ForwardedClass() { }
        public string? Name { get; set; }
    }
}
""", new() { IncludeForwardedTypes = true, IncludeAssemblyAttributes = false });
#else
        AssertPublicApi(typeof(InitialAssembly.SomeClass).Assembly, """
namespace InitialAssembly
{
    public class SomeClass
    {
        public SomeClass() { }
    }
}
""", new() { IncludeForwardedTypes = true, IncludeAssemblyAttributes = false });
#endif
    }
}
