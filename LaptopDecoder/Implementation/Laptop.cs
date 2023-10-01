namespace LaptopDecoder.Implementation;

public class Laptop
{
    public Laptop()
    {
        _decoders = GetDecoders();
        Reset();
    }

    public ValueBase[] Values
    {
        get => _values;
        set => _values = value;
    }
    
    public Cross[] BaseLaptopValues
        => _baseLaptopValues;

    public Decoder[] Decoders
        => _decoders;

    public ValueBaseType ValueBaseType
    {
        get => _valueBaseType;
        set => _valueBaseType = value;
    }

    public void Reset()
    {
        _values = _baseLaptopValues.Select(c => (ValueBase)c).ToArray();
        _valueBaseType = ValueBaseType.Cross;
    }
    
    #region Implementation

    private Decoder[] _decoders;
    private ValueBase[] _values;
    private ValueBaseType _valueBaseType;
    Cross[] _baseLaptopValues = new[]
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
            
            var decodeCrossMethod = decoderType.GetMethod("DecodeCross");
            if (decodeCrossMethod is null)
                throw new Exception($"Decoder {decoderType.FullName} does not contain a DecodeCross method");
            
            var decodeValueMethod = decoderType.GetMethod("DecodeValue");
            if (decodeValueMethod is null)
                throw new Exception($"Decoder {decoderType.FullName} does not contain a DecodeValue method");
            
            decoders.Add(new Decoder()
            {
                Index = decoderIndex,
                Name = decoderType.Name,
                Type = decoderType,
                DecodeCrossMethod = decodeCrossMethod,
                DecodeValueMethod = decodeValueMethod
            });
        }

        return decoders.ToArray();
    }
    #endregion
}