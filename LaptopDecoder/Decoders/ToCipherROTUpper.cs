namespace LaptopDecoder.Decoders;

[Parameter("AMOUNT", ValueType.Decimal)]
public class ToCipherROTUpper : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var result = values
            .Select(c => new Cross(DecodeValue(c.Values, parameters)))
            .Cast<ValueBase>()
            .ToArray();

        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var amount = parameters.First(p => p.Name == "AMOUNT").ToDecimal();
        var newValues = new ValueBase[values.Length];
        for (var valueIndex = 0; valueIndex < values.Length; valueIndex++)
        {
            var value = values[valueIndex].TheValue;
            var isEmpty = StringExtensions.IsNullOrEmptyOrSpace(value);
            var newValue = isEmpty ? "" : GetCharacter(value[0], amount).ToString();
            newValues[valueIndex] = new Value(ValueType.String, newValue, values[valueIndex].Encoding);
        }
        
        return new DecoderResult(newValues);
    }

    char GetCharacter(char value, long amount)
    {
        var lookup = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        var order = value.ToString().ToOrder()[0];
        var rotation = order + amount;
        var mod = rotation % lookup.Length;

        var newOrder = mod > 0 ? mod : order;

        return lookup[(int)newOrder - 1];
    }
}