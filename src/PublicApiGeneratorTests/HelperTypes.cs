namespace PublicApiGeneratorTests.Examples;

public class ComplexType
{
}

public class GenericType<T>
{
}

public class GenericTypeExtra<T, T2, T3>
{
}

[AttributeUsage(AttributeTargets.All)]
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

[Flags]
public enum EnumWithSomeSpecialFlags
{
    None = 0,
    PublicParameterlessConstructor = 1,
    PublicConstructors = 3,
    NonPublicConstructors = 4
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

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithPositionalParameters1Attribute : Attribute
{
    public AttributeWithPositionalParameters1Attribute(string value)
    {
    }
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithPositionalParameters2Attribute : Attribute
{
    public AttributeWithPositionalParameters2Attribute(int value)
    {
    }
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithMultiplePositionalParametersAttribute : Attribute
{
    public AttributeWithMultiplePositionalParametersAttribute(int value, string value2)
    {
    }
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithNamedParameterAttribute : Attribute
{
    public string StringValue { get; set; }
    public int IntValue { get; set; }
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithNamedFieldAttribute : Attribute
{
    public string StringValue;
    public int IntValue;
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithNamedAndPositionalParameterAttribute : Attribute
{
    public AttributeWithNamedAndPositionalParameterAttribute(int value1, string value2)
    {
    }

    public int IntValue { get; set; }
    public string StringValue { get; set; }
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithNamedParameterAndFieldAttribute : Attribute
{
    public int IntField;
    public string StringField;

    public int IntProperty { get; set; }
    public string StringProperty { get; set; }
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithSimpleEnumAttribute : Attribute
{
    public AttributeWithSimpleEnumAttribute(SimpleEnum value)
    {
    }
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithEnumFlagsAttribute : Attribute
{
    public AttributeWithEnumFlagsAttribute(EnumWithFlags value)
    {
    }
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithEnumWithSomeSpecialFlagsAttribute : Attribute
{
    public AttributeWithEnumWithSomeSpecialFlagsAttribute(EnumWithSomeSpecialFlags value)
    {
    }
}

[AttributeUsage(AttributeTargets.All)]
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

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class AttributeWithMultipleUsagesSupport : Attribute
{
    public string StringValue;
    public int IntValue;
}

[AttributeUsage(AttributeTargets.All)]
public class AttributeWithStringInitialiser : Attribute
{
    public AttributeWithStringInitialiser(string value)
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

internal class AttributeWhichIsInternal : Attribute
{
}
