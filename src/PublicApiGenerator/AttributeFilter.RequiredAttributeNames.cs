namespace PublicApiGenerator;

internal partial class AttributeFilter
{
    /// <summary>
    /// Contains attributes that influence the compiler or runtime behavior.
    /// Note that such attributes may be public or internal depending on .NET version.
    /// Also they may be polyfilled by packages like PolySharp/Polyfill and so also
    /// have public or internal access modifier depending on applied settings. Placing
    /// an attribute in this list indicates that it should be visible in the public API
    /// despite its access modifier.
    /// </summary>
    private static readonly HashSet<string> _attributesThatAffectCompilerOrRuntimeBehavior =
    [
        // Nullability
        "System.Diagnostics.CodeAnalysis.AllowNullAttribute",
        "System.Diagnostics.CodeAnalysis.DisallowNullAttribute",
        "System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute",
        "System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute",
        "System.Diagnostics.CodeAnalysis.MaybeNullAttribute",
        "System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute",
        "System.Diagnostics.CodeAnalysis.NotNullAttribute",
        "System.Diagnostics.CodeAnalysis.NotNullIfNotNullAttribute",
        "System.Diagnostics.CodeAnalysis.NotNullWhenAttribute",
        // Serializable
        "System.SerializableAttribute",
        // Caller information propagation to default arguments
        "System.Runtime.CompilerServices.CallerArgumentExpressionAttribute",
        "System.Runtime.CompilerServices.CallerFilePath",
        "System.Runtime.CompilerServices.CallerLineNumberAttribute",
        "System.Runtime.CompilerServices.CallerMemberName",
        "System.Runtime.CompilerServices.ReferenceAssemblyAttribute",
        // Native sized integers
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types#native-sized-integers
        "System.Runtime.CompilerServices.NativeIntegerAttribute",
        // required keyword
        // Note that RequiredMemberAttribute class is public only since NET7, so we have to add it here,
        // otherwise ShouldIncludeAttribute method will filter it out.
        "System.Runtime.CompilerServices.RequiredMemberAttribute",
        // Platform attributes
        "System.Runtime.Versioning.SupportedOSPlatformAttribute",
        "System.Runtime.Versioning.UnsupportedOSPlatformAttribute",
        "System.Runtime.Versioning.SupportedOSPlatformGuardAttribute",
        "System.Runtime.Versioning.UnsupportedOSPlatformGuardAttribute",
        "System.Runtime.Versioning.ObsoletedOSPlatformAttribute",
    ];
}
