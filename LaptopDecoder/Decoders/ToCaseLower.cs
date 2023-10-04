namespace LaptopDecoder.Decoders;

public class ToCaseLower : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var result = values
            .Select(c => new Cross(DecodeValue(c.Values, parameters)))
            .Cast<ValueBase>()
            .ToArray();

        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var newValues = new ValueBase[values.Length];
        var enc = values[0].Encoding;
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        {
            newValues[valueIndex] = new Value(values[valueIndex].ValueType, values[valueIndex].TheValue.ToLower(), enc);
        }

        return new DecoderResult(newValues);
    }
}