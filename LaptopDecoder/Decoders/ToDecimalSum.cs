namespace LaptopDecoder.Decoders;

public class ToDecimalSum : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, string key = "")
    {
        var result = values.Select(c => c.ToDecimalSum()).ToArray();
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, string key = "")
    {
        var result = values.Select(v => v.ToDecimalSum()).ToArray();
        return new DecoderResult(result);
    }
}