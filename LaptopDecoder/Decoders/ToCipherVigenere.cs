namespace LaptopDecoder.Decoders;

[Parameter("ALPHABET", ValueType.String)]
[Parameter("KEY", ValueType.String)]
public class ToCipherVigenere : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var input = CrossesToString(values);
        var key = GetKey(input.Length, parameters);
        var alphabet = GetAlphabet(parameters);
        var decryptResult = Decrypt(input, key, alphabet, values[0].Encoding);

        var newCrosses = new List<ValueBase>();
        var crossValues = new List<ValueBase>();
        
        for (var valueIndex = 0; valueIndex < decryptResult.Values.Length; valueIndex++)
        {
            crossValues.Add(decryptResult.Values[valueIndex]);

            if (crossValues.Count == 4)
            {
                newCrosses.Add(new Cross(new DecoderResult(crossValues.ToArray())));
                crossValues.Clear();
            }
        }
        
        return new DecoderResult(newCrosses.ToArray());
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var input = ValuesToString(values);
        var key = GetKey(input.Length, parameters);
        var alphabet = GetAlphabet(parameters);
        return Decrypt(input, key, alphabet, values[0].Encoding);
    }
    
    Alphabet GetAlphabet(Parameter[] parameters)
    {
        var keyParameter = parameters.First(p => p.Name == "ALPHABET");
        return new Alphabet(keyParameter.Value);
    }

    string GetKey(int inputLength, Parameter[] parameters)
    {
        var keyParameter = parameters.First(p => p.Name == "KEY");
        var key = string.Empty;
        while (key.Length < inputLength)
        {
            key += keyParameter.Value;
        }
        return key;
    }

    DecoderResult Decrypt(string input, string key, Alphabet alphabet, Encoding encoding)
    {
        var result = new List<ValueBase>();
        var counter = 0;
        for (var characterIndex = 0; characterIndex < input.Length; characterIndex++)
        {
            var valueChar = input[characterIndex];
            if (valueChar == ' ')
            {
                result.Add(new Value(ValueType.String, " ", encoding));
                continue;
            }
            
            var character = alphabet.IndexOfIgnoreCase(valueChar) switch
            {
                -1 => valueChar,
                var idx => alphabet.AtMod(idx - alphabet.IndexOfIgnoreCase(key[counter++ % key.Length]))
            };
            
            result.Add(new Value(ValueType.String, character.ToString(), encoding));
        }

        return new DecoderResult(result.ToArray());
    }
    
    string CrossesToString(Cross[] values)
        => values.Aggregate(string.Empty, (current, cross) => current + ValuesToString(cross.Values));

    string ValuesToString(Value[] values)
        => values.Aggregate(string.Empty, (current, value) => current + (StringExtensions.IsNullOrEmptyOrSpace(value.TheValue)
            ? " "
            : value.TheValue[0]));
}