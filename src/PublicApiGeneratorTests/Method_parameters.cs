using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Method_parameters : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_handle_no_parameters()
        {
            AssertPublicApi<MethodWithNoParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithNoParameters
    {
        public MethodWithNoParameters() { }
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_parameter_name()
        {
            AssertPublicApi<MethodWithSingleParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithSingleParameter
    {
        public MethodWithSingleParameter() { }
        public void Method(int value) { }
    }
}");
        }

        [Fact]
        public void Should_output_primitive_parameter()
        {
            AssertPublicApi<MethodWithSingleParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithSingleParameter
    {
        public MethodWithSingleParameter() { }
        public void Method(int value) { }
    }
}");
        }

        [Fact]
        public void Should_use_fully_qualified_type_name_for_parameter()
        {
            AssertPublicApi<MethodWithComplexTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithComplexTypeParameter
    {
        public MethodWithComplexTypeParameter() { }
        public void Method(PublicApiGeneratorTests.Examples.ComplexType value) { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_type()
        {
            AssertPublicApi<MethodWithGenericTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericTypeParameter
    {
        public MethodWithGenericTypeParameter() { }
        public void Method(PublicApiGeneratorTests.Examples.GenericType<int> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_ref_type()
        {
            AssertPublicApi<MethodWithGenericRefTypeParameter>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericRefTypeParameter
    {
        public MethodWithGenericRefTypeParameter() { }
        public void Method(ref PublicApiGeneratorTests.Examples.GenericType<int> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_in_type()
        {
            AssertPublicApi<MethodWithGenericInTypeParameter>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericInTypeParameter
    {
        public MethodWithGenericInTypeParameter() { }
        public void Method(in PublicApiGeneratorTests.Examples.GenericType<int> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_fully_qualified_type_name_for_generic_parameter()
        {
            AssertPublicApi<MethodWithGenericTypeOfComplexTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericTypeOfComplexTypeParameter
    {
        public MethodWithGenericTypeOfComplexTypeParameter() { }
        public void Method(PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_type_of_generic_type()
        {
            AssertPublicApi<MethodWithGenericTypeOfGenericTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericTypeOfGenericTypeParameter
    {
        public MethodWithGenericTypeOfGenericTypeParameter() { }
        public void Method(PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.GenericType<int>> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_with_multiple_type_parameters()
        {
            AssertPublicApi<MethodWithGenericTypeWithMultipleTypeArgumentsParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericTypeWithMultipleTypeArgumentsParameter
    {
        public MethodWithGenericTypeWithMultipleTypeArgumentsParameter() { }
        public void Method(PublicApiGeneratorTests.Examples.GenericTypeExtra<int, string, PublicApiGeneratorTests.Examples.ComplexType> value) { }
    }
}");
        }

        [Fact]
        public void Should_handle_multiple_parameters()
        {
            AssertPublicApi<MethodWithMultipleParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithMultipleParameters
    {
        public MethodWithMultipleParameters() { }
        public void Method(int value1, string value2, PublicApiGeneratorTests.Examples.ComplexType value3) { }
    }
}");
        }

        [Fact]
        public void Should_output_default_values_in_class()
        {
            AssertPublicApi<MethodWithDefaultValues>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithDefaultValues
    {
        public MethodWithDefaultValues() { }
        public void Method1(int value1 = 42, string value2 = ""hello world"", System.Threading.CancellationToken token = default, int value3 = 0, string value4 = null) { }
        public TType Method2<TType>(string name, TType defaultValue = null)
            where TType :  class { }
        public TType Method3<TType>(string name, TType defaultValue = default)
            where TType :  struct { }
        public TType Method4<TType>(string name, TType defaultValue = default) { }
    }
}");
        }

        [Fact]
        public void Should_output_default_values_in_interface()
        {
            AssertPublicApi<InterfaceWithDefaultValues>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface InterfaceWithDefaultValues
    {
        void Method1(int value1 = 42, string value2 = ""hello world"", System.Threading.CancellationToken token = default, int value3 = 0, string value4 = null);
        TType Method2<TType>(string name, TType defaultValue = null)
            where TType :  class;
        TType Method3<TType>(string name, TType defaultValue = default)
            where TType :  struct;
        TType Method4<TType>(string name, TType defaultValue = default);
    }
}");
        }

        [Fact]
        public void Should_output_default_values_even_if_they_look_like_attributes()
        {
            AssertPublicApi<MethodWithDefaultThatLooksLikeAnAttribute>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithDefaultThatLooksLikeAnAttribute
    {
        public MethodWithDefaultThatLooksLikeAnAttribute() { }
        public void Method(string value2 = ""MyAttribute[()]"") { }
    }
}");
        }



        [Fact]
        public void Should_output_ref_parameters()
        {
            AssertPublicApi<MethodWithRefParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithRefParameter
    {
        public MethodWithRefParameter() { }
        public void Method(ref string value) { }
    }
}");
        }

        [Fact]
        public void Should_output_in_parameters()
        {
            AssertPublicApi<MethodWithInParameter>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithInParameter
    {
        public MethodWithInParameter() { }
        public void Method(in string value) { }
    }
}");
        }

        [Fact]
        public void Should_output_out_parameters()
        {
            AssertPublicApi<MethodWithOutParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithOutParameter
    {
        public MethodWithOutParameter() { }
        public void Method(out string value) { }
    }
}");
        }

        [Fact]
        public void Should_output_out_parameter_with_generic_type()
        {
            AssertPublicApi<MethodWithOutGenericTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithOutGenericTypeParameter
    {
        public MethodWithOutGenericTypeParameter() { }
        public void Method(out PublicApiGeneratorTests.Examples.GenericType<int> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_out_parameter_with_generic_enumerable()
        {
            AssertPublicApi<MethodWithOutGenericTypeParameterEnumerable>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithOutGenericTypeParameterEnumerable
    {
        public MethodWithOutGenericTypeParameterEnumerable() { }
        public void Method(out System.Collections.Generic.IEnumerable<int> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_out_parameter_fully_qualified_type_name_for_generic_parameter()
        {
            AssertPublicApi<MethodWithOutGenericTypeOfComplexTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithOutGenericTypeOfComplexTypeParameter
    {
        public MethodWithOutGenericTypeOfComplexTypeParameter() { }
        public void Method(out PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType> value) { }
    }
}");
        }

        //

        [Fact]
        public void Should_output_params_keyword()
        {
            AssertPublicApi<MethodWithParams>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithParams
    {
        public MethodWithParams() { }
        public void Method(string format, params object[] values) { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedParameter.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class MethodWithNoParameters
        {
            public void Method()
            {
            }
        }

        public class MethodWithSingleParameter
        {
            public void Method(int value)
            {
            }
        }

        public class MethodWithComplexTypeParameter
        {
            public void Method(ComplexType value)
            {
            }
        }

        public class MethodWithGenericTypeParameter
        {
            public void Method(GenericType<int> value)
            {
            }
        }

        public class MethodWithGenericRefTypeParameter
        {
            public void Method(ref GenericType<int> value)
            {
            }
        }

        public class MethodWithGenericInTypeParameter
        {
            public void Method(in GenericType<int> value)
            {
            }
        }

        public class MethodWithGenericTypeOfComplexTypeParameter
        {
            public void Method(GenericType<ComplexType> value)
            {
            }
        }

        public class MethodWithGenericTypeOfGenericTypeParameter
        {
            public void Method(GenericType<GenericType<int>> value)
            {
            }
        }

        public class MethodWithGenericTypeWithMultipleTypeArgumentsParameter
        {
            public void Method(GenericTypeExtra<int, string, ComplexType> value)
            {
            }
        }

        public class MethodWithMultipleParameters
        {
            public void Method(int value1, string value2, ComplexType value3)
            {
            }
        }

        public class MethodWithDefaultValues
        {
            public void Method1(int value1 = 42, string value2 = "hello world", CancellationToken token = default, int value3 = default, string value4 = default!)
            {
            }
            public TType Method2<TType>(string name, TType defaultValue = null) where TType : class => default;
            public TType Method3<TType>(string name, TType defaultValue = default) where TType : struct => default;
            public TType Method4<TType>(string name, TType defaultValue = default) => default;
        }

        public interface InterfaceWithDefaultValues
        {
            void Method1(int value1 = 42, string value2 = "hello world", CancellationToken token = default, int value3 = default, string value4 = default!);
            TType Method2<TType>(string name, TType defaultValue = null) where TType : class;
            TType Method3<TType>(string name, TType defaultValue = default) where TType : struct;
            TType Method4<TType>(string name, TType defaultValue = default);
        }

        public class MethodWithDefaultThatLooksLikeAnAttribute
        {
            public void Method(string value2 = "MyAttribute[()]")
            {
            }
        }

        public class MethodWithRefParameter
        {
            public void Method(ref string value)
            {
            }
        }

        public class MethodWithInParameter
        {
            public void Method(in string value)
            {
            }
        }

        public class MethodWithOutParameter
        {
            public void Method(out string value)
            {
                value = null!;
            }
        }

        public class MethodWithOutGenericTypeParameter
        {
            public void Method(out GenericType<int> value)
            {
                value = null!;
            }
        }

        public class MethodWithOutGenericTypeParameterEnumerable
        {
            public void Method(out System.Collections.Generic.IEnumerable<int> value)
            {
                value = null!;
            }
        }

        public class MethodWithOutGenericTypeOfComplexTypeParameter
        {
            public void Method(out GenericType<ComplexType> value)
            {
                value = null!;
            }
        }

        public class MethodWithParams
        {
            public void Method(string format, params object[] values)
            {
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedParameter.Global
    // ReSharper restore UnusedMember.Global
}
