using Mono.Cecil;

namespace PublicApiGenerator;

internal sealed class FullNameComparer : Comparer<TypeReference>
{
    public override int Compare(TypeReference x, TypeReference y) =>
        StringComparer.Ordinal.Compare(x.FullName, y.FullName);
}
