# FF06B5-Laptop-Decoder

Install .NET Framework (at least 4.7.2) https://dotnet.microsoft.com/en-us/download/dotnet-framework

If you want to create a decoder which requires a key just add a `[RequiresKey]` attribute above the class

```csharp
namespace LaptopDecoder.Decoders;

[RequiresKey]
public class CrossHillCipher : DecoderBase
{
    public override ValueBase[] Decode(Cross[] crosses, string key)
    {
        // do logic
    }
}
```
