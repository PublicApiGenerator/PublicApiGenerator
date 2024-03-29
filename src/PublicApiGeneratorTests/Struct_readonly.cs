using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Struct_readonly : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output()
        {
            AssertPublicApi<ReadonlyStruct>(
@"namespace PublicApiGeneratorTests.Examples
{
    public readonly struct ReadonlyStruct { }
}");
        }

        [Fact]
        public void Should_output_with_ctor()
        {
            AssertPublicApi<ReadonlyStructWithCtor>(
@"namespace PublicApiGeneratorTests.Examples
{
    public readonly struct ReadonlyStructWithCtor
    {
        public ReadonlyStructWithCtor(int parameter) { }
    }
}");
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
    }
}
