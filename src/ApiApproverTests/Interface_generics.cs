using System;
using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_generics : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_generic_type_parameters()
        {
            AssertPublicApi(typeof(IWithGenericType<>), 
@"namespace ApiApproverTests.Examples
{
    public interface IWithGenericType<T> { }
}");
        }

        [Fact]
        public void Should_output_multiple_generic_type_parameters()
        {
            AssertPublicApi(typeof(IWithMutlipleGenericTypes<,>),
@"namespace ApiApproverTests.Examples
{
    public interface IWithMutlipleGenericTypes<T1, T2> { }
}");
        }

        [Fact]
        public void Should_output_reference_generic_type_constraint()
        {
            // The extra space before "class" is a hack!
            AssertPublicApi(typeof(IWithReferenceTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public interface IWithReferenceTypeConstraint<T>
        where T :  class { }
}");
        }

        [Fact]
        public void Should_output_value_type_generic_type_constraint()
        {
            // The extra space before "struct" is a hack!
            AssertPublicApi(typeof(IWithValueTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public interface IWithValueTypeConstraint<T>
        where T :  struct { }
}");
        }


        [Fact]
        public void Should_output_new_generic_type_constraint()
        {
            AssertPublicApi(typeof(IWithDefaultConstructorTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public interface IWithDefaultConstructorTypeConstraint<T>
        where T : new() { }
}");
        }

        [Fact]
        public void Should_output_specific_type_constraint()
        {
            AssertPublicApi(typeof(IWithSpecificTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public interface IWithSpecificTypeConstraint<T>
        where T : System.IDisposable { }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_value_type_constraint()
        {
            AssertPublicApi(typeof(IWithSpecificTypeAndValueTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public interface IWithSpecificTypeAndValueTypeConstraint<T>
        where T :  struct, System.IDisposable { }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_reference_type_constraint()
        {
            AssertPublicApi(typeof(IWithSpecificTypeAndReferenceTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public interface IWithSpecificTypeAndReferenceTypeConstraint<T>
        where T :  class, System.IDisposable { }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_default_constructor_type_constraint()
        {
            AssertPublicApi(typeof(IWithSpecificTypeAndDefaultConstructorTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public interface IWithSpecificTypeAndDefaultConstructorTypeConstraint<T>
        where T : System.IDisposable, new () { }
}");
        }
    }

    // ReSharper disable UnusedTypeParameter
    namespace Examples
    {
        public interface IWithGenericType<T>
        {
        }

        public interface IWithMutlipleGenericTypes<T1, T2>
        {
        }

        public interface IWithReferenceTypeConstraint<T>
            where T : class
        {
        }

        public interface IWithValueTypeConstraint<T>
            where T : struct
        {
        }

        public interface IWithDefaultConstructorTypeConstraint<T>
            where T : new()
        {
        }

        public interface IWithSpecificTypeConstraint<T>
            where T : IDisposable
        {
        }

        public interface IWithSpecificTypeAndDefaultConstructorTypeConstraint<T>
            where T : IDisposable, new()
        {
        }

        public interface IWithSpecificTypeAndReferenceTypeConstraint<T>
            where T : class, IDisposable
        {
        }

        public interface IWithSpecificTypeAndValueTypeConstraint<T>
            where T : struct, IDisposable
        {
        }
    }
    // ReSharper restore UnusedTypeParameter
}