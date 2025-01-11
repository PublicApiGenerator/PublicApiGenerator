using System.CodeDom;

namespace PublicApiGenerator;

internal static class CodeTypeDeclarationExtensions
{
    public static CodeTypeDeclarationEx Sort(this CodeTypeDeclarationEx original)
    {
        if (original.IsEnum)
        {
            return original;
        }

        var sorted = new CodeTypeDeclarationEx(original.Name)
        {
            Attributes = original.Attributes,
            CustomAttributes = original.CustomAttributes,
            TypeAttributes = original.TypeAttributes,
            IsClass = original.IsClass,
            IsEnum = original.IsEnum,
            IsInterface = original.IsInterface,
            IsPartial = original.IsPartial,
            IsStruct = original.IsStruct,
            IsStatic = original.IsStatic,
            IsReadonly = original.IsReadonly,
            IsByRefLike = original.IsByRefLike,
            LinePragma = original.LinePragma,
        };

        sorted.BaseTypes.AddRange(original.BaseTypes);
        sorted.Comments.AddRange(original.Comments);
        sorted.EndDirectives.AddRange(original.EndDirectives);
        sorted.StartDirectives.AddRange(original.StartDirectives);
        sorted.TypeParameters.AddRange(original.TypeParameters);

        foreach (var key in original.UserData.Keys)
        {
            sorted.UserData[key] = original.UserData[key];
        }

        var emptyParamList = new List<CodeParameterDeclarationExpression>();

        var sortedMembers = original.Members.OfType<CodeTypeMember>()
            .OrderBy(m => m.Attributes.HasFlag(MemberAttributes.Static))
            .ThenBy(m => m.GetType().Name, StringComparer.Ordinal)
            .ThenBy(m => m is CodeMemberMethod ? CSharpOperatorKeyword.Get(m.Name) : m.Name, StringComparer.Ordinal)
            .ThenBy(m => m is CodeMemberMethod method
                        ? method.TypeParameters.Count
                        : 0)
            .ThenBy(m => m is CodeMemberMethod method
                        ? method.Parameters.Count
                        : 0)
            .ThenBy(m => m is CodeMemberMethod method
                        ? method.Parameters.OfType<CodeParameterDeclarationExpression>().ToList()
                        : emptyParamList,
                    new ParamListComparer());

        foreach (var member in sortedMembers)
        {
            sorted.Members.Add(member);
        }

        return sorted;
    }

    private class ParamListComparer : IComparer<List<CodeParameterDeclarationExpression>>
    {
        public int Compare(List<CodeParameterDeclarationExpression> x, List<CodeParameterDeclarationExpression> y)
        {
            var paramIndex = 0;
            for (; paramIndex < x.Count; ++paramIndex)
            {
                var paramX = x[paramIndex];
                var paramY = y[paramIndex];

                var type = Compare(paramX.Type, paramY.Type);
                if (type != 0)
                {
                    return type;
                }

                var name = string.CompareOrdinal(paramX.Name, paramY.Name);
                if (name != 0)
                {
                    return name;
                }
            }

            return paramIndex < y.Count ? -1 : 0;
        }

        private int Compare(CodeTypeReference x, CodeTypeReference y)
        {
            var baseType = string.CompareOrdinal(x.BaseType, y.BaseType);
            if (baseType != 0)
            {
                return baseType;
            }

            var typeArgsX = x.TypeArguments.OfType<CodeTypeReference>().ToList();
            var typeArgsY = y.TypeArguments.OfType<CodeTypeReference>().ToList();

            var typeArgIndex = 0;
            for (; typeArgIndex < typeArgsX.Count; ++typeArgIndex)
            {
                if (typeArgIndex == typeArgsY.Count)
                {
                    return 1;
                }

                var typeArg = Compare(typeArgsX[typeArgIndex], typeArgsY[typeArgIndex]);
                if (typeArg != 0)
                {
                    return typeArg;
                }
            }

            return typeArgIndex < typeArgsY.Count ? -1 : 0;
        }
    }
}
