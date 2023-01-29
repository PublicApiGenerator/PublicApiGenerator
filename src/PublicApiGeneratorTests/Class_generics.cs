using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Class_generics : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_generic_type_parameters()
        {
            AssertPublicApi(typeof(ClassWithGenericType<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithGenericType<T>
    {
        public ClassWithGenericType() { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_type_parameter_attribute()
        {
            AssertPublicApi(typeof(ClassWithGenericTypeAttribute<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithGenericTypeAttribute<[PublicApiGeneratorTests.Examples.MyType] T>
    {
        public ClassWithGenericTypeAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_output_multiple_generic_type_parameters()
        {
            AssertPublicApi(typeof(ClassWithMultipleGenericTypes<,>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithMultipleGenericTypes<T1, T2>
    {
        public ClassWithMultipleGenericTypes() { }
    }
}");
        }

        [Fact]
        public void Should_output_reference_generic_type_constraint()
        {
            // The extra space before "class" is a hack!
            AssertPublicApi(typeof(ClassWithReferenceTypeConstraint<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithReferenceTypeConstraint<T>
        where T :  class
    {
        public ClassWithReferenceTypeConstraint() { }
    }
}");
        }

        [Fact]
        public void Should_output_value_type_generic_type_constraint()
        {
            // The extra space before "struct" is a hack!
            AssertPublicApi(typeof(ClassWithValueTypeConstraint<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithValueTypeConstraint<T>
        where T :  struct
    {
        public ClassWithValueTypeConstraint() { }
    }
}");
        }


        [Fact]
        public void Should_output_new_generic_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithDefaultConstructorTypeConstraint<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithDefaultConstructorTypeConstraint<T>
        where T : new()
    {
        public ClassWithDefaultConstructorTypeConstraint() { }
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithSpecificTypeConstraint<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithSpecificTypeConstraint<T>
        where T : System.IDisposable
    {
        public ClassWithSpecificTypeConstraint() { }
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_value_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithSpecificTypeAndValueTypeConstraint<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithSpecificTypeAndValueTypeConstraint<T>
        where T :  struct, System.IDisposable
    {
        public ClassWithSpecificTypeAndValueTypeConstraint() { }
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_reference_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithSpecificTypeAndReferenceTypeConstraint<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithSpecificTypeAndReferenceTypeConstraint<T>
        where T :  class, System.IDisposable
    {
        public ClassWithSpecificTypeAndReferenceTypeConstraint() { }
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_default_constructor_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithSpecificTypeAndDefaultConstructorTypeConstraint<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithSpecificTypeAndDefaultConstructorTypeConstraint<T>
        where T : System.IDisposable, new ()
    {
        public ClassWithSpecificTypeAndDefaultConstructorTypeConstraint() { }
    }
}");
        }
    }

    // ReSharper disable UnusedTypeParameter
    namespace Examples
    {
        public class ClassWithGenericType<T>
        {
        }

        public class ClassWithGenericTypeAttribute<[MyType]T>
        {
        }

        [AttributeUsage(AttributeTargets.GenericParameter)]
        public sealed class MyTypeAttribute : Attribute
        {
        }

        public class ClassWithMultipleGenericTypes<T1, T2>
        {
        }

        public class ClassWithReferenceTypeConstraint<T>
            where T : class
        {
        }

        public class ClassWithValueTypeConstraint<T>
            where T : struct
        {
        }

        public class ClassWithDefaultConstructorTypeConstraint<T>
            where T : new()
        {
        }

        public class ClassWithSpecificTypeConstraint<T>
            where T : IDisposable
        {
        }

        public class ClassWithSpecificTypeAndDefaultConstructorTypeConstraint<T>
            where T : IDisposable, new()
        {
        }

        public class ClassWithSpecificTypeAndReferenceTypeConstraint<T>
            where T : class, IDisposable
        {
        }

        public class ClassWithSpecificTypeAndValueTypeConstraint<T>
            where T : struct, IDisposable
        {
        }
    }
    // ReSharper restore UnusedTypeParameter
}
