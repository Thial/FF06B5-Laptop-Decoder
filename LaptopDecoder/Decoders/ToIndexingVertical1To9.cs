namespace LaptopDecoder.Decoders;

public class ToIndexingVertical1To9 : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var newCrosses = new ValueBase[values.Length];
        var indices = new int[]
        {
            1, 4, 7, 1, 4, 7,
            2, 5, 8, 2, 5, 8,
            3, 6, 9, 3, 6, 9
        };
        
        for (var crossIndex = 0; crossIndex < values.Length; crossIndex++)
        {
            var cross = values[crossIndex];
            var valueLength = cross.Values.Length;
            var newValues = new Value[valueLength];
            for (var valueIndex = 0; valueIndex < valueLength; valueIndex++)
            {
                var newValue = GetValue(cross.ValueType, cross.Values[valueIndex], indices[crossIndex], cross.Encoding);
                newValues[valueIndex] = newValue;
            }

            newCrosses[crossIndex] = new Cross(
                newValues[0],
                newValues[1],
                newValues[2],
                newValues[3]);
        }

        return new DecoderResult(newCrosses);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var newValues = new ValueBase[values.Length];
        var indices = new int[]
        {
            1, 2, 3, 1, 2, 3,
            4, 5, 6, 4, 5, 6,
            7, 8, 9, 7, 8, 9
        };

        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        {
            var value = values[valueIndex];
            newValues[valueIndex] = GetValue(value.ValueType, value.TheValue, indices[valueIndex], value.Encoding);
        }

        return new DecoderResult(newValues);
    }

    static Value GetValue(ValueType valueType, string value, int index, Encoding encoding)
    {
        var isEmpty = StringExtensions.IsNullOrEmptyOrSpace(value);
        return new Value(valueType, isEmpty ? "" : $"{index}{value}", encoding);
    }
}