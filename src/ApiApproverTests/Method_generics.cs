using System;
using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Method_generics : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_generic_type_parameters()
        {
            AssertPublicApi<MethodWithTypeParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithTypeParameter
    {
        public void Method<T>() { }
    }
}");
        }

        [Fact]
        public void Should_output_multiple_generic_type_parameters()
        {
            AssertPublicApi<MethodWithMultipleTypeParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithMultipleTypeParameters
    {
        public void Method<T1, T2>() { }
    }
}");
        }

        [Fact]
        public void Should_output_reference_generic_type_constraint()
        {
            // The extra space before "class" is a hack!
            AssertPublicApi<MethodWithTypeParameterWithReferenceTypeConstraint>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithTypeParameterWithReferenceTypeConstraint
    {
        public void Method<T>()
            where T :  class { }
    }
}");
        }

        [Fact]
        public void Should_output_value_type_generic_type_constraint()
        {
            // The extra space before "struct" is a hack!
            AssertPublicApi<MethodWithTypeParameterWithValueTypeConstraint>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithTypeParameterWithValueTypeConstraint
    {
        public void Method<T>()
            where T :  struct { }
    }
}");
        }

        [Fact]
        public void Should_output_new_generic_type_constraint()
        {
            AssertPublicApi<MethodWithTypeParameterWithDefaultConstructorConstraint>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithTypeParameterWithDefaultConstructorConstraint
    {
        public void Method<T>()
            where T : new() { }
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_constraint()
        {
            AssertPublicApi<MethodWithTypeParameterWithSpecificTypeConstraint>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithTypeParameterWithSpecificTypeConstraint
    {
        public void Method<T>()
            where T : System.IDisposable { }
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_value_type_constraint()
        {
            AssertPublicApi<MethodWithTypeParameterWithSpecificTypeAndValueTypeConstraints>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithTypeParameterWithSpecificTypeAndValueTypeConstraints
    {
        public void Method<T>()
            where T :  struct, System.IDisposable { }
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_reference_type_constraint()
        {
            AssertPublicApi<MethodWithTypeParameterWithSpecificTypeAndReferenceTypeConstraints>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithTypeParameterWithSpecificTypeAndReferenceTypeConstraints
    {
        public void Method<T>()
            where T :  class, System.IDisposable { }
    }
}");
        }

        [Fact]
        public void Should_output_specific_type_and_new_constriant()
        {
            AssertPublicApi<MethodWithTypeParameterWithSpecificTypeAndDefaultConstructorConstraints>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithTypeParameterWithSpecificTypeAndDefaultConstructorConstraints
    {
        public void Method<T>()
            where T : System.IDisposable, new () { }
    }
}");
        }

        [Fact]
        public void Should_use_generic_type_name_in_parameter()
        {
            AssertPublicApi<MethodUsingGenericTypeParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodUsingGenericTypeParameter
    {
        public void Method<T>(T item) { }
    }
}");
        }

        [Fact]
        public void Should_use_generic_type_from_class_in_parameters()
        {
            AssertPublicApi(typeof(MethodUsingGenericTypeParameterFromClass<>),
@"namespace ApiApproverTests.Examples
{
    public class MethodUsingGenericTypeParameterFromClass<T>
    {
        public void Method(T item) { }
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
        public class MethodWithTypeParameter
        {
            public void Method<T>()
            {
            }
        }

        public class MethodWithMultipleTypeParameters
        {
            public void Method<T1, T2>()
            {
            }
        }

        public class MethodWithTypeParameterWithReferenceTypeConstraint
        {
            public void Method<T>()
                where T : class
            {
            }
        }

        public class MethodWithTypeParameterWithValueTypeConstraint
        {
            public void Method<T>()
                where T : struct
            {
            }
        }

        public class MethodWithTypeParameterWithDefaultConstructorConstraint
        {
            public void Method<T>()
                where T : new()
            {
            }
        }

        public class MethodWithTypeParameterWithSpecificTypeConstraint
        {
            public void Method<T>()
                where T : IDisposable
            {
            }
        }

        public class MethodWithTypeParameterWithSpecificTypeAndValueTypeConstraints
        {
            public void Method<T>()
                where T : struct, IDisposable
            {
            }
        }

        public class MethodWithTypeParameterWithSpecificTypeAndReferenceTypeConstraints
        {
            public void Method<T>()
                where T : class, IDisposable
            {
            }
        }

        public class MethodWithTypeParameterWithSpecificTypeAndDefaultConstructorConstraints
        {
            public void Method<T>()
                where T : IDisposable, new()
            {
            }
        }

        public class MethodUsingGenericTypeParameter
        {
            public void Method<T>(T item)
            {
            }
        }

        public class MethodUsingGenericTypeParameterFromClass<T>
        {
            public void Method(T item)
            {
            }
        }
    }
    // ReSharper restore UnusedParameter.Global
    // ReSharper restore UnusedTypeParameter
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}