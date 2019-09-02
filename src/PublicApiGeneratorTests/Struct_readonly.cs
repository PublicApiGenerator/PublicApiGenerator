using System;
using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Struct_readonly : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output()
        {
            AssertRoslynPublicApi<ReadonlyStruct>(
@"namespace PublicApiGeneratorTests.Examples
{
    public readonly struct ReadonlyStruct { }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public readonly struct ReadonlyStruct
        {
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
}