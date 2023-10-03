namespace LaptopDecoder.Decoders;

[Parameter("AMOUNT", ValueType.Decimal)]
public class ToCipherROTUpperAndLower : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var amount = parameters.First(p => p.Name == "AMOUNT").ToDecimal();
        var newCrosses = new ValueBase[values.Length];
        for (var crossIndex = 0; crossIndex < values.Length; crossIndex++)
        {
            var cross = values[crossIndex].ToCharacter();
            var newValues = new Value[cross.Values.Length];
            for (var valueIndex = 0; valueIndex < cross.Values.Length; valueIndex++)
            {
                var value = cross.Values[valueIndex];
                var isEmpty = StringExtensions.IsNullOrEmptyOrSpace(value);
                var newValue = isEmpty ? "" : GetCharacter(value[0], amount).ToString();
                newValues[valueIndex] = new Value(ValueType.String, newValue, cross.Encoding);
            }

            newCrosses[crossIndex] = new Cross(
                newValues[0],
                newValues[1],
                newValues[2],
                newValues[3]);
        }

        return new DecoderResult(newCrosses);
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
        var lookup = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        var order = value.ToString().ToOrder()[0];
        var rotation = order + amount;
        var mod = rotation % lookup.Length;

        var newOrder = mod > 0 ? mod : order;

        return lookup[(int)newOrder - 1];
    }
}