namespace LaptopDecoder.Implementation;

public class Parameter
{
    public string Name { get; set; }
    public ValueType ValueType { get; set; }
    public string Value { get; set; }

    public long ToDecimal()
    {
        return long.TryParse(Value, out var parsedValue) ? parsedValue : 0;
    }
}