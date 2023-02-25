using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Property_visibility :  ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_public_property()
        {
            AssertPublicApi<ClassWithPublicProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicProperty
    {
        public ClassWithPublicProperty() { }
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_protected_property()
        {
            AssertPublicApi<ClassWithProtectedProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedProperty
    {
        public ClassWithProtectedProperty() { }
        protected string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_protected_internal_property()
        {
            AssertPublicApi<ClassWithProtectedInternalProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedInternalProperty
    {
        public ClassWithProtectedInternalProperty() { }
        protected string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_not_output_private_protected_property()
        {
            AssertPublicApi<ClassWithPrivateProtectedProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPrivateProtectedProperty
    {
        public ClassWithPrivateProtectedProperty() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_private_property()
        {
            AssertPublicApi<ClassWithPrivateProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPrivateProperty
    {
        public ClassWithPrivateProperty() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_internal_property()
        {
            AssertPublicApi<ClassWithInternalProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithInternalProperty
    {
        public ClassWithInternalProperty() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_private_setter_for_public_property()
        {
            AssertPublicApi<ClassWithPublicGetterPrivateSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicGetterPrivateSetter
    {
        public ClassWithPublicGetterPrivateSetter() { }
        public string Value1 { get; }
    }
}");
        }

        [Fact]
        public void Should_not_output_internal_setter_for_public_property()
        {
            AssertPublicApi<ClassWithPublicGetterInternalSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicGetterInternalSetter
    {
        public ClassWithPublicGetterInternalSetter() { }
        public string Value1 { get; }
    }
}");
        }

        [Fact]
        public void Should_output_protected_setter_for_public_property()
        {
            AssertPublicApi<ClassWithPublicGetterProtectedSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicGetterProtectedSetter
    {
        public ClassWithPublicGetterProtectedSetter() { }
        public string Value1 { get; set; }
    }
}");
        }

        [Fact(Skip = "Not supported by CodeDOM")]
        [Trait("TODO", "Property method modifiers not supported by CodeDOM")]
        public void Should_output_protected_setter_for_public_property_with_correct_modifier()
        {
            AssertPublicApi<ClassWithPublicGetterProtectedSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicGetterProtectedSetter
    {
        public ClassWithPublicGetterProtectedSetter() { }
        public string Value1 { get; protected set; }
    }
}");
        }

        [Fact]
        public void Should_output_protected_internal_setter_for_public_property()
        {
            AssertPublicApi<ClassWithPublicGetterProtectedInternalSetter>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicGetterProtectedInternalSetter
    {
        public ClassWithPublicGetterProtectedInternalSetter() { }
        public string Value1 { get; set; }
    }
}");
        }

        [Fact]
        public void Should_not_output_private_protected_setter_for_public_property()
        {
            AssertPublicApi<ClassWithPublicGetterPrivateProtectedSetter>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicGetterPrivateProtectedSetter
    {
        public ClassWithPublicGetterPrivateProtectedSetter() { }
        public string Value1 { get; }
    }
}");
        }

        [Fact(Skip = "Not supported by CodeDOM")]
        [Trait("TODO", "Property method modifiers not supported by CodeDOM")]
        public void Should_output_protected_internal_setter_for_public_property_with_correct_modifier()
        {
            AssertPublicApi<ClassWithPublicGetterProtectedInternalSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicGetterProtectedInternalSetter
    {
        public ClassWithPublicGetterProtectedInternalSetter() { }
        public string Value1 { get; protected internal set; }
    }
}");
        }

        [Fact]
        public void Should_not_output_private_getter_for_public_property()
        {
            AssertPublicApi<ClassWithPrivateGetterPublicSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPrivateGetterPublicSetter
    {
        public ClassWithPrivateGetterPublicSetter() { }
        public string Value1 { set; }
    }
}");
        }

        [Fact]
        public void Should_not_output_internal_getter_for_public_property()
        {
            AssertPublicApi<ClassWithInternalGetterPublicSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithInternalGetterPublicSetter
    {
        public ClassWithInternalGetterPublicSetter() { }
        public string Value1 { set; }
    }
}");
        }

        [Fact]
        public void Should_output_protected_getter_for_public_property()
        {
            // TODO: CodeDOM doesn't support access modifiers on setters or getters
            // Doesn't really matter for diffing APIs, though
            AssertPublicApi<ClassWithProtectedGetterPublicSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedGetterPublicSetter
    {
        public ClassWithProtectedGetterPublicSetter() { }
        public string Value1 { get; set; }
    }
}");
        }

        [Fact(Skip = "Not supported by CodeDOM")]
        [Trait("TODO", "Property method modifiers not supported by CodeDOM")]
        public void Should_output_protected_getter_for_public_property_with_correct_modifier()
        {
            AssertPublicApi<ClassWithProtectedGetterPublicSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedGetterPublicSetter
    {
        public ClassWithProtectedGetterPublicSetter() { }
        public string Value1 { protected get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_protected_internal_getter_for_public_property()
        {
            // TODO: CodeDOM doesn't support access modifiers on setters or getters
            // Doesn't really matter for diffing APIs, though
            AssertPublicApi<ClassWithProtectedInternalGetterPublicSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedInternalGetterPublicSetter
    {
        public ClassWithProtectedInternalGetterPublicSetter() { }
        public string Value1 { get; set; }
    }
}");
        }

        [Fact(Skip = "Not supported by CodeDOM")]
        [Trait("TODO", "Property method modifiers not supported by CodeDOM")]
        public void Should_output_protected_internal_getter_for_public_property_with_correct_modifier()
        {
            AssertPublicApi<ClassWithProtectedInternalGetterPublicSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedInternalGetterPublicSetter
    {
        public ClassWithProtectedInternalGetterPublicSetter() { }
        public string Value1 { protected get; set; }
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedMember.Local
    // ReSharper disable UnusedAutoPropertyAccessor.Local
    namespace Examples
    {
        public class ClassWithPublicProperty
        {
            public string Value { get; set; }
        }

        public class ClassWithProtectedProperty
        {
            protected string Value { get; set; }
        }

        public class ClassWithPrivateProperty
        {
            private string Value { get; set; }
        }

        public class ClassWithInternalProperty
        {
            internal string Value { get; set; }
        }

        public class ClassWithProtectedInternalProperty
        {
            protected internal string Value { get; set; }
        }

        public class ClassWithPrivateProtectedProperty
        {
            private protected string Value { get; set; }
        }

        public class ClassWithPublicGetterPrivateSetter
        {
            public string Value1 { get; private set; }
        }

        public class ClassWithPublicGetterInternalSetter
        {
            public string Value1 { get; internal set; }
        }

        public class ClassWithPublicGetterProtectedSetter
        {
            public string Value1 { get; protected set; }
        }

        public class ClassWithPublicGetterProtectedInternalSetter
        {
            public string Value1 { get; protected internal set; }
        }

        public class ClassWithPublicGetterPrivateProtectedSetter
        {
            public string Value1 { get; private protected set; }
        }

        public class ClassWithPrivateGetterPublicSetter
        {
            public string Value1 { private get; set; }
        }

        public class ClassWithInternalGetterPublicSetter
        {
            public string Value1 { internal get; set; }
        }

        public class ClassWithProtectedGetterPublicSetter
        {
            public string Value1 { protected get; set; }
        }

        public class ClassWithProtectedInternalGetterPublicSetter
        {
            public string Value1 { protected internal get; set; }
        }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Local
    // ReSharper restore UnusedMember.Local
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}
