using System;
using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Class_generics : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_generic_type_parameters()
        {
            AssertPublicApi(typeof(ClassWithGenericType<>), 
@"namespace ApiApproverTests.Examples
{
    public class ClassWithGenericType<T> { }
}");
        }

        [Fact]
        public void Should_output_multiple_generic_type_parameters()
        {
            AssertPublicApi(typeof(ClassWithMutlipleGenericTypes<,>),
@"namespace ApiApproverTests.Examples
{
    public class ClassWithMutlipleGenericTypes<T1, T2> { }
}");
        }

        [Fact]
        public void Should_output_reference_generic_type_constraint()
        {
            // The extra space before "class" is a hack!
            AssertPublicApi(typeof(ClassWithReferenceTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public class ClassWithReferenceTypeConstraint<T>
        where T :  class { }
}");
        }

        [Fact]
        public void Should_output_value_type_generic_type_constraint()
        {
            // The extra space before "struct" is a hack!
            AssertPublicApi(typeof(ClassWithValueTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public class ClassWithValueTypeConstraint<T>
        where T :  struct { }
}");
        }


        [Fact]
        public void Should_output_new_generic_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithDefaultConstructorTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public class ClassWithDefaultConstructorTypeConstraint<T>
        where T : new() { }
}");
        }

        [Fact]
        public void Should_output_specific_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithSpecificTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public class ClassWithSpecificTypeConstraint<T>
        where T : System.IDisposable { }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_value_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithSpecificTypeAndValueTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public class ClassWithSpecificTypeAndValueTypeConstraint<T>
        where T :  struct, System.IDisposable { }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_reference_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithSpecificTypeAndReferenceTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public class ClassWithSpecificTypeAndReferenceTypeConstraint<T>
        where T :  class, System.IDisposable { }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_default_constructor_type_constraint()
        {
            AssertPublicApi(typeof(ClassWithSpecificTypeAndDefaultConstructorTypeConstraint<>),
@"namespace ApiApproverTests.Examples
{
    public class ClassWithSpecificTypeAndDefaultConstructorTypeConstraint<T>
        where T : System.IDisposable, new () { }
}");
        }
    }

    namespace Examples
    {
        public class ClassWithGenericType<T>
        {
        }

        public class ClassWithMutlipleGenericTypes<T1, T2>
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
}