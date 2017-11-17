using System;
using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Interface_methods : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_void_method()
        {
            AssertPublicApi<IVoidMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IVoidMethod
    {
        void Method();
    }
}");
        }

        [Fact]
        public void Should_output_method_parameters()
        {
            AssertPublicApi<IMethodsWithParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodsWithParameters
    {
        int Method1(int intValue, string stringValue);
        PublicApiGeneratorTests.Examples.ComplexType Method2(PublicApiGeneratorTests.Examples.ComplexType value);
        PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType> Method3(PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType> value);
    }
}");
        }

        [Fact]
        public void Should_output_methods_with_default_values()
        {
            AssertPublicApi<IMethodsWithDefaultParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodsWithDefaultParameters
    {
        void Method1(int intValue = 42, string stringValue = ""hello world"", System.Type typeValue = null);
    }
}");
        }

        [Fact]
        public void Should_output_new_modifier()
        {
            AssertPublicApi<IMethodHiding>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodHiding : PublicApiGeneratorTests.Examples.IVoidMethod
    {
        new void Method();
    }
}");
        }

        [Fact]
        public void Should_output_methods_in_alphabetical_order()
        {
            AssertPublicApi<IMultipleMethods>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMultipleMethods
    {
        void AA_Method();
        void MM_Method();
        void ZZ_Method();
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public interface IVoidMethod
        {
            void Method();
        }

        public interface IMethodsWithParameters
        {
            int Method1(int intValue, string stringValue);
            ComplexType Method2(ComplexType value);
            GenericType<ComplexType> Method3(GenericType<ComplexType> value);
        }

        public interface IMethodsWithDefaultParameters
        {
            void Method1(int intValue = 42, string stringValue = "hello world", Type typeValue = null);
        }

        public interface IMultipleMethods
        {
            void ZZ_Method();
            void MM_Method();
            void AA_Method();
        }

        public interface IMethodHiding : IVoidMethod
        {
            new void Method();
        }
    }
    // ReSharper restore UnusedMember.Global
}