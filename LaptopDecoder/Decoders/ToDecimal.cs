namespace LaptopDecoder.Decoders;

[Parameter("KEY", ValueType.Decimal)]
[Parameter("SOMETHING", ValueType.Hex)]
public class ToDecimal : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var result = values.Select(c => c.ToDecimal()).ToArray();
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var result = values.Select(v => v.ToDecimal()).ToArray();
        return new DecoderResult(result);
    }
}