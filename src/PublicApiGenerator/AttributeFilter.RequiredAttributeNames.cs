namespace PublicApiGenerator;

internal partial class AttributeFilter
{
    /// <summary>
    /// Contains attributes that are internal that influence the compiler or runtime behavior.
    /// </summary>
    private static readonly HashSet<string> _internalAttributesThatAffectCompilerOrRuntimeBehavior = new()
    {
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
    };
}
