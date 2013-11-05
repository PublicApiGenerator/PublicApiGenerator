using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Field_flags : ApiGeneratorTestsBase
    {
        [Fact]
        public void Include_static_fields()
        {
            AssertPublicApi<ClassWithStaticFields>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithStaticFields
    {
        protected static string StaticProtectedField;
        public static int StaticPublicField;
    }
}");
        }

        [Fact(Skip = "Requires constants")]
        [Trait("TODO", "Constant values")]
        public void Include_readonly_fields()
        {
            // Have to include the ctor - I can't figure out how to hide it
            // when values are initialised
            AssertPublicApi<ClassWithReadonlyFields>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithReadonlyFields
    {
        protected readonly string ReadonlyProtectedField" /* = ""hello world"" */ + @";
        public readonly int ReadonlyPublicField" /* = 42 */ + @";
        public ClassWithReadonlyFields() { }
    }
}");
        }

        [Fact(Skip = "Requires constants")]
        [Trait("TODO", "Constant values")]
        public void Include_const_fields()
        {
            // Have to include the ctor - I can't figure out how to hide it
            // when values are initialised
            AssertPublicApi<ClassWithConstFields>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithConstFields
    {
        public const int ConstPublicField = 42;
        protected const string ConstProtectedField  = ""hello world"";
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
            protected const string ReadonlyProtectedField = "hello world";
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}