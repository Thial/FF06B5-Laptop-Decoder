namespace LaptopDecoder.Decoders;

public class ToOrderSum : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var result = values.Select(c => c.ToOrderSum()).ToArray();
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var result = values.Select(v => v.ToOrderSum()).ToArray();
        return new DecoderResult(result);
    }
}