using Mono.Cecil;

namespace PublicApiGenerator;

internal sealed class TypeReferenceComparer : Comparer<TypeReference>
{
    public override int Compare(TypeReference x, TypeReference y)
    {
        int res = StringComparer.Ordinal.Compare(x.Namespace, y.Namespace);

        return res != 0
            ? res
            : StringComparer.Ordinal.Compare(x.FullName, y.FullName);
    }
}
