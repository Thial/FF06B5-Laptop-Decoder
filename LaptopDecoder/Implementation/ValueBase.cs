namespace LaptopDecoder.Implementation;

public abstract class ValueBase
{
    public abstract ValueBaseType BaseType { get; }
    public abstract ValueType ValueType { get; }
    public abstract Value[] Values { get; }
    public abstract Encoding Encoding { get; }
    public abstract void Write(int offset, int valueWidth, int caretX, int caretY);
    public abstract ValueBase ToOrder();
    public abstract ValueBase ToOrderSum();
    public abstract ValueBase ToDecimal();
    public abstract ValueBase ToDecimalSum();
    public abstract ValueBase ToHex();
    public abstract ValueBase ToCharacter();
    public abstract ValueBase ToEncoding(Encoding encoding);

    public abstract int GetValueWidth();
}