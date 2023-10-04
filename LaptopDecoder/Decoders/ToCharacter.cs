namespace LaptopDecoder.Decoders;

public class ToCharacter : DecoderBase
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
            newValues[valueIndex] = values[valueIndex].ToCharacter();
        }

        return new DecoderResult(newValues);
    }
}