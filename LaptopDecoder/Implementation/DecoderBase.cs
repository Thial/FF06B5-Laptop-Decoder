namespace LaptopDecoder.Implementation;

public abstract class DecoderBase
{
    public abstract DecoderResult DecodeCross(Cross[] values, Parameter[] parameters);
    public abstract DecoderResult DecodeValue(Value[] values, Parameter[] parameters);
}