namespace PublicApiGenerator
{
    public static class AttributeNameBuilder
    {
        public static string Get(string name)
        {
            return name != "System.ParamArrayAttribute" ? $"{name}{CodeNormalizer.AttributeMarker}" : name;
        }
    }
}
