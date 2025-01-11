using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Method_generics : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_generic_type_parameters()
        {
            AssertPublicApi<MethodWithTypeParameter>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithTypeParameter
    {
        public MethodWithTypeParameter() { }
        public void Method<T>() { }
    }
}
""");
        }

        [Fact]
        public void Should_output_multiple_generic_type_parameters()
        {
            AssertPublicApi<MethodWithMultipleTypeParameters>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithMultipleTypeParameters
    {
        public MethodWithMultipleTypeParameters() { }
        public void Method<T1, T2>() { }
    }
}
""");
        }

        [Fact]
        public void Should_output_reference_generic_type_constraint()
        {
            // The extra space before "class" is a hack!
            AssertPublicApi<MethodWithTypeParameterWithReferenceTypeConstraint>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithTypeParameterWithReferenceTypeConstraint
    {
        public MethodWithTypeParameterWithReferenceTypeConstraint() { }
        public void Method<T>()
            where T :  class { }
    }
}
""");
        }

        [Fact]
        public void Should_output_value_type_generic_type_constraint()
        {
            // The extra space before "struct" is a hack!
            AssertPublicApi<MethodWithTypeParameterWithValueTypeConstraint>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithTypeParameterWithValueTypeConstraint
    {
        public MethodWithTypeParameterWithValueTypeConstraint() { }
        public void Method<T>()
            where T :  struct { }
    }
}
""");
        }

        [Fact]
        public void Should_output_new_generic_type_constraint()
        {
            AssertPublicApi<MethodWithTypeParameterWithDefaultConstructorConstraint>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithTypeParameterWithDefaultConstructorConstraint
    {
        public MethodWithTypeParameterWithDefaultConstructorConstraint() { }
        public void Method<T>()
            where T : new() { }
    }
}
""");
        }

        [Fact]
        public void Should_output_specific_type_constraint()
        {
            AssertPublicApi<MethodWithTypeParameterWithSpecificTypeConstraint>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithTypeParameterWithSpecificTypeConstraint
    {
        public MethodWithTypeParameterWithSpecificTypeConstraint() { }
        public void Method<T>()
            where T : System.IDisposable { }
    }
}
""");
        }

        [Fact]
        public void Should_output_specific_type_and_value_type_constraint()
        {
            AssertPublicApi<MethodWithTypeParameterWithSpecificTypeAndValueTypeConstraints>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithTypeParameterWithSpecificTypeAndValueTypeConstraints
    {
        public MethodWithTypeParameterWithSpecificTypeAndValueTypeConstraints() { }
        public void Method<T>()
            where T :  struct, System.IDisposable { }
    }
}
""");
        }

        [Fact]
        public void Should_output_specific_type_and_reference_type_constraint()
        {
            AssertPublicApi<MethodWithTypeParameterWithSpecificTypeAndReferenceTypeConstraints>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithTypeParameterWithSpecificTypeAndReferenceTypeConstraints
    {
        public MethodWithTypeParameterWithSpecificTypeAndReferenceTypeConstraints() { }
        public void Method<T>()
            where T :  class, System.IDisposable { }
    }
}
""");
        }

        [Fact]
        public void Should_output_specific_type_and_new_constraint()
        {
            AssertPublicApi<MethodWithTypeParameterWithSpecificTypeAndDefaultConstructorConstraints>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithTypeParameterWithSpecificTypeAndDefaultConstructorConstraints
    {
        public MethodWithTypeParameterWithSpecificTypeAndDefaultConstructorConstraints() { }
        public void Method<T>()
            where T : System.IDisposable, new () { }
    }
}
""");
        }

        [Fact]
        public void Should_use_generic_type_name_in_parameter()
        {
            AssertPublicApi<MethodUsingGenericTypeParameter>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodUsingGenericTypeParameter
    {
        public MethodUsingGenericTypeParameter() { }
        public void Method<T>(T item) { }
    }
}
""");
        }

        [Fact]
        public void Should_output_complex_type_constraints()
        {
            AssertPublicApi<MethodComplexTypeParameterConstraint>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MethodComplexTypeParameterConstraint
    {
        public MethodComplexTypeParameterConstraint() { }
        public void Add<T, U>(string s)
            where U : class, System.Collections.Generic.IComparer<T>, System.Collections.Generic.IEnumerable<U> { }
    }
}
""");
        }

        [Fact]
        public void Should_use_generic_type_from_class_in_parameters()
        {
            AssertPublicApi(typeof(MethodUsingGenericTypeParameterFromClass<>), """
namespace PublicApiGeneratorTests.Examples
{
    public class MethodUsingGenericTypeParameterFromClass<T>
    {
        public MethodUsingGenericTypeParameterFromClass() { }
        public void Method(T item) { }
    }
}
""");
        }

        [Fact]
        public void Should_use_short_name_for_generic_type_parameter_in_generic_args()
        {
            AssertPublicApi(typeof(MethodWithGenericUseOfClassTypeParameter<>), """
namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericUseOfClassTypeParameter<T>
    {
        public MethodWithGenericUseOfClassTypeParameter() { }
        public System.Collections.Generic.List<T> Method(System.Collections.Generic.List<T> item) { }
    }
}
""");
        }
    }

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

        public class MethodComplexTypeParameterConstraint
        {
            public void Add<T, U>(string s) where U : class, IComparer<T>, IEnumerable<U>
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

        public class MethodWithGenericUseOfClassTypeParameter<T>
        {
            public List<T> Method(List<T> item)
            {
                return null;
            }
        }
    }
}
