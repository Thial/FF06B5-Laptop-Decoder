namespace LaptopDecoder.Decoders;

public class ToEncodingUTF8 : DecoderBase
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
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        {
            newValues[valueIndex] = values[valueIndex].ToEncoding(Encoding.UTF8);
        }

        return new DecoderResult(newValues);
    }
}