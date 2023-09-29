namespace LaptopDecoder.Decoders;

public class CrossToDecimalSumUnicode : DecoderBase
{
    public override ValueBase[] Decode(Cross[] crosses, string key)
    {
        var length = crosses.Length;
        var unicodeValues = new ValueBase[length];

        var decimalSumResult = new CrossToDecimalSum().Decode(crosses, key);

        for (var valueIndex = 0; valueIndex < length; valueIndex++)
        {
            var hexStrings = decimalSumResult[valueIndex].GetHexForEach();
            var unicodeString = hexStrings
                .Aggregate(string.Empty,
                    (current, hexString) 
                        => current + (char)int.Parse(hexString, NumberStyles.HexNumber));

            unicodeValues[valueIndex] = new Value(ValueType.String, unicodeString);
        }

        return unicodeValues;
    }
}