using PublicApiGenerator;

namespace PublicApiGeneratorTests
{
    class DefaultApiGeneratorOptions : ApiGeneratorOptions
    {
        public DefaultApiGeneratorOptions()
        {
            IncludeAssemblyAttributes = false;
        }
    }
}
