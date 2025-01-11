using System.CodeDom;

namespace PublicApiGenerator;

internal sealed class DynamicContext
{
    private readonly bool[]? _transformFlags;
    private int _index;

    public DynamicContext(CodeAttributeDeclarationCollection attributes)
    {
        for (int i = 0; i < attributes.Count; ++i)
        {
            var attribute = attributes[i];
            // return: System.Runtime.CompilerServices.DynamicAttribute is a workaround for CecilEx.MakeReturn.
            if (attribute.Name == "System.Runtime.CompilerServices.DynamicAttribute" || attribute.Name == "return: System.Runtime.CompilerServices.DynamicAttribute")
            {
                if (attribute.Arguments.Count > 0)
                {
                    // https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.dynamicattribute.-ctor
                    var expression = (CodeArrayCreateExpression)attribute.Arguments[0].Value;
                    _transformFlags = new bool[expression.Initializers.Count];
                    for (int j = 0; j < _transformFlags.Length; ++j)
                    {
                        _transformFlags[j] = (bool)((CodePrimitiveExpression)expression.Initializers[j]).Value;
                    }
                }
                else
                {
                    _transformFlags = [];
                }

                break;
            }
        }
    }

    public void Move() => ++_index;

    public bool IsDynamic => _transformFlags != null && (_transformFlags.Length == 0 || _transformFlags[_index]);
}
