using System.Collections.Generic;

namespace PublicApiGenerator
{
    partial class AttributeFilter
    {
        private static readonly HashSet<string> RequiredAttributeNames = new HashSet<string>
        {
            "System.Diagnostics.CodeAnalysis.AllowNullAttribute",
            "System.Diagnostics.CodeAnalysis.DisallowNullAttribute",
            "System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute",
            "System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute",
            "System.Diagnostics.CodeAnalysis.MaybeNullAttribute",
            "System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute",
            "System.Diagnostics.CodeAnalysis.NotNullAttribute",
            "System.Diagnostics.CodeAnalysis.NotNullIfNotNullAttribute",
            "System.Diagnostics.CodeAnalysis.NotNullWhenAttribute"
        };
    }
}
