using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Struct_readonly : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_readonly_struct()
        {
            AssertPublicApi<ReadonlyStruct>("""
namespace PublicApiGeneratorTests.Examples
{
    public readonly struct ReadonlyStruct { }
}
""");
        }

        [Fact]
        public void Should_output_readonly_struct_with_ctor()
        {
            AssertPublicApi<ReadonlyStructWithCtor>("""
namespace PublicApiGeneratorTests.Examples
{
    public readonly struct ReadonlyStructWithCtor
    {
        public ReadonlyStructWithCtor(int parameter) { }
    }
}
""");
        }

        [Fact]
        public void Should_output_readonly_ref_struct()
        {
            AssertPublicApi(typeof(ReadonlyRefStruct), """
namespace PublicApiGeneratorTests.Examples
{
    public readonly ref struct ReadonlyRefStruct { }
}
""");
        }
    }

    namespace Examples
    {
        public readonly struct ReadonlyStruct
        {
        }

        public readonly struct ReadonlyStructWithCtor
        {
            public ReadonlyStructWithCtor(int parameter)
            {
            }
        }

        public readonly ref struct ReadonlyRefStruct
        {
        }
    }
}
