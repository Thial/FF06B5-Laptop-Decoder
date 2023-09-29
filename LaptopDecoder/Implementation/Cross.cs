namespace LaptopDecoder.Implementation;

public class Cross : ValueBase
{
    public Cross(Value topLeft, Value topRight, Value bottomLeft, Value bottomRight)
    {
        BaseType = ValueBaseType.Cross;
        ValueType = topLeft.ValueType;

        _values[0] = topLeft;
        _values[1] = topRight;
        _values[2] = bottomLeft;
        _values[3] = bottomRight;
    }

    public override ValueBaseType BaseType { get; }
    public override ValueType ValueType { get; }

    public override string[] Values
        => _values.Select(v => v.TheValue).ToArray();

    public override void Write(int valueWidth, int caretX, int caretY)
    {
        var halfWidth = (int)Math.Floor(valueWidth / 2f);
        WriteCross(valueWidth, caretX, caretY);
        Console.SetCursorPosition(caretX, caretY);
        Console.Write(_values[0].TheValue);
        Console.SetCursorPosition(caretX + halfWidth + 2, caretY);
        Console.Write(_values[1].TheValue);
        Console.SetCursorPosition(caretX, caretY + 2);
        Console.Write(_values[2].TheValue);
        Console.SetCursorPosition(caretX + halfWidth + 2, caretY + 2);
        Console.Write(_values[3].TheValue);
    }

    public override long[] GetOrderForEach()
    {
        var decimalResult = GetDecimalForEach();
        var result = new long[decimalResult.Length];
        for (var charIndex = 0; charIndex < decimalResult.Length; charIndex++)
            result[charIndex] = decimalResult[charIndex] - 64;
        return result;
    }

    public override long[] GetDecimalForEach()
    {
        var length = _values.Length;
        var result = new long[length];
        for (var valueIndex = 0; valueIndex < length; valueIndex++)
            result[valueIndex] = _values[valueIndex].GetDecimalForEach().Sum();

        return result;
    }

    public override string[] GetHexForEach()
    {
        var length = _values.Length;
        var result = new string[length];
        for (var valueIndex = 0; valueIndex < length; valueIndex++)
        {
            var value = _values[valueIndex];
            result[valueIndex] = value.GetHexForEach()[0];
        }

        return result;
    }

    public override byte[] GetHexByteForEach()
    {
        var hexResult = GetHexForEach();
        var result = new List<byte>();
        foreach (var hexString in hexResult)
        {
            var hexBytes = Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();

            result.AddRange(hexBytes);
        }

        return result.ToArray();
    }
    
    public override int GetValueWidth()
    {
        var pair1Length = _values[0].TheValue.Length + _values[1].TheValue.Length + 3;
        var pair2Length = _values[1].TheValue.Length + _values[2].TheValue.Length + 3;

        return pair1Length > pair2Length ? pair1Length : pair2Length;
    }

    #region Implementation

    private Value[] _values = new Value[4];

    void WriteCross(int crossWidth, int caretX, int caretY)
    {
        var halfWidth = (int)Math.Floor(crossWidth / 2f);
        Console.SetCursorPosition(caretX + halfWidth, caretY);
        Console.Write($"|");
        Console.SetCursorPosition(caretX, caretY + 1);
        Console.Write(string.Join("",Enumerable.Range(0,crossWidth).Select(i => '-')));
        Console.SetCursorPosition(caretX + halfWidth, caretY + 2);
        Console.Write($"|");
    }

    #endregion
}