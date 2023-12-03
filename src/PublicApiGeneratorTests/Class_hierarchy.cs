using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Class_hierarchy : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_base_class()
        {
            AssertPublicApi<DerivedClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class DerivedClass : PublicApiGeneratorTests.Examples.BaseClass
    {
        public DerivedClass() { }
    }
}");
        }

        [Fact]
        public void Should_output_implementing_interfaces_in_alphabetical_order()
        {
            AssertPublicApi<ClassWithInterfaces>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithInterfaces : System.ICloneable, System.IDisposable
    {
        public ClassWithInterfaces() { }
        public object Clone() { }
        public void Dispose() { }
    }
}");
        }

        [Fact]
        public void Should_output_base_class_and_interfaces()
        {
            AssertPublicApi<ClassWithBaseAndInterfaces>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithBaseAndInterfaces : PublicApiGeneratorTests.Examples.BaseClass, System.ICloneable, System.IDisposable
    {
        public ClassWithBaseAndInterfaces() { }
        public object Clone() { }
        public void Dispose() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_interfaces_implemented_on_base_classes()
        {
            AssertPublicApi<DerivedFromClassWithInterfaces>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class DerivedFromClassWithInterfaces : PublicApiGeneratorTests.Examples.ClassWithInterfaces
    {
        public DerivedFromClassWithInterfaces() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_base_class_of_object()
        {
            AssertPublicApi<BaseClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class BaseClass
    {
        public BaseClass() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_internal_interfaces_on_class()
        {
            AssertPublicApi<ClassWithInternalInterface>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithInternalInterface
    {
        public ClassWithInternalInterface() { }
    }
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

        public class ClassWithInternalInterface : IInternalInterface
        {
        }
    }
}
