namespace LaptopDecoder.Extensions;

public static class StringExtensions
{
    public static long[] ToDecimal(this string value, ValueType valueType)
    {
        if (IsNullOrEmptyOrSpace(value))
            return Array.Empty<long>();
        
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
        if (IsNullOrEmptyOrSpace(value))
            return Array.Empty<long>();
        
        var result = value.Select(c => (long)c - 64).ToArray();
        return result;
    }

    public static string ToHex(this string value, ValueType valueType)
    {
        if (IsNullOrEmptyOrSpace(value))
            return value;
        
        var result = BitConverter.ToString(Encoding.UTF8.GetBytes(value)).Replace("-", String.Empty);
        return result;
    }

    public static string ToCharacter(this string value, ValueType valueType, Encoding encoding)
    {
        if (IsNullOrEmptyOrSpace(value))
            return value;

        var result = valueType switch
        {
            ValueType.Decimal => (char)int.Parse(value.ToDecimal(valueType)[0].ToString("X"), NumberStyles.HexNumber),
            ValueType.Hex => (char)int.Parse(value, NumberStyles.HexNumber),
            ValueType.String => (char)int.Parse(BitConverter.ToString(encoding.GetBytes(value)).Replace("-",""), NumberStyles.HexNumber)
        };

        return result.ToString();
    }

    public static string ToEncoding(this string value, Encoding oldEncoding, Encoding newEncoding)
    {
        if (IsNullOrEmptyOrSpace(value))
            return string.Empty;

        var oldBytes = oldEncoding.GetBytes(value);
        var newBytes = Encoding.Convert(oldEncoding, newEncoding, oldBytes);
        var chars = newEncoding.GetChars(newBytes);

        return string.Join("", chars);
    }

    public static bool IsNullOrEmptyOrSpace(string value)
    {
        return string.IsNullOrEmpty(value) || value == " ";
    }
}