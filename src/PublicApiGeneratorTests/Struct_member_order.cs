using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Struct_member_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_in_known_order_and_alphabetically()
        {
            var options = new DefaultApiGeneratorOptions
            {
                ExcludeAttributes = ["System.Runtime.CompilerServices.IsReadOnlyAttribute"]
            };

            // Fields, properties, events, methods, nested type (inc. delegates)
            AssertPublicApi<StructMemberOrder>(
@"namespace PublicApiGeneratorTests.Examples
{
    public struct StructMemberOrder
    {
        public int Field1;
        public int Field2;
        public int IField2;
        public int iField1;
        public int IProperty2 { get; set; }
        public int Property1 { get; set; }
        public int Property2 { get; set; }
        public int iProperty1 { get; set; }
        public event System.EventHandler Event1;
        public event System.EventHandler Event2;
        public event System.EventHandler IEvent2;
        public event System.EventHandler iEvent1;
        public void IMethod2() { }
        public void Method1() { }
        public void Method2() { }
        public void iMethod1() { }
        public delegate System.EventHandler Delegate1();
        public delegate System.EventHandler Delegate2();
        public delegate System.EventHandler IDelegate2();
        public delegate System.EventHandler iDelegate1();
    }
}", options);
        }

        [Fact]
        public void Should_output_in_known_order_with_nested_class()
        {
            var options = new DefaultApiGeneratorOptions
            {
                ExcludeAttributes = ["System.Runtime.CompilerServices.IsReadOnlyAttribute"]
            };

            // Fields, properties, events, methods
            AssertPublicApi<StructMemberOrderAndNestedClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public struct StructMemberOrderAndNestedClass
    {
        public int Field1;
        public int Field2;
        public int IField2;
        public int iField1;
        public int IProperty2 { get; set; }
        public int Property1 { get; set; }
        public int Property2 { get; set; }
        public int iProperty1 { get; set; }
        public event System.EventHandler Event1;
        public event System.EventHandler Event2;
        public event System.EventHandler IEvent2;
        public event System.EventHandler iEvent1;
        public void IMethod2() { }
        public void Method1() { }
        public void Method2() { }
        public void iMethod1() { }
        public struct AnotherNestedStruct
        {
            public int Field;
            public int IField;
            public int iField;
        }
        public class ClassMemberOrderAsNestedClass
        {
            public int Field1;
            public int Field2;
            public int IField2;
            public int iField1;
            public ClassMemberOrderAsNestedClass() { }
            public int IProperty2 { get; set; }
            public int Property1 { get; set; }
            public int Property2 { get; set; }
            public int iProperty1 { get; set; }
            public event System.EventHandler Event1;
            public event System.EventHandler Event2;
            public event System.EventHandler IEvent2;
            public event System.EventHandler iEvent1;
            public void IMethod2() { }
            public void Method1() { }
            public void Method2() { }
            public void iMethod1() { }
            public delegate System.EventHandler Delegate11();
            public delegate System.EventHandler Delegate21();
            public delegate System.EventHandler IDelegate21();
            public delegate System.EventHandler iDelegate11();
        }
        public delegate System.EventHandler Delegate1();
        public delegate System.EventHandler Delegate2();
        public delegate System.EventHandler IDelegate2();
        public delegate System.EventHandler iDelegate1();
    }
}", options);
        }
    }

    namespace Examples
    {
        public struct StructMemberOrder
        {
            public int Field2;
            public int Field1;
            public int IField2;
            public int iField1;

            public event EventHandler Event2;
            public event EventHandler Event1;
            public event EventHandler IEvent2;
            public event EventHandler iEvent1;

            public delegate EventHandler Delegate2();
            public delegate EventHandler Delegate1();
            public delegate EventHandler IDelegate2();
            public delegate EventHandler iDelegate1();

            public int Property2 { get; set; }
            public int Property1 { get; set; }
            public int IProperty2 { get; set; }
            public int iProperty1 { get; set; }

            public void Method2() { }
            public void Method1() { }
            public void IMethod2() { }
            public void iMethod1() { }
        }

        public struct StructMemberOrderAndNestedClass
        {
            public int Field2;
            public int Field1;
            public int IField2;
            public int iField1;

            public event EventHandler Event2;
            public event EventHandler Event1;
            public event EventHandler IEvent2;
            public event EventHandler iEvent1;

            public delegate EventHandler Delegate2();
            public delegate EventHandler IDelegate2();

            public class ClassMemberOrderAsNestedClass
            {
                public int Field2;
                public int Field1;
                public int IField2;
                public int iField1;

                public event EventHandler Event2;
                public event EventHandler Event1;
                public event EventHandler IEvent2;
                public event EventHandler iEvent1;

                public delegate EventHandler Delegate21();
                public delegate EventHandler Delegate11();
                public delegate EventHandler IDelegate21();
                public delegate EventHandler iDelegate11();

                public int Property2 { get; set; }
                public int Property1 { get; set; }
                public int IProperty2 { get; set; }
                public int iProperty1 { get; set; }

                public void Method2() { }
                public void Method1() { }
                public void IMethod2() { }
                public void iMethod1() { }
            }

            public struct AnotherNestedStruct
            {
                public int Field;
                public int IField;
                public int iField;
            }

            public delegate EventHandler Delegate1();
            public delegate EventHandler iDelegate1();

            public int Property2 { get; set; }
            public int Property1 { get; set; }
            public int IProperty2 { get; set; }
            public int iProperty1 { get; set; }

            public void Method2() { }
            public void Method1() { }
            public void IMethod2() { }
            public void iMethod1() { }
        }
    }
}
