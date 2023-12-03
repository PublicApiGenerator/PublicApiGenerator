using Mono.Cecil;

namespace PublicApiGenerator;

internal sealed partial class AttributeFilter
{
    private readonly HashSet<string> _excludedAttributes;

    public AttributeFilter(IEnumerable<string>? excludedAttributes)
    {
        _excludedAttributes = excludedAttributes is null
            ? _attributesNotRelevantForThePublicApi
            : new HashSet<string>(_attributesNotRelevantForThePublicApi.Concat(excludedAttributes));
    }

    public bool ShouldIncludeAttribute(CustomAttribute attribute)
    {
        var attributeTypeDefinition = attribute.AttributeType.Resolve();

        return attributeTypeDefinition != null
               && !_excludedAttributes.Contains(attribute.AttributeType.FullName)
               && (attributeTypeDefinition.IsPublic || _internalAttributesThatAffectCompilerOrRuntimeBehavior.Contains(attribute.AttributeType.FullName));
    }
}
