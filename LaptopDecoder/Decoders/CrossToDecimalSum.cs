namespace LaptopDecoder.Decoders;

public class CrossToDecimalSum : DecoderBase
{
    public override ValueBase[] Decode(Cross[] crosses, string key)
    {
        var length = crosses.Length;
        var values = new ValueBase[length];

        for (var crossIndex = 0; crossIndex < length; crossIndex++)
        {
            var cross = crosses[crossIndex];
            var decimalResult = cross.GetDecimalForEach();
            var sum = decimalResult.Sum();

            values[crossIndex] = new Value(ValueType.Decimal, sum.ToString());
        }
        
        return values;
    }
}