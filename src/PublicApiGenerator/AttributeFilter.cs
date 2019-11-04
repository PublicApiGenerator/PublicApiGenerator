using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;

namespace PublicApiGenerator
{
    internal sealed partial class AttributeFilter
    {
        private readonly HashSet<string> _excludedAttributes;

        public AttributeFilter(IEnumerable<string> excludedAttributes)
        {
            _excludedAttributes = excludedAttributes is null
                ? AttributesNotRelevantForThePublicApi
                : new HashSet<string>(AttributesNotRelevantForThePublicApi.Concat(excludedAttributes));
        }

        public bool ShouldIncludeAttribute(CustomAttribute attribute)
        {
            var attributeTypeDefinition = attribute.AttributeType.Resolve();

            return attributeTypeDefinition != null
                   && !_excludedAttributes.Contains(attribute.AttributeType.FullName)
                   && (attributeTypeDefinition.IsPublic || InternalAttributesThatAffectCompilerOrRuntimeBehavior.Contains(attribute.AttributeType.FullName));
        }
    }
}
