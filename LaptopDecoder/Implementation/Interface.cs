namespace LaptopDecoder.Implementation;

public class Interface
{
    public Interface(bool writeSeparator)
    {
        _writeSeparator = writeSeparator;
        _laptop = new Laptop();
        _decodersLength = _laptop.Decoders.Select(d => d.Name.Length).Max();
        _valuesOffset = writeSeparator ? 60 : 50;
        _separatorOffset = 50;
    }

    public Laptop Laptop
        => _laptop;
    
    public Decoder GetDecoder()
    {
        var decoderIndex = default(int?);
        var decoderCount = _laptop.Decoders.Length;
        
        while (decoderIndex == null)
        {
            Console.SetCursorPosition(0, decoderCount);
            Console.Write("R = Reset");
            Console.SetCursorPosition(0, decoderCount + 2);
            Console.Write("Your choice: ");
            
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                continue;

            if (input.ToLower() == "r") //reset
            {
                _laptop.Reset();
                return null;
            }

            if (int.TryParse(input, out var parsedInput))
                decoderIndex = parsedInput;
        }

        return decoderIndex >= _laptop.Decoders.Length 
            ? null 
            : _laptop.Decoders[decoderIndex ?? 0];
    }
    
    public string GetDecoderKey()
    {
        var decoderKey = string.Empty;
        var decoderCount = _laptop.Decoders.Length;
        
        while (string.IsNullOrEmpty(decoderKey))
        {
            Console.SetCursorPosition(0, decoderCount + 3);
            Console.Write("Please enter the key: ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) == false)
                decoderKey = input;
        }

        return decoderKey;
    }
    
    public void WriteDecoders()
    {
        foreach (var decoder in _laptop.Decoders)
        {
            Console.SetCursorPosition(0, decoder.Index);
            Console.WriteLine($"{decoder.Index} = {decoder.Name}");
        }
    }

    public void WriteSeparators()
    {
        if (_writeSeparator == false)
            return;
        
        for (var i = 0; i < 14; i++)
        {
            Console.SetCursorPosition(_separatorOffset, i);
            Console.Write("|");
        }
    }
    
    public void WriteValues()
    {
        var valueWidth = _laptop.Values.Select(v => v.GetValueWidth()).Max();
        
        for (var y = 0; y < 3; y++)
        for (var x = 0; x < 6; x++)
        {
            var value = _laptop.Values[y * 6 + x];
            var caretX = x * (valueWidth + 3);
            var caretY = value.BaseType == ValueBaseType.Cross
                ? y * 4
                : y * 2;
            value.Write(_valuesOffset, valueWidth, caretX, caretY);
        }
    }
    
    #region Implementation
    
    readonly bool _writeSeparator;
    readonly Laptop _laptop;
    private readonly int _decodersLength;
    private readonly int _valuesOffset;
    private readonly int _separatorOffset;

    #endregion
}