using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Interface_method_generics : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_generic_type_parameters()
        {
            AssertPublicApi<IMethodWithTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithTypeParameter
    {
        void Method<T>();
    }
}");
        }

        [Fact]
        public void Should_output_multiple_generic_type_parameters()
        {
            AssertPublicApi<IMethodWithMultipleTypeParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithMultipleTypeParameters
    {
        void Method<T1, T2>();
    }
}");
        }

        [Fact]
        public void Should_output_reference_generic_type_constraint()
        {
            // The extra space before "class" is a hack!
            AssertPublicApi<IMethodWithTypeParameterWithReferenceTypeConstraint>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithTypeParameterWithReferenceTypeConstraint
    {
        void Method<T>()
            where T :  class;
    }
}");
        }

        [Fact]
        public void Should_output_value_type_generic_type_constraint()
        {
            // The extra space before "struct" is a hack!
            AssertPublicApi<IMethodWithTypeParameterWithValueTypeConstraint>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithTypeParameterWithValueTypeConstraint
    {
        void Method<T>()
            where T :  struct;
    }
}");
        }

        [Fact]
        public void Should_output_new_generic_type_constraint()
        {
            AssertPublicApi<IMethodWithTypeParameterWithDefaultConstructorConstraint>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithTypeParameterWithDefaultConstructorConstraint
    {
        void Method<T>()
            where T : new();
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_constraint()
        {
            AssertPublicApi<IMethodWithTypeParameterWithSpecificTypeConstraint>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithTypeParameterWithSpecificTypeConstraint
    {
        void Method<T>()
            where T : System.IDisposable;
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_value_type_constraint()
        {
            AssertPublicApi<IMethodWithTypeParameterWithSpecificTypeAndValueTypeConstraints>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithTypeParameterWithSpecificTypeAndValueTypeConstraints
    {
        void Method<T>()
            where T :  struct, System.IDisposable;
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_reference_type_constraint()
        {
            AssertPublicApi<IMethodWithTypeParameterWithSpecificTypeAndReferenceTypeConstraints>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithTypeParameterWithSpecificTypeAndReferenceTypeConstraints
    {
        void Method<T>()
            where T :  class, System.IDisposable;
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_new_constraint()
        {
            AssertPublicApi<IMethodWithTypeParameterWithSpecificTypeAndDefaultConstructorConstraints>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithTypeParameterWithSpecificTypeAndDefaultConstructorConstraints
    {
        void Method<T>()
            where T : System.IDisposable, new ();
    }
}");
        }

        [Fact]
        public void Should_use_generic_type_name_in_parameter()
        {
            AssertPublicApi<IMethodUsingGenericTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodUsingGenericTypeParameter
    {
        void Method<T>(T item);
    }
}");
        }

        [Fact]
        public void Should_use_generic_type_from_class_in_parameters()
        {
            AssertPublicApi(typeof(IMethodUsingGenericTypeParameterFromClass<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodUsingGenericTypeParameterFromClass<T>
    {
        void Method(T item);
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedTypeParameter
    // ReSharper disable UnusedParameter.Global
    namespace Examples
    {
        public interface IMethodWithTypeParameter
        {
            void Method<T>();
        }

        public interface IMethodWithMultipleTypeParameters
        {
            void Method<T1, T2>();
        }

        public interface IMethodWithTypeParameterWithReferenceTypeConstraint
        {
            void Method<T>() where T : class;
        }

        public interface IMethodWithTypeParameterWithValueTypeConstraint
        {
            void Method<T>() where T : struct;
        }

        public interface IMethodWithTypeParameterWithDefaultConstructorConstraint
        {
            void Method<T>() where T : new();
        }

        public interface IMethodWithTypeParameterWithSpecificTypeConstraint
        {
            void Method<T>() where T : IDisposable;
        }

        public interface IMethodWithTypeParameterWithSpecificTypeAndValueTypeConstraints
        {
            void Method<T>() where T : struct, IDisposable;
        }

        public interface IMethodWithTypeParameterWithSpecificTypeAndReferenceTypeConstraints
        {
            void Method<T>() where T : class, IDisposable;
        }

        public interface IMethodWithTypeParameterWithSpecificTypeAndDefaultConstructorConstraints
        {
            void Method<T>() where T : IDisposable, new();
        }

        public interface IMethodUsingGenericTypeParameter
        {
            void Method<T>(T item);
        }

        public interface IMethodUsingGenericTypeParameterFromClass<T>
        {
            void Method(T item);
        }
    }
    // ReSharper restore UnusedParameter.Global
    // ReSharper restore UnusedTypeParameter
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}