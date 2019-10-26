using Mono.Cecil;
using Mono.Collections.Generic;
using System;

namespace PublicApiGenerator
{
    internal sealed class SuppressNullableProvider : ICustomAttributeProvider
    {
        public static readonly SuppressNullableProvider Instance = new SuppressNullableProvider();

        public Collection<CustomAttribute> CustomAttributes => throw new NotImplementedException();

        public bool HasCustomAttributes => throw new NotImplementedException();

        public MetadataToken MetadataToken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
