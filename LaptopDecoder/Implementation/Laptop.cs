namespace LaptopDecoder.Implementation;

public class Laptop
{
    public Laptop()
    {
        _decoders = GetDecoders();
    }
    
    public Cross[] Crosses
        => _crosses;

    public Decoder GetDecoder()
    {
        Console.Clear();
        
        foreach (var decoder in _decoders)
        {
            Console.WriteLine($"{decoder.Index} = {decoder.Name}");
        }

        var decoderIndex = default(int?);
        while (decoderIndex == null)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("Please select the decoder: ");
            var input = Console.ReadLine();

            if (input != null && int.TryParse(input, out var parsedInput))
            {
                decoderIndex = parsedInput;
            }
        }

        return _decoders[decoderIndex ?? 0];
    }

    public string GetDecoderKey()
    {
        var decoderKey = string.Empty;
        while (string.IsNullOrEmpty(decoderKey))
        {
            Console.WriteLine("");
            Console.Write("Please enter the key: ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) == false)
                decoderKey = input;
        }

        return decoderKey;
    }
    
    public void WriteValues(ValueBase[] values)
    {
        Console.Clear();

        var valueWidth = values.Select(v => v.GetValueWidth()).Max();
        
        for (var y = 0; y < 3; y++)
        for (var x = 0; x < 6; x++)
        {
            var value = values[y * 6 + x];
            var caretX = x * (valueWidth + 3);
            var caretY = value.BaseType == ValueBaseType.Cross
                ? y * 4
                : y * 2;
            value.Write(valueWidth, caretX, caretY);
        }
        
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.Write("Press any key to decrypt again.");
        Console.ReadKey();
    }

    #region Implementation

    private Decoder[] _decoders;
    Cross[] _crosses = new[]
    {
        new Cross(new Value(ValueType.String, "P"), new Value(ValueType.String, "H"),
            new Value(ValueType.String, "P"), new Value(ValueType.String, "U")),

        new Cross(new Value(ValueType.String, "V"), new Value(ValueType.String, "K"),
            new Value(ValueType.String, "P"), new Value(ValueType.String, "K")),

        new Cross(new Value(ValueType.String, "V"), new Value(ValueType.String, "G"),
            new Value(ValueType.String, "V"), new Value(ValueType.String, "Z")),

        new Cross(new Value(ValueType.String, "V"), new Value(ValueType.String, "S"),
            new Value(ValueType.String, "P"), new Value(ValueType.String, "N")),

        new Cross(new Value(ValueType.String, "V"), new Value(ValueType.String, "B"),
            new Value(ValueType.String, "P"), new Value(ValueType.String, "B")),

        new Cross(new Value(ValueType.String, "V"), new Value(ValueType.String, "D"),
            new Value(ValueType.String, "V"), new Value(ValueType.String, "D")),

        new Cross(new Value(ValueType.String, "O"), new Value(ValueType.String, "Y"),
            new Value(ValueType.String, "Y"), new Value(ValueType.String, "Y")),

        new Cross(new Value(ValueType.String, "O"), new Value(ValueType.String, "W"),
            new Value(ValueType.String, "O"), new Value(ValueType.String, "K")),

        new Cross(new Value(ValueType.String, "Y"), new Value(ValueType.String, "N"),
            new Value(ValueType.String, "Y"), new Value(ValueType.String, "S")),

        new Cross(new Value(ValueType.String, "O"), new Value(ValueType.String, "D"),
            new Value(ValueType.String, "O"), new Value(ValueType.String, "D")),

        new Cross(new Value(ValueType.String, "Y"), new Value(ValueType.String, "F"),
            new Value(ValueType.String, "O"), new Value(ValueType.String, "F")),

        new Cross(new Value(ValueType.String, "H"), new Value(ValueType.String, "G"),
            new Value(ValueType.String, "U"), new Value(ValueType.String, "Z")),

        new Cross(new Value(ValueType.String, "U"), new Value(ValueType.String, "T"),
            new Value(ValueType.String, "U"), new Value(ValueType.String, "I")),

        new Cross(new Value(ValueType.String, "H"), new Value(ValueType.String, "B"),
            new Value(ValueType.String, "U"), new Value(ValueType.String, "B")),

        new Cross(new Value(ValueType.String, "H"), new Value(ValueType.String, "F"),
            new Value(ValueType.String, "H"), new Value(ValueType.String, "F")),

        new Cross(new Value(ValueType.String, "K"), new Value(ValueType.String, "Z"),
            new Value(ValueType.String, "W"), new Value(ValueType.String, "G")),

        new Cross(new Value(ValueType.String, "W"), new Value(ValueType.String, "B"),
            new Value(ValueType.String, "W"), new Value(ValueType.String, "B")),

        new Cross(new Value(ValueType.String, "W"), new Value(ValueType.String, "D"),
            new Value(ValueType.String, "K"), new Value(ValueType.String, "D"))
    };

    Decoder[] GetDecoders()
    {
        var decoderTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.FullName == $"LaptopDecoder.Decoders.{t.Name}")
            .ToArray();

        var decoderCount = decoderTypes.Length;
        var decoders = new List<Decoder>();

        for (var decoderIndex = 0; decoderIndex < decoderCount; decoderIndex++)
        {
            var decoderType = decoderTypes[decoderIndex];
            var decodeMethod = decoderType.GetMethod("Decode");
            if (decodeMethod is null)
                throw new Exception($"Decoder {decoderType.FullName} does not contain a Decode method");
            
            decoders.Add(new Decoder()
            {
                Index = decoderIndex,
                Name = decoderType.Name.Humanize(),
                Type = decoderType,
                DecodeMethod = decodeMethod
            });
        }

        return decoders.ToArray();
    }
    #endregion
}