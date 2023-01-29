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
    public class Assembly_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Attributes()
        {
#if NET7_0
            const string TFM = ".NETCoreApp,Version=v7.0";
            const string TFMNAME = ".NET 7.0";
#elif NET6_0
            const string TFM = ".NETCoreApp,Version=v6.0";
            const string TFMNAME = ".NET 6.0";
#else
            const string TFM = ".NETCoreApp,Version=v3.1";
            const string TFMNAME = ".NET Core 3.1";
#endif
            var api = @$"[assembly: PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
[assembly: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
[assembly: PublicApiGeneratorTests.Examples.Simple]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Runtime.InteropServices.Guid(""3B8D506A-5247-47FF-B053-D29A51A97C33"")]
[assembly: System.Runtime.Versioning.TargetFramework(""{TFM}"", FrameworkDisplayName=""{TFMNAME}"")]
namespace PublicApiGeneratorTests.Examples
{{
    public class NotImportant
    {{
        public NotImportant() {{ }}
    }}
}}";

            AssertPublicApi<NotImportant>(api, new ApiGeneratorOptions { IncludeAssemblyAttributes = true });
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class NotImportant { }
    }
    // ReSharper restore ClassNeverInstantiated.Global
}
