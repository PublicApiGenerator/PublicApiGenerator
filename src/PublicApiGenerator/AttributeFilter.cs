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

    public bool ShouldIncludeAttribute(CustomAttribute attribute, ICustomAttributeProvider parent)
    {
        var attributeTypeDefinition = attribute.AttributeType.Resolve();

        var should = attributeTypeDefinition != null
               && !_excludedAttributes.Contains(attribute.AttributeType.FullName)
               && (attributeTypeDefinition.IsPublic || _internalAttributesThatAffectCompilerOrRuntimeBehavior.Contains(attribute.AttributeType.FullName));

        // Do not print compiler-generated ObsoleteAttribute for readonly ref struct, see https://github.com/PublicApiGenerator/PublicApiGenerator/issues/104
        if (should
            && parent is TypeDefinition def
            && def.IsValueType
            && attributeTypeDefinition?.Name == "ObsoleteAttribute"
            && attribute.ConstructorArguments.Count > 0
            && (string)attribute.ConstructorArguments[0].Value == "Types with embedded references are not supported in this version of your compiler.")
        {
            should = false;
        }

        // Do not print compiler-generated ObsoleteAttribute for constructors of classes with required properties, see https://github.com/PublicApiGenerator/PublicApiGenerator/issues/472
        if (should
            && parent is MethodDefinition def2
            && def2.IsConstructor
            && attributeTypeDefinition?.Name == "ObsoleteAttribute"
            && attribute.ConstructorArguments.Count > 0
            && (string)attribute.ConstructorArguments[0].Value == "Constructors of types with required members are not supported in this version of your compiler.")
        {
            should = false;
        }

        return should;
    }
}
