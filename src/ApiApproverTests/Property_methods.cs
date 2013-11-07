using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Property_methods : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_getter_and_setter()
        {
            AssertPublicApi<PropertyReadWrite>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyReadWrite
    {
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_getter_only()
        {
            AssertPublicApi<PropertyReadOnly>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyReadOnly
    {
        public string Value { get; }
    }
}");
        }

        [Fact]
        public void Should_output_setter_only()
        {
            AssertPublicApi<PropertyWriteOnly>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWriteOnly
    {
        public string Value { set; }
    }
}");
        }

        [Fact]
        public void Should_output_indexer_read_write()
        {
            AssertPublicApi<PropertyIndexer>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyIndexer
    {
        public string this[int index] { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_indexer_read_only()
        {
            AssertPublicApi<PropertyIndexerReadOnly>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyIndexerReadOnly
    {
        public string this[int index] { get; }
    }
}");
        }

        [Fact]
        public void Should_output_indexer_write_only()
        {
            AssertPublicApi<PropertyIndexerWriteOnly>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyIndexerWriteOnly
    {
        public string this[int index] { set; }
    }
}");
        }

        [Fact]
        public void Should_output_indexer_multiple_parameters()
        {
            AssertPublicApi<PropertyIndexerMultipleParameters>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyIndexerMultipleParameters
    {
        public string this[int index, int order] { get; set; }
    }
}");
        }
    }

    // ReSharper disable ValueParameterNotUsed
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class PropertyReadWrite
        {
            public string Value { get { return string.Empty; } set { } }
        }

        public class PropertyReadOnly
        {
            public string Value { get { return string.Empty; } }
        }

        public class PropertyWriteOnly
        {
            public string Value { set { } }
        }

        public class PropertyIndexer
        {
            public string this[int index]
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class PropertyIndexerReadOnly
        {
            public string this[int index]
            {
                get { return string.Empty; }
            }
        }

        public class PropertyIndexerWriteOnly
        {
            public string this[int index]
            {
                set { }
            }
        }

        public class PropertyIndexerMultipleParameters
        {
            public string this[int index, int order]
            {
                get { return string.Empty; }
                set { }
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore ValueParameterNotUsed
}