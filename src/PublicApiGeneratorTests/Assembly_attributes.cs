using System;
using System.Runtime.InteropServices;
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
#if NET452
            var api = @"[assembly: PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
[assembly: PublicApiGeneratorTests.Examples.SimpleAttribute()]
[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]
[assembly: System.Runtime.InteropServices.GuidAttribute(""3B8D506A-5247-47FF-B053-D29A51A97C33"")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute("".NETFramework,Version=v4.5.2"", FrameworkDisplayName="".NET Framework 4.5.2"")]
namespace PublicApiGeneratorTests.Examples
{
    public class NotImportant
    {
        public NotImportant() { }
    }
}";
#endif

#if NETCOREAPP2_1
            var api = @"[assembly: PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
[assembly: PublicApiGeneratorTests.Examples.SimpleAttribute()]
[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]
[assembly: System.Runtime.InteropServices.GuidAttribute(""3B8D506A-5247-47FF-B053-D29A51A97C33"")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute("".NETCoreApp,Version=v2.1"", FrameworkDisplayName="""")]
namespace PublicApiGeneratorTests.Examples
{
    public class NotImportant
    {
        public NotImportant() { }
    }
}";
#endif

            AssertRoslynPublicApi<NotImportant>(api, true);
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class NotImportant {}
    }
    // ReSharper restore ClassNeverInstantiated.Global
}