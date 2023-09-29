namespace LaptopDecoder.Decoders;

public class CrossToOrderSum : DecoderBase
{
    public override ValueBase[] Decode(Cross[] crosses, string key)
    {
        var length = crosses.Length;
        var values = new ValueBase[length];

        for (var crossIndex = 0; crossIndex < length; crossIndex++)
        {
            var cross = crosses[crossIndex];
            var orderResult = cross.GetOrderForEach();
            var sum = orderResult.Sum();

            values[crossIndex] = new Value(ValueType.Decimal, sum.ToString());
        }
        
        return values;
    }
}