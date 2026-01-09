using System.CodeDom;
using Mono.Cecil;

namespace PublicApiGenerator;

internal class CodeMemberEventEx : CodeMemberEvent
{
    public EventDefinition EventDefinition { get; set; } = null!;
}
