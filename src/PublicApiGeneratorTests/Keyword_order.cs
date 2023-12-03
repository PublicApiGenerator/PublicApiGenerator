using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Keyword_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Include_static_fields()
        {
            AssertPublicApi<ClassWithStaticFields>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithStaticFields
    {
        protected static string StaticProtectedField;
        public static int StaticPublicField;
        public ClassWithStaticFields() { }
    }
}");
        }

        [Fact]
        public void Include_readonly_fields_without_constant_values()
        {
            // TODO: Initializing values are set in the constructor. Very tricky to get
            AssertPublicApi<ClassWithReadonlyFields>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithReadonlyFields
    {
        protected readonly string ReadonlyProtectedField;
        public readonly int ReadonlyPublicField;
        public ClassWithReadonlyFields() { }
    }
}");
        }

        [Fact]
        public void Include_const_fields()
        {
            // Have to include the ctor - I can't figure out how to hide it
            // when values are initialized
            AssertPublicApi<ClassWithConstFields>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithConstFields
    {
        protected const string ConstProtectedField = ""hello world"";
        public const int ConstPublicField = 42;
        public ClassWithConstFields() { }
    }
}");
        }
    }

    namespace Examples
    {
    }
}
