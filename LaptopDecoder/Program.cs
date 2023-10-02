using System.Threading;

Console.BufferWidth = 2048;
Console.BufferHeight = 2048;
Interface _ui = new Interface(false);

while (true)
{
    Console.Clear();
    _ui.WriteDecoders();
    _ui.WriteSeparators();
    _ui.WriteValues();

    var parameters = Array.Empty<Parameter>();
    var decoder = _ui.GetDecoder();
    
    if (decoder is null) //reset
        continue;
    
    if (decoder.HasParameters)
        parameters = _ui.GetDecoderParameters(decoder);
    
    var decoderInstance = (DecoderBase)Activator.CreateInstance(decoder.Type);

    try
    {
        var decodedValues = _ui.Laptop.ValueBaseType switch
        {
            ValueBaseType.Cross => (DecoderResult)decoder.DecodeCrossMethod.Invoke(decoderInstance,
                new object[] { _ui.Laptop.Values.ToCrosses(), parameters }),
            ValueBaseType.Value => (DecoderResult)decoder.DecodeValueMethod.Invoke(decoderInstance,
                new object[] { _ui.Laptop.Values.ToValues(), parameters })
        };
        
        _ui.Laptop.Values = decodedValues.Values;
        _ui.Laptop.ValueBaseType = decodedValues.ValueBaseType;
    }
    catch (Exception e)
    {
        Console.Clear();
        Console.WriteLine("There was an error with the conversion. Resetting in 3 seconds.");
        Console.WriteLine("");
        Console.WriteLine(e.Message);
        Thread.Sleep(TimeSpan.FromMilliseconds(2000));
        _ui.Laptop.Reset();
    }
}