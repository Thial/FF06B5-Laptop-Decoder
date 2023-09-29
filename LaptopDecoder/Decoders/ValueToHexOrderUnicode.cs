namespace LaptopDecoder.Decoders;

public class ValueToHexOrderUnicode : DecoderBase
{
    public override ValueBase[] Decode(Cross[] crosses, string key)
    {
        var length = crosses.Length;
        var newCrosses = new ValueBase[length];

        var orderResult = new ValueToOrder().Decode(crosses, string.Empty);

        for (var crossIndex = 0; crossIndex < length; crossIndex++)
        {
            var cross = orderResult[crossIndex];
            var hexResult = cross.GetHexForEach();

            newCrosses[crossIndex] = new Cross(
                new Value(ValueType.String, ((char)int.Parse(hexResult[0], NumberStyles.HexNumber)).ToString()),
                new Value(ValueType.String,  ((char)int.Parse(hexResult[1], NumberStyles.HexNumber)).ToString()),
                new Value(ValueType.String,  ((char)int.Parse(hexResult[2], NumberStyles.HexNumber)).ToString()),
                new Value(ValueType.String,  ((char)int.Parse(hexResult[3], NumberStyles.HexNumber)).ToString()));
        }

        return newCrosses;
    }
}