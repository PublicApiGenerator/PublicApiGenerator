using PublicApiGeneratorTests.Examples;
using System.Collections.Generic;
using Xunit;

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
    public class static StringExtensions
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
    public class static GenericExtensions
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
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}