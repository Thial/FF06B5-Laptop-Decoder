namespace LaptopDecoder.Implementation;

public class Laptop
{
    public Laptop()
    {
        var enc = Encoding.ASCII;
        _baseLaptopValues = new[]
        {
            new Cross(new Value(ValueType.String, "P", enc), new Value(ValueType.String, "H", enc),
                new Value(ValueType.String, "P", enc), new Value(ValueType.String, "U", enc)),

            new Cross(new Value(ValueType.String, "V", enc), new Value(ValueType.String, "K", enc),
                new Value(ValueType.String, "P", enc), new Value(ValueType.String, "K", enc)),

            new Cross(new Value(ValueType.String, "V", enc), new Value(ValueType.String, "G", enc),
                new Value(ValueType.String, "V", enc), new Value(ValueType.String, "Z", enc)),

            new Cross(new Value(ValueType.String, "V", enc), new Value(ValueType.String, "S", enc),
                new Value(ValueType.String, "P", enc), new Value(ValueType.String, "N", enc)),

            new Cross(new Value(ValueType.String, "V", enc), new Value(ValueType.String, "B", enc),
                new Value(ValueType.String, "P", enc), new Value(ValueType.String, "B", enc)),

            new Cross(new Value(ValueType.String, "V", enc), new Value(ValueType.String, "D", enc),
                new Value(ValueType.String, "V", enc), new Value(ValueType.String, "D", enc)),

            new Cross(new Value(ValueType.String, "O", enc), new Value(ValueType.String, "Y", enc),
                new Value(ValueType.String, "Y", enc), new Value(ValueType.String, "Y", enc)),

            new Cross(new Value(ValueType.String, "O", enc), new Value(ValueType.String, "W", enc),
                new Value(ValueType.String, "O", enc), new Value(ValueType.String, "K", enc)),

            new Cross(new Value(ValueType.String, "Y", enc), new Value(ValueType.String, "N", enc),
                new Value(ValueType.String, "Y", enc), new Value(ValueType.String, "S", enc)),

            new Cross(new Value(ValueType.String, "O", enc), new Value(ValueType.String, "D", enc),
                new Value(ValueType.String, "O", enc), new Value(ValueType.String, "D", enc)),

            new Cross(new Value(ValueType.String, "Y", enc), new Value(ValueType.String, "F", enc),
                new Value(ValueType.String, "O", enc), new Value(ValueType.String, "F", enc)),

            new Cross(new Value(ValueType.String, "H", enc), new Value(ValueType.String, "G", enc),
                new Value(ValueType.String, "U", enc), new Value(ValueType.String, "Z", enc)),

            new Cross(new Value(ValueType.String, "U", enc), new Value(ValueType.String, "T", enc),
                new Value(ValueType.String, "U", enc), new Value(ValueType.String, "I", enc)),

            new Cross(new Value(ValueType.String, "H", enc), new Value(ValueType.String, "B", enc),
                new Value(ValueType.String, "U", enc), new Value(ValueType.String, "B", enc)),

            new Cross(new Value(ValueType.String, "H", enc), new Value(ValueType.String, "F", enc),
                new Value(ValueType.String, "H", enc), new Value(ValueType.String, "F", enc)),

            new Cross(new Value(ValueType.String, "K", enc), new Value(ValueType.String, "Z", enc),
                new Value(ValueType.String, "W", enc), new Value(ValueType.String, "G", enc)),

            new Cross(new Value(ValueType.String, "W", enc), new Value(ValueType.String, "B", enc),
                new Value(ValueType.String, "W", enc), new Value(ValueType.String, "B", enc)),

            new Cross(new Value(ValueType.String, "W", enc), new Value(ValueType.String, "D", enc),
                new Value(ValueType.String, "K", enc), new Value(ValueType.String, "D", enc))
        };
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
    private Cross[] _baseLaptopValues;

    Decoder[] GetDecoders()
    {
        var decoderTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.FullName == $"LaptopDecoder.Decoders.{t.Name}")
            .OrderBy(t => t.Name)
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