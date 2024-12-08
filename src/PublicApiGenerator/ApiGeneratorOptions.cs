namespace PublicApiGenerator;

/// <summary>
/// Options to influence the ApiGenerator output.
/// </summary>
public class ApiGeneratorOptions
{
    /// <summary>
    /// Allows to control which types of the generated assembly should be included.
    /// If this option is specified all other types found in the assembly that are not present here will be excluded.
    /// </summary>
    public Type[]? IncludeTypes { get; set; }

    /// <summary>
    /// Allows to control which types of the generated assembly should be explicitly excluded.
    /// </summary>
    public Type[]? ExcludeTypes { get; set; }

    /// <summary>
    /// Instructs the generator to include assembly level attributes.
    /// </summary>
    /// <remarks>Defaults to <see langword="true"/>.</remarks>
    public bool IncludeAssemblyAttributes { get; set; } = true;

    /// <summary>
    /// Allows to print APIs in certain namespace prefixes.
    /// For example by default types found in Microsoft or System namespaces are not treated as part of the public API.
    /// This option has priority over <see cref="DenyNamespacePrefixes"/>.
    /// </summary>
    /// <example>
    /// <code>
    /// var options = new DefaultApiGeneratorOptions
    /// {
    ///    AllowNamespacePrefixes = new[] { "Microsoft" }
    /// };
    /// </code>
    /// </example>
    public string[] AllowNamespacePrefixes { get; set; } = _defaultAllowNamespacePrefixes;

    /// <summary>
    /// Denies to print APIs in certain namespace prefixes.
    /// By default types found in Microsoft or System namespaces are not treated as part of the public API.
    /// </summary>
    /// <example>
    /// <code>
    /// var options = new DefaultApiGeneratorOptions
    /// {
    ///    DenyNamespacePrefixes = new[] { "System", "Microsoft", "ThirdParty" }
    /// };
    /// </code>
    /// </example>
    public string[] DenyNamespacePrefixes { get; set; } = _defaultDenyNamespacePrefixes;

    /// <summary>
    /// Allows to control whether to include extension methods into generated API even when
    /// containing class falls into <see cref="DenyNamespacePrefixes"/> option. This
    /// option may be useful, for example, for those who writes extensions for IServiceCollection
    /// keeping them into Microsoft.Extensions.DependencyInjection namespace for better discoverability.
    /// </summary>
    /// <remarks>Defaults to <see langword="true"/>, i.e. extension methods are excluded from output.</remarks>
    public bool UseDenyNamespacePrefixesForExtensionMethods { get; set; } = true;

    /// <summary>
    /// Allows to exclude attributes by specifying the fullname of the attribute to exclude.
    /// </summary>
    /// <example>
    ///<code>
    /// var options = new DefaultApiGeneratorOptions
    /// {
    ///    ExcludeAttributes = new[] { "PublicApiGeneratorTests.Examples.SimpleAttribute" }
    /// };
    /// </code>
    /// </example>
    public string[]? ExcludeAttributes { get; set; }

    private static readonly string[] _defaultAllowNamespacePrefixes = [];

    private static readonly string[] _defaultDenyNamespacePrefixes = ["System", "Microsoft"];

    /// <summary>
    /// Indentation string. Defaults to 4 whitespace.
    /// </summary>
    public string IndentString { get; set; } = "    ";

    /// <summary>
    /// Style for braces. Available values: C, Block. Defaults to C.
    /// </summary>
    public string BracingStyle { get; set; } = "C";
}
