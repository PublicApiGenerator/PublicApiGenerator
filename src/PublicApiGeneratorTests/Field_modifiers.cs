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
        public void Include_Volatile_field_Without_modreq()
        {
            AssertPublicApi<ClassWithVolatileField>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithVolatileField
    {
        public static int StaticVolatilePublicField;
        public ClassWithVolatileField() { }
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

        [Fact]
        public void Include_unsafe_fields()
        {
            // Have to include the ctor - I can't figure out how to hide it
            // when values are initialized
            AssertPublicApi<ClassWithUnsafeFields>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithUnsafeFields
    {
        protected unsafe void* UnsafeProtectedField;
        public unsafe void* UnsafePublicField;
        public ClassWithUnsafeFields() { }
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

        public class ClassWithVolatileField
        {
            public static volatile int StaticVolatilePublicField;
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

        public class ClassWithUnsafeFields
        {
            public unsafe void* UnsafePublicField;
            protected unsafe void* UnsafeProtectedField;
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}
