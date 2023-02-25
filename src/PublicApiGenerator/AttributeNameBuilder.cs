namespace PublicApiGenerator;

public static class AttributeNameBuilder
{
    public static string Get(string name)
    {
        // ParamArrayAttribute cannot be augment with the attribute marker, it would trip up CodeDom
        return name == "System.ParamArrayAttribute" ? name : $"{name}{CodeNormalizer.ATTRIBUTE_MARKER}";
    }
}
