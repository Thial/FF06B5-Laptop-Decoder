namespace LaptopDecoder.Decoders;

public class ToEncodingWindows1252 : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var newCrosses = new ValueBase[values.Length];
        for (var crossIndex = 0; crossIndex < values.Length; crossIndex++)
        {
            newCrosses[crossIndex] = values[crossIndex].ToEncoding(Encoding.GetEncoding(1252));
        }

        return new DecoderResult(newCrosses);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var newValues = new ValueBase[values.Length];
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        {
            newValues[valueIndex] = values[valueIndex].ToEncoding(Encoding.GetEncoding(1252));
        }

        return new DecoderResult(newValues);
    }
}