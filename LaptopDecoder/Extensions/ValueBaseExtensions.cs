namespace LaptopDecoder.Extensions;

public static class ValueBaseExtensions
{
    public static Cross[] ToCrosses(this ValueBase[] values)
    {
        var enc = values[0].Encoding;
        var isCross = values[0] is Cross;
        var result = values
            .Select(v => new Cross(
                new Value(v.ValueType, v.Values[0], enc),
                new Value(v.ValueType, v.Values[isCross ? 1 : 0], enc),
                new Value(v.ValueType, v.Values[isCross ? 2 : 0], enc),
                new Value(v.ValueType, v.Values[isCross ? 3 : 0], enc)))
            .ToArray();
        
        return result;
    }

    public static Value[] ToValues(this ValueBase[] values)
    {
        var enc = values[0].Encoding;
        var isCross = values[0] is Cross;
        var result = values
            .Select(v => new Value(v.ValueType, isCross
                ? v.Values[0] + v.Values[1] + v.Values[2] + v.Values[3]
                : v.Values[0], enc))
            .ToArray();
        
        return result;
    }
}