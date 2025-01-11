using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Enum : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_not_output_internal_enum()
        {
            AssertPublicApi<InternalEnum>(string.Empty);
        }

        [Fact]
        public void Should_output_enum_names_in_defined_order()
        {
            AssertPublicApi<PublicEnum>("""
namespace PublicApiGeneratorTests.Examples
{
    public enum PublicEnum
    {
        One = 0,
        Two = 1,
        Three = 2,
    }
}
""");
        }

        [Fact]
        public void Should_output_enum_values()
        {
            AssertPublicApi<EnumWithValues>("""
namespace PublicApiGeneratorTests.Examples
{
    public enum EnumWithValues
    {
        One = 100,
        Two = 200,
        Three = 300,
    }
}
""");
        }

        [Fact]
        public void Should_output_enum_base_type()
        {
            AssertPublicApi<EnumWithBaseType>("""
namespace PublicApiGeneratorTests.Examples
{
    public enum EnumWithBaseType : short
    {
        One = 0,
    }
}
""");
        }

        [Fact]
        public void Should_output_enum_attributes()
        {
            AssertPublicApi<EnumWithFlagsAttribute>("""
namespace PublicApiGeneratorTests.Examples
{
    [System.Flags]
    public enum EnumWithFlagsAttribute
    {
        One = 1,
        Two = 2,
        Three = 3,
    }
}
""");
        }

        // TODO: Enum with flags + undefined value
        // Not supported by Cecil?
    }

    namespace Examples
    {
        internal enum InternalEnum
        {
            One
        }

        public enum PublicEnum
        {
            One, Two, Three
        }

        public enum EnumWithValues
        {
            One = 100,
            Two = 200,
            Three = 300
        }

        public enum EnumWithBaseType : short
        {
            One
        }

        [Flags]
        public enum EnumWithFlagsAttribute
        {
            One = 1,
            Two = 2,
            Three = 3
        }
    }
}
