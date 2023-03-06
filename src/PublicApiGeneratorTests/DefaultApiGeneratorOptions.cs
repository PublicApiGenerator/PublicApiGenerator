using PublicApiGenerator;

namespace PublicApiGeneratorTests;

internal class DefaultApiGeneratorOptions : ApiGeneratorOptions
{
    public DefaultApiGeneratorOptions()
    {
        IncludeAssemblyAttributes = false;
    }
}
