namespace LaptopDecoder.Extensions;

public static class ValueBaseExtensions
{
    public static Cross[] ToCrosses(this ValueBase[] values)
    {
        var enc = values[0].Encoding;
        var isCross = values[0] is Cross;
        var result = values
            .Select(v => new Cross(
                new Value(v.ValueType, v.Values[0].TheValue, enc),
                new Value(v.ValueType, v.Values[isCross ? 1 : 0].TheValue, enc),
                new Value(v.ValueType, v.Values[isCross ? 2 : 0].TheValue, enc),
                new Value(v.ValueType, v.Values[isCross ? 3 : 0].TheValue, enc)))
            .ToArray();
        
        return result;
    }

    public static Value[] ToValues(this ValueBase[] values)
    {
        var enc = values[0].Encoding;
        var isCross = values[0] is Cross;
        var result = values
            .Select(v => new Value(v.ValueType, isCross
                ? v.Values[0].TheValue + v.Values[1].TheValue + v.Values[2].TheValue + v.Values[3].TheValue
                : v.Values[0].TheValue, enc))
            .ToArray();
        
        return result;
    }
}