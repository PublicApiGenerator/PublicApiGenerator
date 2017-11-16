using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Class_nested : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_nested_classes()
        {
            AssertPublicApi<ClassWithNestedClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithNestedClass
    {
        public ClassWithNestedClass() { }
        public void Method() { }
        public class NestedClass
        {
            public NestedClass() { }
            public void Method() { }
        }
    }
}");
        }

        [Fact]
        public void Should_ignore_private_nested_class()
        {
            AssertPublicApi<ClassWithPrivateNestedClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPrivateNestedClass
    {
        public ClassWithPrivateNestedClass() { }
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_nested_class()
        {
            AssertPublicApi<ClassWithProtectedNestedClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedNestedClass
    {
        public ClassWithProtectedNestedClass() { }
        public void Method() { }
        protected class NestedClass
        {
            public NestedClass() { }
            public void Method() { }
        }
    }
}");
        }

        [Fact]
        public void Should_output_multiple_nested_classes()
        {
            AssertPublicApi<ClassWithDeeplyNestedClasses>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithDeeplyNestedClasses
    {
        public ClassWithDeeplyNestedClasses() { }
        public void Method3() { }
        public class ClassWithOneNestedClass
        {
            public ClassWithOneNestedClass() { }
            public void Method2() { }
            public class InnerNestedClass
            {
                public InnerNestedClass() { }
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
@"namespace PublicApiGeneratorTests.Examples
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
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithDeeplyNestedStructs
    {
        public ClassWithDeeplyNestedStructs() { }
        public void Method3() { }
        public class ClassNestedAlongsideStruct
        {
            public ClassNestedAlongsideStruct() { }
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