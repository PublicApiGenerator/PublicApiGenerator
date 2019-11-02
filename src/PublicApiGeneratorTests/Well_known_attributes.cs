using PublicApiGeneratorTests.Examples;
using System;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Well_known_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void NotNullIfNotNullAttribute()
        {
            AssertPublicApi<ClassWithInternalWellKnownAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
     public class ClassWithInternalWellKnownAttributes
    {
        [System.Diagnostics.CodeAnalysis.AllowNull]
        [System.Diagnostics.CodeAnalysis.DisallowNull]
        [System.Diagnostics.CodeAnalysis.MaybeNull]
        [System.Diagnostics.CodeAnalysis.NotNull]
        public object Field;
        public ClassWithInternalWellKnownAttributes() { }
        public bool BoolReturningMethod([System.Diagnostics.CodeAnalysis.MaybeNullWhen(true)] object a, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object b) { }
        [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull(""a"")]
        public object C(object a) { }
        [System.Diagnostics.CodeAnalysis.DoesNotReturn]
        public void MethodWithBoolParameter([System.Diagnostics.CodeAnalysis.DoesNotReturnIf(true)] bool a) { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        using System.Diagnostics.CodeAnalysis;

        public class ClassWithInternalWellKnownAttributes
        {
            [AllowNull]
            [DisallowNull]
            [MaybeNull]
            [NotNull]
            public object Field;

            [return: NotNullIfNotNull("a")]
            public object C(object a) => a;

            public bool BoolReturningMethod([MaybeNullWhen(true)] object a, [NotNullWhen(true)] object b) => false;

            [DoesNotReturn]
            public void MethodWithBoolParameter([DoesNotReturnIf(true)] bool a) { }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}

namespace System.Diagnostics.CodeAnalysis
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    internal sealed class AllowNullAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    internal sealed class DisallowNullAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    internal sealed class MaybeNullAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    internal sealed class NotNullAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class MaybeNullWhenAttribute : Attribute
    {
        public MaybeNullWhenAttribute(bool returnValue) { }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class NotNullWhenAttribute : Attribute
    {
        public NotNullWhenAttribute(bool returnValue) { }
    }

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true)]
    internal sealed class NotNullIfNotNullAttribute : Attribute
    {
        public NotNullIfNotNullAttribute(string parameterName) { }
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class DoesNotReturnAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class DoesNotReturnIfAttribute : Attribute
    {
        public DoesNotReturnIfAttribute(bool parameterValue) { }
    }
}
