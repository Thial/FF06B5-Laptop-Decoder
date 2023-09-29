namespace LaptopDecoder.Implementation;

public abstract class ValueBase
{
    public abstract ValueBaseType BaseType { get; }
    public abstract ValueType ValueType { get; }
    public abstract string[] Values { get; }
    public abstract void Write(int valueWidth, int caretX, int caretY);
    public abstract long[] GetOrderForEach();
    public abstract long[] GetDecimalForEach();

    public abstract string[] GetHexForEach();

    public abstract byte[] GetHexByteForEach();

    public abstract int GetValueWidth();
}