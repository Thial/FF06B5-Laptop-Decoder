namespace LaptopDecoder.Decoders;

public class ValueToHexOrder : DecoderBase
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
                new Value(ValueType.Hex, hexResult[0]),
                new Value(ValueType.Hex,  hexResult[1]),
                new Value(ValueType.Hex,  hexResult[2]),
                new Value(ValueType.Hex,  hexResult[3]));
        }

        return newCrosses;
    }
}