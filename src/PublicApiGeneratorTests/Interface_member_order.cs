using System;
using PublicApiGeneratorTests.Examples;
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
        int IProperty1 { get; set; }
        int Property1 { get; set; }
        int Property2 { get; set; }
        int iProperty2 { get; set; }
        public event System.EventHandler Event1;
        public event System.EventHandler Event2;
        public event System.EventHandler IEvent1;
        public event System.EventHandler iEvent2;
        void IMethod1();
        void Method1();
        void Method2();
        void iMethod2();
    }
}");
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
            event EventHandler iEvent2;
            event EventHandler IEvent1;

            int Property2 { get; set; }
            int Property1 { get; set; }
            int iProperty2 { get; set; }
            int IProperty1 { get; set; }

            void Method2();
            void Method1();
            void iMethod2();
            void IMethod1();
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore EventNeverSubscribedTo.Global
    // ReSharper restore EventNeverInvoked
}