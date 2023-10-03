namespace LaptopDecoder.Implementation;

public class Value : ValueBase
{
    public Value(ValueType valueType, string value, Encoding encoding)
    {
        BaseType = ValueBaseType.Value;
        ValueType = valueType;

        _value = value;

        Encoding = encoding;
    }

    public override ValueBaseType BaseType { get; }
    public override ValueType ValueType { get; }

    public override string[] Values
        => new[] { _value };
    
    public override Encoding Encoding { get; }

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
        return new Value(ValueType.Decimal, string.Join("", result), Encoding);
    }

    public override ValueBase ToOrderSum()
        => ToOrder();

    public override ValueBase ToDecimal()
    {
        var result = _value.ToDecimal(ValueType);
        return new Value(ValueType.Decimal, string.Join("", result), Encoding);
    }

    public override ValueBase ToDecimalSum()
        => ToDecimal();

    public override ValueBase ToHex()
    {
        var result = _value.ToHex(ValueType);
        return new Value(ValueType.Hex, result, Encoding);
    }

    public override ValueBase ToCharacter()
    {
        var result = _value.ToCharacter(ValueType, Encoding);
        return new Value(ValueType.String, result, Encoding);
    }

    public override ValueBase ToEncoding(Encoding encoding)
    {
        var result = _value.ToEncoding(Encoding, encoding);
        return new Value(ValueType, result, encoding);
    }
    
    public override int GetValueWidth()
    {
        return _value.Length;
    }

    #region Implementation

    private readonly string _value;

    #endregion
}