Console.BufferWidth = 2048;
Console.BufferHeight = 2048;

Laptop _laptop = new Laptop();

while (true)
{
    var decoder = _laptop.GetDecoder();
    var key = string.Empty;

    if (decoder.RequiresKey)
        key = _laptop.GetDecoderKey();
    
    var decoderInstance = (DecoderBase)Activator.CreateInstance(decoder.Type);
    var decodedValues = (ValueBase[])decoder.DecodeMethod.Invoke(decoderInstance, new object[] { _laptop.Crosses, key });
    _laptop.WriteValues(decodedValues);
}