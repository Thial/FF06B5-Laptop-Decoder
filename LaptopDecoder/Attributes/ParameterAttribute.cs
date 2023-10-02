namespace LaptopDecoder.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ParameterAttribute : Attribute
{
    public ParameterAttribute(string name, ValueType valueType)
    {
        Name = name;
        ValueType = valueType;
    }

    public string Name { get; }
    public ValueType ValueType { get; }
}