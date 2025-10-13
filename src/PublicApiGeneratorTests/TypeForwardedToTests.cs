#if !NETCOREAPP
using Shouldly;
#endif

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
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SomeClass
    {
        public SomeClass() { }
    }
}
namespace OtherAssembly
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ForwardedClass
    {
        public ForwardedClass() { }
        public string? Name { get; set; }
    }
}
""", new() { IncludeForwardedTypes = true, IncludeAssemblyAttributes = false });
#else
        var ex = Should.Throw<PlatformNotSupportedException>(() => AssertPublicApi(typeof(InitialAssembly.SomeClass).Assembly, "not_used", new() { IncludeForwardedTypes = true, IncludeAssemblyAttributes = false }));
        ex.Message.ShouldBe("IncludeForwardedTypes option works only in .NET Core apps. Please either migrate your app or disable this option.");
#endif
    }
}
