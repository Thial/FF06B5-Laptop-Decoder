# FF06B5-Laptop-Decoder

## Install .NET Framework (at least 4.7.2) https://dotnet.microsoft.com/en-us/download/dotnet-framework

## Maximize the console window. Otherwise Windows will cry if your results exceed the window size.

If you want to create a decoder which requires a key just add a `[Parameter("somename", ValueType.Decimal)]` attribute above the class.

Each decoder contains 2 methods: `DecodeCross` and `DecodeValue`.

`DecodeCross` method is hit when the decoder input collection consists of `Cross` type objects.

`DecodeValue` method is hit when the decoder input collection consists of `Value` type objects.

Example decoder:

```csharp
namespace LaptopDecoder.Decoders;

[Parameter("KEY", ValueType.String)]
[Parameter("SALT", ValueType.Hex)]
public class SomeCipher : DecoderBase
{
    public override DecoderResult DecodeCross(Cross[] values, Parameter[] parameters)
    {
        var result = //some logic
        return new DecoderResult(result);
    }

    public override DecoderResult DecodeValue(Value[] values, Parameter[] parameters)
    {
        var result = //some logic
        return new DecoderResult(result);
    }
}
```
