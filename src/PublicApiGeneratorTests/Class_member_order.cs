using System;
using System.Globalization;
using PublicApiGeneratorTests.Examples;
using PublicApiGeneratorTests.Examples_Ordinal;
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
        public ClassMemberOrder() { }
        public int Property1 { get; set; }
        public int Property2 { get; set; }
        public event System.EventHandler Event1;
        public event System.EventHandler Event2;
        public void Method1() { }
        public void Method2() { }
        public delegate System.EventHandler Delegate1();
        public delegate System.EventHandler Delegate2();
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
        public ClassMemberOrderAndNestedClass() { }
        public int Property1 { get; set; }
        public int Property2 { get; set; }
        public event System.EventHandler Event1;
        public event System.EventHandler Event2;
        public void Method1() { }
        public void Method2() { }
        public class AnotherNestedClass
        {
            public int Field;
            public AnotherNestedClass() { }
        }
        public class ClassMemberOrderAsNestedClass
        {
            public int Field1;
            public int Field2;
            public ClassMemberOrderAsNestedClass() { }
            public int Property1 { get; set; }
            public int Property2 { get; set; }
            public event System.EventHandler Event1;
            public event System.EventHandler Event2;
            public void Method1() { }
            public void Method2() { }
            public delegate System.EventHandler Delegate11();
            public delegate System.EventHandler Delegate21();
        }
        public delegate System.EventHandler Delegate1();
        public delegate System.EventHandler Delegate2();
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
                    typeof(ClassMemberOrdinal)
                });

                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");
                var apiWithDifferentCulture = GeneratePublicApi(new[]
                {
                    typeof(ClassMemberOrdinal)
                });

                Assert.Equal(apiWithCurrentCulture, apiWithDifferentCulture, ignoreCase: false, ignoreLineEndingDifferences: true,
                    ignoreWhiteSpaceDifferences: true);
            }
            finally
            {
                CultureInfo.DefaultThreadCurrentCulture = currentCulture;
            }
        }

        [Fact]
        public void Should_order_ordinal_with_nested_class()
        {
            var currentCulture = CultureInfo.DefaultThreadCurrentCulture;

            try
            {
                var apiWithCurrentCulture = GeneratePublicApi(new[]
                {
                    typeof(ClassMemberOrdinalAndNestedClass)
                });

                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");
                var apiWithDifferentCulture = GeneratePublicApi(new[]
                {
                    typeof(ClassMemberOrdinalAndNestedClass)
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
        public class ClassMemberOrder
        {
            public int Field2;
            public int Field1;

            public event EventHandler Event2;
            public event EventHandler Event1;

            public delegate EventHandler Delegate2();
            public delegate EventHandler Delegate1();

            public int Property2 { get; set; }
            public int Property1 { get; set; }

            public void Method2() { }
            public void Method1() { }
        }

        public class ClassMemberOrderAndNestedClass
        {
            public int Field2;
            public int Field1;

            public event EventHandler Event2;
            public event EventHandler Event1;

            public delegate EventHandler Delegate2();

            public class ClassMemberOrderAsNestedClass
            {
                public int Field2;
                public int Field1;

                public event EventHandler Event2;
                public event EventHandler Event1;

                public delegate EventHandler Delegate21();
                public delegate EventHandler Delegate11();

                public int Property2 { get; set; }
                public int Property1 { get; set; }

                public void Method2() { }
                public void Method1() { }
            }

            public class AnotherNestedClass
            {
                public int Field;
            }

            public delegate EventHandler Delegate1();

            public int Property2 { get; set; }
            public int Property1 { get; set; }

            public void Method2() { }
            public void Method1() { }
        }
    }
    namespace Examples_Ordinal
    {
        public class ClassMemberOrdinal
        {
            public int IField;
            public int iField;

            public event EventHandler IEvent;
            public event EventHandler iEvent;

            public delegate EventHandler IDelegate();
            public delegate EventHandler iDelegate();

            public int IProperty { get; set; }
            public int iProperty { get; set; }

            public void IMethod() { }
            public void iMethod() { }
        }

        public class ClassMemberOrdinalAndNestedClass
        {
            public int IField;
            public int iField;

            public event EventHandler IEvent;
            public event EventHandler iEvent;

            public delegate EventHandler IDelegate();

            public class ClassMemberOrderAsNestedClass
            {
                public int IField;
                public int iField;

                public event EventHandler IEvent;
                public event EventHandler iEvent;

                public delegate EventHandler IDelegate1();
                public delegate EventHandler iDelegate1();

                public int IProperty { get; set; }
                public int iProperty { get; set; }

                public void IMethod() { }
                public void iMethod() { }
            }

            public class AnotherNestedClass
            {
                public int IField;
                public int iField;
            }

            public delegate EventHandler iDelegate();

            public int IProperty { get; set; }
            public int iProperty { get; set; }

            public void iMethod() { }
            public void IMethod() { }
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore EventNeverSubscribedTo.Global
    // ReSharper restore EventNeverInvoked
}