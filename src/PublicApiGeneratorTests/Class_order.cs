using System.Globalization;
using PublicApiGeneratorTests.Examples;
using PublicApiGeneratorTests.Examples_Ordinal;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Class_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_classes_in_alphabetical_order()
        {
            AssertPublicApi(new[] { typeof(MM_Class), typeof(ZZ_Class), typeof(AA_Class) },
@"namespace PublicApiGeneratorTests.Examples
{
    public class AA_Class
    {
        public AA_Class() { }
    }
    public class MM_Class
    {
        public MM_Class() { }
    }
    public class ZZ_Class
    {
        public ZZ_Class() { }
    }
}");
        }

        [Fact]
        public void Should_order_ordinal()
        {
            var currentCulture = CultureInfo.DefaultThreadCurrentCulture;

            try
            {
                var apiWithCurrentCulture = GeneratePublicApi(new[]
                {
                    typeof(I_Class),
                    typeof(i_Class)
                });

                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");
                var apiWithDifferentCulture = GeneratePublicApi(new[]
                {
                    typeof(I_Class),
                    typeof(i_Class)
                });

                Assert.Equal(apiWithCurrentCulture, apiWithDifferentCulture, ignoreCase: false, ignoreLineEndingDifferences: true,
                    ignoreWhiteSpaceDifferences: true);
            }
            finally
            {
                CultureInfo.DefaultThreadCurrentCulture = currentCulture;
            }
        }
    }

    // ReSharper disable InconsistentNaming
    namespace Examples
    {
        public class MM_Class
        {
        }

        public class ZZ_Class
        {
        }

        public class AA_Class
        {
        }
    }
    namespace Examples_Ordinal
    {
        public class I_Class
        {
        }

        public class i_Class
        {
        }
    }
    // ReSharper restore InconsistentNaming
}