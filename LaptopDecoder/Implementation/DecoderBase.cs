namespace LaptopDecoder.Implementation;

public abstract class DecoderBase
{
    public abstract ValueBase[] Decode(Cross[] crosses, string key);
}