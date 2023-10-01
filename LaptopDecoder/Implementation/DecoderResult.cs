namespace LaptopDecoder.Implementation;

public class DecoderResult
{
    public DecoderResult(ValueBase[] values)
    {
        Values = values;
        ValueBaseType = values[0] is Cross 
            ? ValueBaseType.Cross 
            : ValueBaseType.Value;
    }
    
    public ValueBase[] Values { get; }
    public ValueBaseType ValueBaseType { get; }
}