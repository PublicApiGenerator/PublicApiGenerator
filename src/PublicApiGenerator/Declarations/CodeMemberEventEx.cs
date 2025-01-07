using System.CodeDom;
using Mono.Cecil;

internal class CodeMemberEventEx : CodeMemberEvent
{
    public EventDefinition EventDefinition { get; set; } = null!;
}