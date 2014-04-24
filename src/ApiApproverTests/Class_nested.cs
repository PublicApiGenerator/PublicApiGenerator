using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Class_nested : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_nested_classes()
        {
            AssertPublicApi<ClassWithNestedClass>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithNestedClass
    {
        public void Method() { }
        public class NestedClass
        {
            public void Method() { }
        }
    }
}");
        }

        [Fact]
        public void Should_ignore_private_nested_class()
        {
            AssertPublicApi<ClassWithPrivateNestedClass>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithPrivateNestedClass
    {
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_nested_class()
        {
            AssertPublicApi<ClassWithProtectedNestedClass>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithProtectedNestedClass
    {
        public void Method() { }
        protected class NestedClass
        {
            public void Method() { }
        }
    }
}");
        }

        [Fact]
        public void Should_output_multiple_nested_classes()
        {
            AssertPublicApi<ClassWithDeeplyNestedClasses>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithDeeplyNestedClasses
    {
        public void Method3() { }
        public class ClassWithOneNestedClass
        {
            public void Method2() { }
            public class InnerNestedClass
            {
                public void Method1() { }
            }
        }
    }
}");
        }

        [Fact]
        public void Should_output_nested_structs()
        {
            AssertPublicApi<StructWithNestedStruct>(
@"namespace ApiApproverTests.Examples
{
    public struct StructWithNestedStruct
    {
        public void Method() { }
        public struct NestedStruct
        {
            public void Method() { }
        }
    }
}");
        }

        [Fact]
        public void Should_output_multiple_nested_classes_and_structs()
        {
            AssertPublicApi<ClassWithDeeplyNestedStructs>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithDeeplyNestedStructs
    {
        public void Method3() { }
        public class ClassNestedAlongsideStruct
        {
            public void Method4() { }
        }
        public struct StructWithOneNestedStruct
        {
            public void Method2() { }
            public struct InnerNestedStruct
            {
                public void Method1() { }
            }
        }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Local
    namespace Examples
    {
        public class ClassWithNestedClass
        {
            public class NestedClass
            {
                public void Method() { }
            }

            public void Method() { }
        }

        public class ClassWithPrivateNestedClass
        {
            private class NestedClass
            {
                public void Method() { }
            }

            public void Method() { }
        }

        public class ClassWithProtectedNestedClass
        {
            protected class NestedClass
            {
                public void Method() { }
            }

            public void Method() { }
        }

        public class ClassWithDeeplyNestedClasses
        {
            public class ClassWithOneNestedClass
            {
                public class InnerNestedClass
                {
                    public void Method1() { }
                }

                public void Method2() { }
            }

            public void Method3() { }
        }

        public class ClassWithDeeplyNestedStructs
        {
            public struct StructWithOneNestedStruct
            {
                public struct InnerNestedStruct
                {
                    public void Method1() { }
                }

                public void Method2() { }
            }

            public class ClassNestedAlongsideStruct
            {
                public void Method4() { }
            }

            public void Method3() { }
        }

        public struct StructWithNestedStruct
        {
            public struct NestedStruct
            {
                public void Method() { }
            }

            public void Method() { }
        }
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}