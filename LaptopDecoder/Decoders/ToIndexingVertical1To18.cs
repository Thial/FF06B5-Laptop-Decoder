namespace LaptopDecoder.Decoders;

public class ToIndexingVertical1To18 : DecoderBase
{
    int[] _indices = new int[]
    {
        1, 4, 7, 10, 13, 16,
        2, 5, 8, 11, 14, 17,
        3, 6, 9, 12, 15, 18
    };
    
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var result = values
            .Select(c => new Cross(DecodeValue(c.Values, parameters)))
            .Cast<ValueBase>()
            .ToArray();

        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var newValues = new ValueBase[values.Length];

        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        {
            var value = values[valueIndex];
            newValues[valueIndex] = GetValue(value.ValueType, value.TheValue, _indices[valueIndex], value.Encoding);
        }

        return new DecoderResult(newValues);
    }

    static Value GetValue(ValueType valueType, string value, int index, Encoding encoding)
    {
        var isEmpty = StringExtensions.IsNullOrEmptyOrSpace(value);
        return new Value(valueType, isEmpty ? "" : $"{index}{value}", encoding);
    }
}