using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace OtherAssembly;

/// <summary>
/// Type to test <see cref="TypeForwardedToAttribute"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public class ForwardedClass
{
    /// <summary>
    /// Some property.
    /// </summary>
    public string? Name { get; set; }
}
