namespace LaptopDecoder.Implementation;

public abstract class DecoderBase
{
    public abstract DecoderResult DecodeCross(Cross[] values, string key = "");
    public abstract DecoderResult DecodeValue(Value[] values, string key = "");
}