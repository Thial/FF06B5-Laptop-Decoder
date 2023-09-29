namespace LaptopDecoder.Decoders;

[RequiresKey]
public class ValueToOrder : DecoderBase
{
    public override ValueBase[] Decode(Cross[] crosses, string key)
    {
        var length = crosses.Length;
        var newCrosses = new ValueBase[length];

        for (var crossIndex = 0; crossIndex < length; crossIndex++)
        {
            var cross = crosses[crossIndex];
            var decimalResult = cross.GetOrderForEach();

            newCrosses[crossIndex] = new Cross(
                new Value(ValueType.Decimal, decimalResult[0].ToString()),
                new Value(ValueType.Decimal, decimalResult[1].ToString()),
                new Value(ValueType.Decimal, decimalResult[2].ToString()),
                new Value(ValueType.Decimal, decimalResult[3].ToString()));
        }

        return newCrosses;
    }
}