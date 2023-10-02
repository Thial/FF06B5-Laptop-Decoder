namespace LaptopDecoder.Decoders;

public class ToHex : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var result = values.Select(c => c.ToHex()).ToArray();
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var result = values.Select(v => v.ToHex()).ToArray();
        return new DecoderResult(result);
    }
}