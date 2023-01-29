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
        public void Should_ignore_internal_nested_class()
        {
            AssertPublicApi<ClassWithInternalNestedClass>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithInternalNestedClass
    {
        public ClassWithInternalNestedClass() { }
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_ignore_private_protected_nested_class()
        {
            AssertPublicApi<ClassWithPrivateProtectedNestedClass>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPrivateProtectedNestedClass
    {
        public ClassWithPrivateProtectedNestedClass() { }
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
        public void Should_output_protected_internal_nested_class()
        {
            AssertPublicApi<ClassWithProtectedInternalNestedClass>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedInternalNestedClass
    {
        public ClassWithProtectedInternalNestedClass() { }
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

        [Fact]
        public void Should_output_Nested_Classes_From_NullableExample1()
        {
            AssertPublicApi(typeof(Foo),
@"namespace PublicApiGeneratorTests.Examples
{
    public class Foo
    {
        public Foo(PublicApiGeneratorTests.Examples.Foo.Bar bar) { }
        public class Bar
        {
            public Bar(PublicApiGeneratorTests.Examples.Foo.Bar.Baz? baz) { }
            public class Baz
            {
                public Baz() { }
            }
        }
    }
}");
        }

        [Fact]
        public void Should_output_Nested_Classes_From_NullableExample2()
        {
            AssertPublicApi(typeof(Foo<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class Foo<T>
    {
        public Foo(PublicApiGeneratorTests.Examples.Foo<T>.Bar<int> bar) { }
        public class Bar<U>
        {
            public Bar(PublicApiGeneratorTests.Examples.Foo<T>.Bar<U>.Baz<T, U>? baz) { }
            public class Baz<V, K>
            {
                public Baz() { }
            }
        }
    }
}");
        }

        [Fact]
        public void Should_Not_Output_Generic_Parameters_From_Declaring_Type()
        {
            AssertPublicApi(typeof(Foo<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class Foo<T1, T2>
    {
        public Foo() { }
        public class Bar
        {
            public T1 Data;
            public Bar() { }
        }
        public class Bar<T3>
        {
            public System.Collections.Generic.List<T2>? Field;
            public Bar() { }
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

        public class ClassWithInternalNestedClass
        {
            internal class NestedClass
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

        public class ClassWithProtectedInternalNestedClass
        {
            protected internal class NestedClass
            {
                public void Method() { }
            }

            public void Method() { }
        }

        public class ClassWithPrivateProtectedNestedClass
        {
            private protected class NestedClass
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

        // nullable example
        public class Foo
        {
            public class Bar
            {
                public class Baz { }

                public Bar(Baz? baz) { }
            }

            public Foo(Bar bar)
            {
            }
        }

        // nullable generic example 1
        public class Foo<T>
        {
            public class Bar<U>
            {
                public class Baz<V, K> { }

                public Bar(Baz<T, U>? baz) { }
            }

            public Foo(Bar<int> bar)
            {
            }
        }

        // nullable generic example 2
        public class Foo<T1, T2>
        {
            public class Bar
            {
                public T1 Data;
            }

            public class Bar<T3>
            {
                public List<T2>? Field;
            }
        }
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}
