using System;

// ReSharper disable once CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedTypeParameter
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace ApiApproverTests.Examples
{
    public class ComplexType
    {
    }

    public class GenericType<T>
    {
    }

    public class GenericTypeExtra<T, T2, T3>
    {
    }

    public class SimpleAttribute : Attribute
    {
    }

    public enum SimpleEnum
    {
        Red,
        Green,
        Blue
    }

    [Flags]
    public enum EnumWithFlags
    {
        One,
        Two,
        Three
    }

    public class Attribute_AA : Attribute
    {
    }

    public class Attribute_MM : Attribute
    {
    }

    public class Attribute_ZZ : Attribute
    {
    }

    public class AttributeWithPositionalParameters1Attribute : Attribute
    {
        public AttributeWithPositionalParameters1Attribute(string value)
        {
        }
    }

    public class AttributeWithPositionalParameters2Attribute : Attribute
    {
        public AttributeWithPositionalParameters2Attribute(int value)
        {
        }
    }

    public class AttributeWithMultiplePositionalParametersAttribute : Attribute
    {
        public AttributeWithMultiplePositionalParametersAttribute(int value, string value2)
        {
        }
    }

    public class AttributeWithNamedParameterAttribute : Attribute
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }

    public class AttributeWithNamedAndPositionalParameterAttribute : Attribute
    {
        public AttributeWithNamedAndPositionalParameterAttribute(int value1, string value2)
        {
        }

        public int IntValue { get; set; }
        public string StringValue { get; set; }
    }

    public class AttributeWithSimpleEnumAttribute : Attribute
    {
        public AttributeWithSimpleEnumAttribute(SimpleEnum value)
        {
        }
    }

    public class AttributeWithEnumFlagsAttribute : Attribute
    {
        public AttributeWithEnumFlagsAttribute(EnumWithFlags value)
        {
        }
    }

    public class AttributeWithTypeParameterAttribute : Attribute
    {
        public AttributeWithTypeParameterAttribute(Type type)
        {
        }
    }

    public class AttributeWithObjectInitialiser : Attribute
    {
        public AttributeWithObjectInitialiser(object values)
        {
        }
    }

    public class AttributeWithObjectArrayInitialiser : Attribute
    {
        public AttributeWithObjectArrayInitialiser(params object[] values)
        {
        }
    }

    public class AttributeWithStringArrayInitialiser : Attribute
    {
        public AttributeWithStringArrayInitialiser(params string[] values)
        {
        }
    }
}
// ReSharper restore UnusedAutoPropertyAccessor.Global
// ReSharper restore UnusedParameter.Local
// ReSharper restore InconsistentNaming
// ReSharper restore UnusedTypeParameter
// ReSharper restore ClassNeverInstantiated.Global
