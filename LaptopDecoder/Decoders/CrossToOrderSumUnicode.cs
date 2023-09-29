namespace LaptopDecoder.Decoders;

public class CrossToOrderSumUnicode : DecoderBase
{
    public override ValueBase[] Decode(Cross[] crosses, string key)
    {
        var length = crosses.Length;
        var unicodeValues = new ValueBase[length];

        var orderSumResult = new CrossToOrderSum().Decode(crosses, key);

        for (var valueIndex = 0; valueIndex < length; valueIndex++)
        {
            var hexStrings = orderSumResult[valueIndex].GetHexForEach();
            var unicodeString = hexStrings
                .Aggregate(string.Empty,
                    (current, hexString) 
                        => current + (char)int.Parse(hexString, NumberStyles.HexNumber));

            unicodeValues[valueIndex] = new Value(ValueType.String, unicodeString);
        }

        return unicodeValues;
    }
}