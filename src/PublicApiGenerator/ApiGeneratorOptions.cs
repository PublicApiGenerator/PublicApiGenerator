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
    /// Instructs the generator to include assembly level attributes.
    /// </summary>
    /// <remarks>Defaults to <see langword="true"/>.</remarks>
    public bool IncludeAssemblyAttributes { get; set; } = true;

    /// <summary>
    /// Allows to whitelist certain namespace prefixes.
    /// For example by default types found in Microsoft or System namespaces are not treated as part of the public API.
    /// This option has priority over <see cref="BlacklistedNamespacePrefixes"/>.
    /// </summary>
    /// <example>
    /// <code>
    /// var options = new DefaultApiGeneratorOptions
    /// {
    ///    WhitelistedNamespacePrefixes = new[] { "Microsoft.Whitelisted" }
    /// };
    /// </code>
    /// </example>
    public string[] WhitelistedNamespacePrefixes { get; set; } = _defaultWhitelistedNamespacePrefixes;

    /// <summary>
    /// Allows to blacklist certain namespace prefixes.
    /// By default types found in Microsoft or System namespaces are not treated as part of the public API.
    /// </summary>
    /// <example>
    /// <code>
    /// var options = new DefaultApiGeneratorOptions
    /// {
    ///    BlacklistedNamespacePrefixes = new[] { "System", "Microsoft", "ThirdParty" }
    /// };
    /// </code>
    /// </example>
    public string[] BlacklistedNamespacePrefixes { get; set; } = _defaultBlacklistedNamespacePrefixes;

    /// <summary>
    /// Allows to control whether to include extension methods into generated API even when
    /// containing class falls into <see cref="BlacklistedNamespacePrefixes"/> option. This
    /// option may be useful, for example, for those who writes extensions for IServiceCollection
    /// keeping them into Microsoft.Extensions.DependencyInjection namespace for better discoverability.
    /// </summary>
    /// <remarks>Defaults to <see langword="true"/>, i.e. extension methods are excluded from output.</remarks>
    public bool UseBlacklistedNamespacePrefixesForExtensionMethods { get; set; } = true;

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

    private static readonly string[] _defaultWhitelistedNamespacePrefixes = Array.Empty<string>();

    private static readonly string[] _defaultBlacklistedNamespacePrefixes = new[] { "System", "Microsoft" };
}
