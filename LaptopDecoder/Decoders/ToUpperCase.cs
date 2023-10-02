namespace LaptopDecoder.Decoders;

public class ToUpperCase : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var newCrosses = new ValueBase[values.Length];
        for (var crossIndex = 0; crossIndex < values.Length; crossIndex++)
        {
            var newCrossValues = new string[4];
            for (var crossValueIndex = 0; crossValueIndex < 4; crossValueIndex++)
            {
                newCrossValues[crossValueIndex] = values[crossIndex].Values[crossValueIndex].ToUpper();
            }

            newCrosses[crossIndex] = new Cross(
                new Value(values[crossIndex].ValueType, newCrossValues[0]),
                new Value(values[crossIndex].ValueType, newCrossValues[1]),
                new Value(values[crossIndex].ValueType, newCrossValues[2]),
                new Value(values[crossIndex].ValueType, newCrossValues[3]));
        }

        return new DecoderResult(newCrosses);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var newValues = new ValueBase[values.Length];
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        {
            newValues[valueIndex] = new Value(values[valueIndex].ValueType, values[valueIndex].TheValue.ToUpper());
        }

        return new DecoderResult(newValues);
    }
}