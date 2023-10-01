namespace LaptopDecoder.Decoders;

public class ToOrder : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, string key = "")
    {
        var result = values.Select(c => c.ToOrder()).ToArray();
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, string key = "")
    {
        var result = values.Select(v => v.ToOrder()).ToArray();
        return new DecoderResult(result);
    }
}