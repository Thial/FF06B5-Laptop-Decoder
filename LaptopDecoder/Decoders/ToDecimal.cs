namespace LaptopDecoder.Decoders;

public class ToDecimal : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, string key = "")
    {
        var result = values.Select(c => c.ToDecimal()).ToArray();
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, string key = "")
    {
        var result = values.Select(v => v.ToDecimal()).ToArray();
        return new DecoderResult(result);
    }
}