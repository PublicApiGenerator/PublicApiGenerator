using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Field_modifiers : ApiGeneratorTestsBase
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
            // TODO: Initialising values are set in the constructor. Very tricky to get
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
            // when values are initialised
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

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public class ClassWithStaticFields
        {
            public static int StaticPublicField;
            protected static string StaticProtectedField;
        }

        public class ClassWithReadonlyFields
        {
            public readonly int ReadonlyPublicField = 42;
            protected readonly string ReadonlyProtectedField = "hello world";
        }

        public class ClassWithConstFields
        {
            public const int ConstPublicField = 42;
            protected const string ConstProtectedField = "hello world";
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}