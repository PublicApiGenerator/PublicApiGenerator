using System;
using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Class_constructors : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_default_constructor()
        {
            AssertPublicApi<ClassWithDefaultConstructor>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithDefaultConstructor
    {
        public ClassWithDefaultConstructor() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_default_constructor_in_abstract_class()
        {
            AssertPublicApi<AbstractClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public abstract class AbstractClass
    {
        protected AbstractClass() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_protected_default_constructor_in_abstract_class_with_other_constructors()
        {
            AssertPublicApi<AbstractClassWithCtors>(
@"namespace PublicApiGeneratorTests.Examples
{
    public abstract class AbstractClassWithCtors
    {
        public AbstractClassWithCtors(int i) { }
        protected AbstractClassWithCtors(string j) { }
    }
}");
        }

        [Fact]
        public void Should_not_output_internal_constructor()
        {
            AssertPublicApi<ClassWithInternalConstructor>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithInternalConstructor { }
}");
        }

        [Fact]
        public void Should_not_output_private_constructor()
        {
            AssertPublicApi<ClassWithPrivateConstructor>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPrivateConstructor { }
}");
        }

        [Fact]
        public void Should_not_output_private_protected_constructor()
        {
            AssertPublicApi<ClassWithPrivateProtectedConstructor>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPrivateProtectedConstructor { }
}");
        }

        [Fact]
        public void Should_output_public_constructor()
        {
            AssertPublicApi<ClassWithPublicConstructor>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicConstructor
    {
        public ClassWithPublicConstructor() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_constructor()
        {
            AssertPublicApi<ClassWithProtectedConstructor>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedConstructor
    {
        protected ClassWithProtectedConstructor() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_internal_constructor()
        {
            AssertPublicApi<ClassWithProtectedInternalConstructor>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedInternalConstructor
    {
        protected ClassWithProtectedInternalConstructor() { }
    }
}");
        }

        [Fact]
        public void Should_output_constructor_parameters()
        {
            AssertPublicApi<ClassWithConstructorWithParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithConstructorWithParameters
    {
        public ClassWithConstructorWithParameters(int intValue, string stringValue, PublicApiGeneratorTests.Examples.ComplexType complexType, PublicApiGeneratorTests.Examples.GenericType<int> genericType, ref int intValueByRef, in int intValueByIn) { }
    }
}");
        }

        [Fact]
        public void Should_output_multiple_constructors()
        {
            // TODO: Not sure about ordering here
            AssertPublicApi<ClassWithMultipleConstructors>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithMultipleConstructors
    {
        public ClassWithMultipleConstructors() { }
        protected ClassWithMultipleConstructors(int value) { }
    }
}");
        }

        [Fact]
        public void Should_output_constructor_default_parameter_values()
        {
            AssertPublicApi<ClassWithConstructorWithDefaultValues>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithConstructorWithDefaultValues
    {
        public ClassWithConstructorWithDefaultValues(int intValue = 42, string stringValue = ""hello world"", System.Type typeValue = null) { }
    }
}");
        }

        [Fact]
        public void Should_not_output_static_constructor_of_class()
        {
            AssertPublicApi<ClassWithStaticConstructor>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithStaticConstructor
    {
        public ClassWithStaticConstructor() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_static_constructor_of_static_class()
        {
            AssertPublicApi(typeof(StaticClassWithStaticConstructor),
@"namespace PublicApiGeneratorTests.Examples
{
    public static class StaticClassWithStaticConstructor { }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedParameter.Local
    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public class ClassWithDefaultConstructor
        {
        }

        public class ClassWithInternalConstructor
        {
            internal ClassWithInternalConstructor()
            {
            }
        }

        public class ClassWithPrivateConstructor
        {
            private ClassWithPrivateConstructor()
            {
            }
        }

        public class ClassWithPrivateProtectedConstructor
        {
            private protected ClassWithPrivateProtectedConstructor()
            {
            }
        }

        public class ClassWithProtectedConstructor
        {
            protected ClassWithProtectedConstructor()
            {
            }
        }

        public class ClassWithProtectedInternalConstructor
        {
            protected internal ClassWithProtectedInternalConstructor()
            {
            }
        }

        public class ClassWithPublicConstructor
        {
            public ClassWithPublicConstructor()
            {
                Console.WriteLine();
            }
        }

        public class ClassWithConstructorWithParameters
        {
            public ClassWithConstructorWithParameters(int intValue, string stringValue, ComplexType complexType, GenericType<int> genericType, ref int intValueByRef, in int intValueByIn)
            {
            }
        }

        public class ClassWithMultipleConstructors
        {
            public ClassWithMultipleConstructors()
            {
            }

            protected ClassWithMultipleConstructors(int value)
            {
            }
        }

        public class ClassWithConstructorWithDefaultValues
        {
            public ClassWithConstructorWithDefaultValues(int intValue = 42, string stringValue = "hello world", Type typeValue = null!)
            {
            }
        }

        public class ClassWithStaticConstructor
        {
            static ClassWithStaticConstructor()
            {
            }
        }

        public static class StaticClassWithStaticConstructor
        {
            static StaticClassWithStaticConstructor()
            {
            }
        }

        public abstract class AbstractClassWithCtors
        {
            public AbstractClassWithCtors(int i) { }
            protected AbstractClassWithCtors(string j) { }
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedParameter.Local
    // ReSharper restore ClassNeverInstantiated.Global
}
