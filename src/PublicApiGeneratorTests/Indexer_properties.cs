using System.Runtime.CompilerServices;
using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Indexer_properties : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_indexer()
        {
            AssertPublicApi<ClassWithIndexer>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithIndexer
    {
        public ClassWithIndexer() { }
        public int this[int x] { get; }
    }
}");
        }

        [Fact]
        public void Should_output_named_indexer()
        {
            AssertPublicApi<ClassWithNamedIndexer>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithNamedIndexer
    {
        public ClassWithNamedIndexer() { }
        [System.Runtime.CompilerServices.IndexerName(""Bar"")]
        public int this[int x] { get; }
    }
}");
        }
    }


    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable ValueParameterNotUsed
    namespace Examples
    {
        public class ClassWithIndexer
        {
            public int this[int x] => x;
        }

        public class ClassWithNamedIndexer
        {
            [IndexerName("Bar")]
            public int this[int x] => x;
        }
    }
    // ReSharper restore ValueParameterNotUsed
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}
