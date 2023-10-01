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

    var key = string.Empty;
    var decoder = _ui.GetDecoder();
    
    if (decoder is null) //reset
        continue;
    
    if (decoder.RequiresKey)
        key = _ui.GetDecoderKey();
    
    var decoderInstance = (DecoderBase)Activator.CreateInstance(decoder.Type);

    try
    {
        var decodedValues = _ui.Laptop.ValueBaseType switch
        {
            ValueBaseType.Cross => (DecoderResult)decoder.DecodeCrossMethod.Invoke(decoderInstance,
                new object[] { _ui.Laptop.Values.ToCrosses(), key }),
            ValueBaseType.Value => (DecoderResult)decoder.DecodeValueMethod.Invoke(decoderInstance,
                new object[] { _ui.Laptop.Values.ToValues(), key })
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