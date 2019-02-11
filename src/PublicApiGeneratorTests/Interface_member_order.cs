using System;
using System.Globalization;
using PublicApiGeneratorTests.Examples;
using PublicApiGeneratorTests.Examples_Ordinal;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Interface_member_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_in_known_order_and_alphabetically()
        {
            // Yes, CodeDOM inserts public for events...
            AssertPublicApi<IInterfaceMemberOrder>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IInterfaceMemberOrder
    {
        int Property1 { get; set; }
        int Property2 { get; set; }
        public event System.EventHandler Event1;
        public event System.EventHandler Event2;
        void Method1();
        void Method2();
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
                    typeof(IInterfaceMemberOrdinal)
                });

                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");
                var apiWithDifferentCulture = GeneratePublicApi(new[]
                {
                    typeof(IInterfaceMemberOrdinal)
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

    // ReSharper disable EventNeverInvoked
    // ReSharper disable EventNeverSubscribedTo.Global
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public interface IInterfaceMemberOrder
        {
            event EventHandler Event2;
            event EventHandler Event1;

            int Property2 { get; set; }
            int Property1 { get; set; }

            void Method2();
            void Method1();
        }
    }
    namespace Examples_Ordinal
    {
        public interface IInterfaceMemberOrdinal
        {
            event EventHandler IEvent;
            event EventHandler iEvent;

            int IProperty { get; set; }
            int iProperty { get; set; }

            void IMethod();
            void iMethod();
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore EventNeverSubscribedTo.Global
    // ReSharper restore EventNeverInvoked
}