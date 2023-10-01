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

    public override void Write(int offset, int valueWidth, int caretX, int caretY)
    {
        Console.SetCursorPosition(offset + caretX, caretY);
        Console.Write(_value);
    }

    public override ValueBase ToOrder()
    {
        var result = _value.ToOrder();
        return new Value(ValueType.Decimal, string.Join("", result));
    }

    public override ValueBase ToOrderSum()
        => ToOrder();

    public override ValueBase ToDecimal()
    {
        var result = _value.ToDecimal(ValueType);
        return new Value(ValueType.Decimal, string.Join("", result));
    }

    public override ValueBase ToDecimalSum()
        => ToDecimal();

    public override ValueBase ToHex()
    {
        var result = _value.ToHex(ValueType);
        return new Value(ValueType.Hex, result);
    }
    
    public override int GetValueWidth()
    {
        return _value.Length;
    }

    #region Implementation

    private readonly string _value;

    #endregion
}