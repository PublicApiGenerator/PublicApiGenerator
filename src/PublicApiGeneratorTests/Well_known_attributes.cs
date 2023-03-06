using System.Diagnostics.CodeAnalysis;
using PublicApiGeneratorTests.Examples;

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

    namespace Examples
    {
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
            public void MethodWithBoolParameter([DoesNotReturnIf(true)] bool a) => throw null;
        }
    }
}
