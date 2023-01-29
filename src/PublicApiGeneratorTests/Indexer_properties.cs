using System.Runtime.CompilerServices;
using PublicApiGeneratorTests.Examples;

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
        public void Should_output_abstract_read_only_indexer()
        {
            AssertPublicApi<ClassWithAbstractReadOnlyIndexer>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractReadOnlyIndexer
    {
        protected ClassWithAbstractReadOnlyIndexer() { }
        public abstract int this[int x] { get; }
    }
}");
        }

        [Fact]
        public void Should_output_abstract_read_write_indexer()
        {
            AssertPublicApi<ClassWithAbstractReadWriteIndexer>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractReadWriteIndexer
    {
        protected ClassWithAbstractReadWriteIndexer() { }
        public abstract int this[int x] { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_abstract_write_only_indexer()
        {
            AssertPublicApi<ClassWithAbstractWriteOnlyIndexer>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractWriteOnlyIndexer
    {
        protected ClassWithAbstractWriteOnlyIndexer() { }
        public abstract int this[int x] { set; }
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

        [Fact]
        public void Should_output_named_indexer_with_getset()
        {
            AssertPublicApi<ClassWithNamedIndexerGetSet>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithNamedIndexerGetSet
    {
        public ClassWithNamedIndexerGetSet() { }
        [System.Runtime.CompilerServices.IndexerName(""Bar"")]
        public int this[int x] { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_other_properties_when_indexer_exists()
        {
            AssertPublicApi<InterfaceWithIndexerAndAnotherProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public interface InterfaceWithIndexerAndAnotherProperty
    {
        object this[string key] { get; }
        string Property { get; }
    }
}");
        }

        [Fact]
        public void Should_output_other_properties_when_named_indexer_exists()
        {
            AssertPublicApi<InterfaceWithNamedIndexerAndAnotherProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public interface InterfaceWithNamedIndexerAndAnotherProperty
    {
        [System.Runtime.CompilerServices.IndexerName(""Bar"")]
        object this[string key] { get; }
        string Property { get; }
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

        public abstract class ClassWithAbstractReadOnlyIndexer
        {
            public abstract int this[int x] { get; }
        }

        public abstract class ClassWithAbstractReadWriteIndexer
        {
            public abstract int this[int x] { get; set; }
        }

        public abstract class ClassWithAbstractWriteOnlyIndexer
        {
            public abstract int this[int x] { set; }
        }

        public class ClassWithNamedIndexer
        {
            [IndexerName("Bar")]
            public int this[int x] => x;
        }

        public class ClassWithNamedIndexerGetSet
        {
            private int y;

            [IndexerName("Bar")]
            public int this[int x]
            {
                get => y;
                set => y = value;
            }
        }

        public interface InterfaceWithIndexerAndAnotherProperty
        {
            string Property { get; }

            object this[string key] { get; }
        }

        public interface InterfaceWithNamedIndexerAndAnotherProperty
        {
            string Property { get; }

            [IndexerName("Bar")]
            object this[string key] { get; }
        }
    }
    // ReSharper restore ValueParameterNotUsed
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}
