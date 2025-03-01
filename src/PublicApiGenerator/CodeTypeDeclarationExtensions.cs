using System.CodeDom;

namespace PublicApiGenerator;

internal static class CodeTypeDeclarationExtensions
{
    private static readonly Dictionary<string, string> _map = new()
    {
        { "System.Int16", "short" },
        { "System.Int32", "int" },
        { "System.Int64", "long" },
        { "System.String", "string" },
        { "System.Object", "object" },
        { "System.Boolean", "bool" },
        { "System.Char", "char" },
        { "System.Byte", "byte" },
        { "System.UInt16", "ushort" },
        { "System.UInt32", "uint" },
        { "System.UInt64", "ulong" },
        { "System.SByte", "sbyte" },
        { "System.Single", "float" },
        { "System.Double", "double" },
        { "System.Decimal", "decimal" },
    };

    private static string MapToAlias(string type) => _map.TryGetValue(type, out string alias) ? alias : type;

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
            IsRecord = original.IsRecord,
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
            .ThenBy(m => m is CodeMemberMethod method && method.Parameters.Count > 0
                ? method.Parameters.OfType<CodeParameterDeclarationExpression>().ToList()
                : [],
                ParamListComparer.Instance)
            .ThenBy(m => m is CodeMemberMethod method && method.TypeParameters.Count > 0
                ? method.TypeParameters.OfType<CodeTypeParameter>().ToList()
                : [],
                TypeParamListComparer.Instance);

        foreach (var member in sortedMembers)
        {
            sorted.Members.Add(member);
        }

        return sorted;
    }

    private class ParamListComparer : IComparer<List<CodeParameterDeclarationExpression>>
    {
        public static readonly ParamListComparer Instance = new();

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
            string GetBaseType(CodeTypeReference r)
            {
                return r.BaseType == "System.Nullable`1" && r.TypeArguments.Count == 1
                    ? r.TypeArguments[0].BaseType + "?"
                    : MapToAlias(r.BaseType);
            }

            var baseType = string.CompareOrdinal(GetBaseType(x), GetBaseType(y));
            if (baseType != 0)
            {
                return baseType;
            }

            var typeArgsX = x.BaseType == "System.Nullable`1" && x.TypeArguments.Count == 1 ? [] : x.TypeArguments.OfType<CodeTypeReference>().ToList();
            var typeArgsY = y.BaseType == "System.Nullable`1" && y.TypeArguments.Count == 1 ? [] : y.TypeArguments.OfType<CodeTypeReference>().ToList();

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

    private class TypeParamListComparer : IComparer<List<CodeTypeParameter>>
    {
        public static readonly TypeParamListComparer Instance = new();

        public int Compare(List<CodeTypeParameter> x, List<CodeTypeParameter> y)
        {
            var paramIndex = 0;
            for (; paramIndex < x.Count; ++paramIndex)
            {
                var paramX = x[paramIndex];
                var paramY = y[paramIndex];

                var name = string.CompareOrdinal(paramX.Name, paramY.Name);
                if (name != 0)
                {
                    return name;
                }

                var constraints = Compare(paramX.Constraints, paramY.Constraints);
                if (constraints != 0)
                {
                    return constraints;
                }
            }

            return paramIndex < y.Count ? -1 : 0;
        }

        private int Compare(CodeTypeReferenceCollection x, CodeTypeReferenceCollection y)
        {
            var count = x.Count.CompareTo(y.Count);
            if (count != 0)
            {
                return count;
            }

            for (int i = 0; i < x.Count; ++i)
            {
                var baseType = x[i].BaseType.CompareTo(y[i].BaseType);
                if (baseType != 0)
                {
                    return baseType;
                }
            }

            return 0;
        }
    }
}
