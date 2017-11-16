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
        public void Should_output_constructor_parameters()
        {
            AssertPublicApi<ClassWithConstructorWithParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithConstructorWithParameters
    {
        public ClassWithConstructorWithParameters(int intValue, string stringValue, PublicApiGeneratorTests.Examples.ComplexType complexType, PublicApiGeneratorTests.Examples.GenericType<int> genericType) { }
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

        public class ClassWithProtectedConstructor
        {
            protected ClassWithProtectedConstructor()
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
            public ClassWithConstructorWithParameters(int intValue, string stringValue, ComplexType complexType, GenericType<int> genericType)
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
            public ClassWithConstructorWithDefaultValues(int intValue = 42, string stringValue = "hello world", Type typeValue = null)
            {
            }
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedParameter.Local
    // ReSharper restore ClassNeverInstantiated.Global
}