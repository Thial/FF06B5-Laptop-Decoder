namespace LaptopDecoder.Decoders;

public class ToDecimalSum : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var result = values.Select(c => c.ToDecimalSum()).ToArray();
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var result = values.Select(v => v.ToDecimalSum()).ToArray();
        return new DecoderResult(result);
    }
}