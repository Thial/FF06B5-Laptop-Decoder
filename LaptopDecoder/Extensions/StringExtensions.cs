namespace LaptopDecoder.Extensions;

public static class StringExtensions
{
    public static long[] ToDecimal(this string value, ValueType valueType)
    {
        var result = valueType switch
        {
            ValueType.String => value.Select(c => (long)c).ToArray(),
            ValueType.Decimal => new []{ long.Parse(value)},
            ValueType.Hex => new[] {long.Parse(value, NumberStyles.HexNumber)}
        };
        return result;
    }

    public static long[] ToOrder(this string value)
    {
        var result = value.Select(c => (long)c - 64).ToArray();
        return result;
    }

    public static string ToHex(this string value, ValueType valueType)
    {
        var result = BitConverter.ToString(Encoding.UTF8.GetBytes(value)).Replace("-", String.Empty);
        return result;
    }

    public static char ToCharacter(this string value, ValueType valueType)
    {
        var result = (char)int.Parse(value, NumberStyles.HexNumber);
        return result;
    }
}