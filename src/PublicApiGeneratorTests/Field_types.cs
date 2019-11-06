using PublicApiGeneratorTests.Examples;
using System;
using System.Collections.Generic;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Field_types : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_use_fully_qualified_type_name()
        {
            AssertPublicApi<FieldWithComplexType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithComplexType
    {
        public PublicApiGeneratorTests.Examples.ComplexType Field;
        public FieldWithComplexType() { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_parameters()
        {
            AssertPublicApi<FieldWithGenericType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithGenericType
    {
        public PublicApiGeneratorTests.Examples.GenericType<int> Field;
        public FieldWithGenericType() { }
    }
}");
        }

        [Fact]
        public void Should_use_fully_qualified_type_name_for_generic_parameters()
        {
            AssertPublicApi<FieldWithGenericComplexType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithGenericComplexType
    {
        public PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType> Field;
        public FieldWithGenericComplexType() { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_parameters_with_generic_parameters()
        {
            AssertPublicApi<FieldWithGenericTypeParametersOfGenericTypeParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithGenericTypeParametersOfGenericTypeParameters
    {
        public PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType>> Field;
        public FieldWithGenericTypeParametersOfGenericTypeParameters() { }
    }
}");
        }

        [Fact]
        public void Should_output_multiple_generic_parameters()
        {
            AssertPublicApi<FieldWithMultipleGenericTypeParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithMultipleGenericTypeParameters
    {
        public PublicApiGeneratorTests.Examples.GenericTypeExtra<int, string, PublicApiGeneratorTests.Examples.ComplexType> Field;
        public FieldWithMultipleGenericTypeParameters() { }
    }
}");
        }

        [Fact]
        public void Should_output_readonly_func_with_type_arguments()
        {
            AssertPublicApi<FieldWithFunc>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithFunc
    {
        public readonly System.Func<string, string, string, System.Collections.Generic.IEnumerable<string>, string> FuncField;
        public readonly System.Func<string, string, string, string> FuncField2;
        public FieldWithFunc() { }
    }
}");
        }

        [Fact]
        public void Should_output_nullable_types()
        {
            AssertPublicApi<FieldWithNullable>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithNullable
    {
        public readonly int? NullableInt;
        public readonly TimeSpan? NullableTimespan;
        public FieldWithNullable() { }
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public class FieldWithComplexType
        {
            public ComplexType Field;
        }

        public class FieldWithGenericType
        {
            public GenericType<int> Field;
        }

        public class FieldWithGenericComplexType
        {
            public GenericType<ComplexType> Field;
        }

        public class FieldWithGenericTypeParametersOfGenericTypeParameters
        {
            public GenericType<GenericType<ComplexType>> Field;
        }

        public class FieldWithMultipleGenericTypeParameters
        {
            public GenericTypeExtra<int, string, ComplexType> Field;
        }

        public class FieldWithFunc
        {
            public readonly Func<string, string, string, IEnumerable<string>, string> FuncField = (a, b, c, d) => null;
            public readonly Func<string, string, string, string> FuncField2;
        }

        public class FieldWithNullable
        {
            public readonly TimeSpan? NullableTimespan;
            public readonly int? NullableInt;
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}