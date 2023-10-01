# FF06B5-Laptop-Decoder

## Install .NET Framework (at least 4.7.2) https://dotnet.microsoft.com/en-us/download/dotnet-framework

## Maximize the console window. Otherwise Windows will cry if your results exceed the window size.

If you want to create a decoder which requires a key just add a `[RequiresKey]` attribute above the class.

Each decoder contains 2 methods: `DecodeCross` and `DecodeValue`.

`DecodeCross` method is hit when the decoder input collection consists of `Cross` type objects.

`DecodeValue` method is hit when the decoder input collection consists of `Value` type objects.

Example decoder:

```csharp
namespace LaptopDecoder.Decoders;

[RequiresKey]
public class HillCipher : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, string key = "")
    {
        var result = //some logic
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, string key = "")
    {
        var result = //some logic
        return new DecoderResult(result);
    }
}
```
