using System;
using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Struct_hierarchy : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_not_output_base_class_of_value_type()
        {
            AssertRoslynPublicApi<SimpleStruct>(
@"namespace PublicApiGeneratorTests.Examples
{
    public struct SimpleStruct { }
}");
        }

        [Fact]
        public void Should_output_implementing_interfaces_in_alphabetical_order()
        {
            AssertPublicApi<StructWithInterfaces>(
@"namespace PublicApiGeneratorTests.Examples
{
    public struct StructWithInterfaces : System.ICloneable, System.IDisposable
    {
        public object Clone() { }
        public void Dispose() { }
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public struct SimpleStruct
        {
        }

        public struct StructWithInterfaces : IDisposable, ICloneable
        {
            public void Dispose()
            {
            }

            public object Clone()
            {
                return this;
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
}