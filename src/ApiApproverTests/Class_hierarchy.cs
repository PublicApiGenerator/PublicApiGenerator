using System;
using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Class_hierarchy : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_base_class()
        {
            AssertPublicApi<DerivedClass>(
@"namespace ApiApproverTests.Examples
{
    public class DerivedClass : ApiApproverTests.Examples.BaseClass { }
}");
        }

        [Fact]
        public void Should_output_implementing_interfaces_in_alphabetical_order()
        {
            AssertPublicApi<ClassWithInterfaces>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithInterfaces : System.ICloneable, System.IDisposable
    {
        public object Clone() { }
        public void Dispose() { }
    }
}");
        }

        [Fact]
        public void Should_output_base_class_and_interfaces()
        {
            AssertPublicApi<ClassWithBaseAndInterfaces>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithBaseAndInterfaces : ApiApproverTests.Examples.BaseClass, System.ICloneable, System.IDisposable
    {
        public object Clone() { }
        public void Dispose() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_interfaces_implemented_on_base_classes()
        {
            AssertPublicApi<DerivedFromClassWithInterfaces>(
@"namespace ApiApproverTests.Examples
{
    public class DerivedFromClassWithInterfaces : ApiApproverTests.Examples.ClassWithInterfaces { }
}");
        }

        [Fact]
        public void Should_not_output_base_class_of_object()
        {
            AssertPublicApi<BaseClass>(
@"namespace ApiApproverTests.Examples
{
    public class BaseClass { }
}");
        }
    }

    namespace Examples
    {
        public class BaseClass
        {
        }

        public class DerivedClass : BaseClass
        {
        }

        public class ClassWithInterfaces : IDisposable, ICloneable
        {
            public void Dispose()
            {
            }

            public object Clone()
            {
                return this;
            }
        }

        public class ClassWithBaseAndInterfaces : BaseClass, IDisposable, ICloneable
        {
            public void Dispose()
            {
            }

            public object Clone()
            {
                return this;
            }
        }

        public class DerivedFromClassWithInterfaces : ClassWithInterfaces
        {
        }
    }
}