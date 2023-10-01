namespace LaptopDecoder.Decoders;

public class ToHex : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, string key = "")
    {
        var result = values.Select(c => c.ToHex()).ToArray();
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, string key = "")
    {
        var result = values.Select(v => v.ToHex()).ToArray();
        return new DecoderResult(result);
    }
}