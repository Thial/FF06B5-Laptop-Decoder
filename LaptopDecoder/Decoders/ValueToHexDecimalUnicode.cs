namespace LaptopDecoder.Decoders;

public class ValueToHexDecimalUnicode : DecoderBase
{
    public override ValueBase[] Decode(Cross[] crosses, string key)
    {
        var length = crosses.Length;
        var newCrosses = new ValueBase[length];

        var decimalResult = new ValueToDecimal().Decode(crosses, key);

        for (var crossIndex = 0; crossIndex < length; crossIndex++)
        {
            var cross = decimalResult[crossIndex];
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