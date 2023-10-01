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

    public override void Write(int offset, int valueWidth, int caretX, int caretY)
    {
        var halfWidth = (int)Math.Floor(valueWidth / 2f);
        WriteCross(offset, valueWidth, caretX, caretY);
        Console.SetCursorPosition(offset + caretX, caretY);
        Console.Write(_values[0].TheValue);
        Console.SetCursorPosition(offset + caretX + halfWidth + 2, caretY);
        Console.Write(_values[1].TheValue);
        Console.SetCursorPosition(offset + caretX, caretY + 2);
        Console.Write(_values[2].TheValue);
        Console.SetCursorPosition(offset + caretX + halfWidth + 2, caretY + 2);
        Console.Write(_values[3].TheValue);
    }

    public override ValueBase ToOrder()
    {
        var r = _values.Select(v => v.ToOrder()).ToArray();
        return new Cross((Value)r[0], (Value)r[1], (Value)r[2], (Value)r[3]);
    }

    public override ValueBase ToOrderSum()
    {
        var result = _values.Select(v => v.TheValue.ToOrder().Sum()).ToArray();
        return new Value(ValueType.Decimal, result.Sum().ToString());
    }

    public override ValueBase ToDecimal()
    {
        var r = _values.Select(v => v.ToDecimal()).ToArray();
        return new Cross((Value)r[0], (Value)r[1], (Value)r[2], (Value)r[3]);
    }

    public override ValueBase ToDecimalSum()
    {
        var result = _values.Select(v => v.TheValue.ToDecimal(v.ValueType).Sum()).ToArray();
        return new Value(ValueType.Decimal, result.Sum().ToString());
    }

    public override ValueBase ToHex()
    {
        var r = _values.Select(v => v.ToHex()).ToArray();
        return new Cross(
            (Value)r[0],
            (Value)r[1],
            (Value)r[2],
            (Value)r[3]);
    }
    
    public override int GetValueWidth()
    {
        var pair1Length = _values[0].TheValue.Length + _values[1].TheValue.Length + 3;
        var pair2Length = _values[1].TheValue.Length + _values[2].TheValue.Length + 3;

        return pair1Length > pair2Length ? pair1Length : pair2Length;
    }

    #region Implementation

    private Value[] _values = new Value[4];

    void WriteCross(int offset, int crossWidth, int caretX, int caretY)
    {
        var halfWidth = (int)Math.Floor(crossWidth / 2f);
        Console.SetCursorPosition(offset + caretX + halfWidth, caretY);
        Console.Write($"|");
        Console.SetCursorPosition(offset + caretX, caretY + 1);
        Console.Write(string.Join("",Enumerable.Range(0,crossWidth).Select(i => '-')));
        Console.SetCursorPosition(offset + caretX + halfWidth, caretY + 2);
        Console.Write($"|");
    }

    #endregion
}