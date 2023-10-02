namespace LaptopDecoder.Implementation;

public class Decoder
{
    public int Index { get; set; }
    public string Name { get; set; }
    public Type Type { get; set; }
    public MethodInfo DecodeCrossMethod { get; set; }
    public MethodInfo DecodeValueMethod { get; set; }

    public bool HasParameters
        => Type.GetCustomAttributes(typeof(ParameterAttribute), true).FirstOrDefault() != null;
}