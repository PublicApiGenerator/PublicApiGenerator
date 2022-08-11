using System;
using System.Runtime.InteropServices;
using PublicApiGenerator;
using PublicApiGeneratorTests.Examples;
using Xunit;

[assembly: Guid("3B8D506A-5247-47FF-B053-D29A51A97C33")]
[assembly: Simple]
[assembly: AttributeWithPositionalParameters1("Hello")]
[assembly: AttributeWithPositionalParameters2(42)]
[assembly: AttributeWithMultiplePositionalParameters(42, "Hello")]
[assembly: AttributeWithNamedParameterAttribute(StringValue = "Hello", IntValue = 42)]
[assembly: ComVisibleAttribute(false)]

namespace PublicApiGeneratorTests
{
    using NotInTestAssembly = Object;

    public class Assembly_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Attributes()
        {
#if NET6_0
            var api = @"[assembly: PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
[assembly: PublicApiGeneratorTests.Examples.Simple]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Runtime.InteropServices.Guid(""3B8D506A-5247-47FF-B053-D29A51A97C33"")]
[assembly: System.Runtime.Versioning.TargetFramework("".NETCoreApp,Version=v6.0"", FrameworkDisplayName="""")]
namespace PublicApiGeneratorTests.Examples
{
    public class NotImportant
    {
        public NotImportant() { }
    }
}";
#else
            var api = @"[assembly: PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
[assembly: PublicApiGeneratorTests.Examples.Simple]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Runtime.InteropServices.Guid(""3B8D506A-5247-47FF-B053-D29A51A97C33"")]
[assembly: System.Runtime.Versioning.TargetFramework("".NETCoreApp,Version=v3.1"", FrameworkDisplayName="""")]
namespace PublicApiGeneratorTests.Examples
{
    public class NotImportant
    {
        public NotImportant() { }
    }
}";
#endif

            AssertPublicApi<NotImportant>(api, new ApiGeneratorOptions { IncludeAssemblyAttributes = true });
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class NotImportant {}
    }
    // ReSharper restore ClassNeverInstantiated.Global
}
