using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Method_extensions : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_extension_methods()
        {
            // Note the class static reverse order hack
            AssertPublicApi(typeof(StringExtensions),
@"namespace PublicApiGeneratorTests.Examples
{
    public static class StringExtensions
    {
        public static bool CheckLength(this string value, int length) { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_extension_methods()
        {
            AssertPublicApi(typeof(GenericExtensions),
@"namespace PublicApiGeneratorTests.Examples
{
    public static class GenericExtensions
    {
        public static PublicApiGeneratorTests.Examples.Configurator<T> Add<T>(this PublicApiGeneratorTests.Examples.Configurator<T> configurator)
            where T :  class { }
        public static PublicApiGeneratorTests.Examples.Configurator<T> Add<T, U>(this PublicApiGeneratorTests.Examples.Configurator<T> configurator)
            where U :  class, System.Collections.Generic.IComparer<T>, System.Collections.Generic.IEnumerable<U> { }
        public static void Add<T, U>(this string s)
            where U :  class, System.Collections.Generic.IComparer<T>, System.Collections.Generic.IEnumerable<U> { }
    }
}");
        }

        [Fact]
        public void Should_output_extension_methods_with_nullable()
        {
            AssertPublicApi(typeof(ExtensionMethodWithNullable),
@"namespace PublicApiGeneratorTests.Examples
{
    public static class ExtensionMethodWithNullable
    {
            public static int? Int(this object @object, int? value = default) { }
            public static long? Long(this object @object, long? value = default) { }
            public static System.TimeSpan? Time(this object @object, System.TimeSpan? timeSpan = default) { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public static class StringExtensions
        {
            public static bool CheckLength(this string value, int length)
            {
                return value.Length == length;
            }
        }


        public class Configurator<TConfig> { }

        public static class GenericExtensions
        {
            public static Configurator<T> Add<T>(this Configurator<T> configurator) where T : class => configurator;

            public static Configurator<T> Add<T, U>(this Configurator<T> configurator) where U : class, IComparer<T>, IEnumerable<U> => configurator;

            public static void Add<T, U>(this string s) where U : class, IComparer<T>, IEnumerable<U> { }
        }

        public static class ExtensionMethodWithNullable
        {
            public static TimeSpan? Time(this object @object, TimeSpan? timeSpan = null) => null;

            public static int? Int(this object @object, int? @value = null) => null;

            public static long? Long(this object @object, long? @value = null) => null;
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}
