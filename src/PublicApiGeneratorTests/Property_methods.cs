using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Property_methods : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_getter_and_setter()
        {
            AssertPublicApi<PropertyReadWrite>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyReadWrite
    {
        public PropertyReadWrite() { }
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_getter_only()
        {
            AssertPublicApi<PropertyReadOnly>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyReadOnly
    {
        public PropertyReadOnly() { }
        public string Value { get; }
    }
}");
        }

        [Fact]
        public void Should_output_setter_only()
        {
            AssertPublicApi<PropertyWriteOnly>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWriteOnly
    {
        public PropertyWriteOnly() { }
        public string Value { set; }
    }
}");
        }

        [Fact]
        public void Should_output_getter_and_init_only_setter()
        {
            AssertPublicApi<PropertyReadInit>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyReadInit
    {
        public PropertyReadInit() { }
        public string Value { get; init; }
    }
}");
        }

        [Fact]
        public void Should_output_init_only_setter_only()
        {
            AssertPublicApi<PropertyInitOnly>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyInitOnly
    {
        public PropertyInitOnly() { }
        public string Value { init; }
    }
}");
        }

        [Fact]
        public void Should_output_indexer_read_write()
        {
            AssertPublicApi<PropertyIndexer>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyIndexer
    {
        public PropertyIndexer() { }
        public string this[int index] { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_indexer_read_only()
        {
            AssertPublicApi<PropertyIndexerReadOnly>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyIndexerReadOnly
    {
        public PropertyIndexerReadOnly() { }
        public string this[int index] { get; }
    }
}");
        }

        [Fact]
        public void Should_output_indexer_write_only()
        {
            AssertPublicApi<PropertyIndexerWriteOnly>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyIndexerWriteOnly
    {
        public PropertyIndexerWriteOnly() { }
        public string this[int index] { set; }
    }
}");
        }

        [Fact]
        public void Should_output_indexer_multiple_parameters()
        {
            AssertPublicApi<PropertyIndexerMultipleParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyIndexerMultipleParameters
    {
        public PropertyIndexerMultipleParameters() { }
        public string this[int index, int order] { get; set; }
    }
}");
        }
    }

    // ReSharper disable ValueParameterNotUsed
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedParameter.Global
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

        public class PropertyReadInit
        {
            public string Value { get; init; }
        }

        public class PropertyInitOnly
        {
            public string Value { init { } }
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
    // ReSharper restore UnusedParameter.Global
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore ValueParameterNotUsed
}
