using System.CodeDom;
using Mono.Cecil;

namespace PublicApiGenerator;

internal class CodeMemberPropertyEx : CodeMemberProperty
{
    public PropertyDefinition PropertyDefinition { get; set; } = null!;
}
