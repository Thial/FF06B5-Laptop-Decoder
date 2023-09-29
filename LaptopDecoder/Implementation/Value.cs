namespace LaptopDecoder.Implementation;

public class Value : ValueBase
{
    public Value(ValueType valueType, string value)
    {
        BaseType = ValueBaseType.Value;
        ValueType = valueType;

        _value = value;
    }

    public override ValueBaseType BaseType { get; }
    public override ValueType ValueType { get; }

    public override string[] Values
        => new[] { _value };

    public string TheValue
        => _value;

    public override void Write(int valueWidth, int caretX, int caretY)
    {
        Console.SetCursorPosition(caretX, caretY);
        Console.Write(_value);
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
        var length = _value.Length;
        var result = new long[length];
        for (var charIndex = 0; charIndex < length; charIndex++) 
            result[charIndex] = _value[charIndex];

        return result;
    }

    public override string[] GetHexForEach()
    {
        var decimalResult = GetDecimalForEach();

        var hexResult = ValueType switch
        {
            ValueType.String => BitConverter.ToString(Encoding.UTF8.GetBytes(_value)).Replace("-", String.Empty),
            ValueType.Decimal => long.Parse(_value).ToString("X4")
        };

        return new[] { hexResult };
    }

    public override byte[] GetHexByteForEach()
    {
        var hexResult = GetHexForEach()[0];
        var hexBytes = Enumerable.Range(0, hexResult.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hexResult.Substring(x, 2), 16))
            .ToArray();
        return hexBytes;
    }
    
    public override int GetValueWidth()
    {
        return _value.Length;
    }

    #region Implementation

    private readonly string _value;

    #endregion
}