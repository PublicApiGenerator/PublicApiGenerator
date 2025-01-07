using System.CodeDom;
using Mono.Cecil;

namespace PublicApiGenerator;

internal class CodeMemberMethodEx : CodeMemberMethod
{
    public MethodDefinition MethodDefinition { get; set; } = null!;
}
