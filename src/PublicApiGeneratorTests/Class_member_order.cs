using System;
using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Class_member_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_in_known_order_and_alphabetically()
        {
            // Fields, properties, events, methods, nested type (inc. delegates)
            AssertPublicApi<ClassMemberOrder>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassMemberOrder
    {
        public int Field1;
        public int Field2;
        public int IField1;
        public int iField2;
        public ClassMemberOrder() { }
        public int IProperty1 { get; set; }
        public int Property1 { get; set; }
        public int Property2 { get; set; }
        public int iProperty2 { get; set; }
        public event System.EventHandler Event1;
        public event System.EventHandler Event2;
        public event System.EventHandler IEvent1;
        public event System.EventHandler iEvent2;
        public void IMethod1() { }
        public void Method1() { }
        public void Method2() { }
        public void iMethod2() { }
        public delegate System.EventHandler Delegate1();
        public delegate System.EventHandler Delegate2();
        public delegate System.EventHandler IDelegate1();
        public delegate System.EventHandler iDelegate2();
    }
}");
        }

        [Fact]
        public void Should_output_in_known_order_with_nested_class()
        {
            // Fields, properties, events, methods
            AssertPublicApi<ClassMemberOrderAndNestedClass>(
@"namespace PublicApiGeneratorTests.Examples
{
        public class ClassMemberOrderAndNestedClass
    {
        public int Field1;
        public int Field2;
        public int IField1;
        public int iField2;
        public ClassMemberOrderAndNestedClass() { }
        public int IProperty1 { get; set; }
        public int Property1 { get; set; }
        public int Property2 { get; set; }
        public int iProperty2 { get; set; }
        public event System.EventHandler Event1;
        public event System.EventHandler Event2;
        public event System.EventHandler IEvent1;
        public event System.EventHandler iEvent2;
        public void IMethod1() { }
        public void Method1() { }
        public void Method2() { }
        public void iMethod2() { }
        public class AnotherNestedClass
        {
            public int Field;
            public int IField;
            public int iField;
            public AnotherNestedClass() { }
        }
        public class ClassMemberOrderAsNestedClass
        {
            public int Field1;
            public int Field2;
            public int IField1;
            public int iField2;
            public ClassMemberOrderAsNestedClass() { }
            public int IProperty1 { get; set; }
            public int Property1 { get; set; }
            public int Property2 { get; set; }
            public int iProperty2 { get; set; }
            public event System.EventHandler Event1;
            public event System.EventHandler Event2;
            public event System.EventHandler IEvent1;
            public event System.EventHandler iEvent2;
            public void IMethod1() { }
            public void Method1() { }
            public void Method2() { }
            public void iMethod2() { }
            public delegate System.EventHandler Delegate11();
            public delegate System.EventHandler Delegate21();
            public delegate System.EventHandler IDelegate11();
            public delegate System.EventHandler iDelegate21();
        }
        public delegate System.EventHandler Delegate1();
        public delegate System.EventHandler Delegate2();
        public delegate System.EventHandler iDelegate1();
        public delegate System.EventHandler iDelegate2();
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
        public class ClassMemberOrder
        {
            public int Field2;
            public int Field1;
            public int iField2;
            public int IField1;

            public event EventHandler Event2;
            public event EventHandler Event1;
            public event EventHandler iEvent2;
            public event EventHandler IEvent1;

            public delegate EventHandler Delegate2();
            public delegate EventHandler Delegate1();
            public delegate EventHandler iDelegate2();
            public delegate EventHandler IDelegate1();

            public int Property2 { get; set; }
            public int Property1 { get; set; }
            public int iProperty2 { get; set; }
            public int IProperty1 { get; set; }

            public void Method2() { }
            public void Method1() { }
            public void iMethod2() { }
            public void IMethod1() { }
        }

        public class ClassMemberOrderAndNestedClass
        {
            public int Field2;
            public int Field1;
            public int iField2;
            public int IField1;

            public event EventHandler Event2;
            public event EventHandler Event1;
            public event EventHandler iEvent2;
            public event EventHandler IEvent1;

            public delegate EventHandler Delegate2();
            public delegate EventHandler iDelegate2();

            public class ClassMemberOrderAsNestedClass
            {
                public int Field2;
                public int Field1;
                public int iField2;
                public int IField1;

                public event EventHandler Event2;
                public event EventHandler Event1;
                public event EventHandler iEvent2;
                public event EventHandler IEvent1;

                public delegate EventHandler Delegate21();
                public delegate EventHandler Delegate11();
                public delegate EventHandler iDelegate21();
                public delegate EventHandler IDelegate11();

                public int Property2 { get; set; }
                public int Property1 { get; set; }
                public int iProperty2 { get; set; }
                public int IProperty1 { get; set; }

                public void Method2() { }
                public void Method1() { }
                public void iMethod2() { }
                public void IMethod1() { }
            }

            public class AnotherNestedClass
            {
                public int Field;
                public int IField;
                public int iField;
            }

            public delegate EventHandler Delegate1();
            public delegate EventHandler iDelegate1();

            public int Property2 { get; set; }
            public int Property1 { get; set; }
            public int iProperty2 { get; set; }
            public int IProperty1 { get; set; }

            public void Method2() { }
            public void Method1() { }
            public void iMethod2() { }
            public void IMethod1() { }
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore EventNeverSubscribedTo.Global
    // ReSharper restore EventNeverInvoked
}