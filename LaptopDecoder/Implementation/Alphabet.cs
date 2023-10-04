namespace LaptopDecoder.Implementation;

public class Alphabet
{
	public Alphabet(string characters)
	{
		_chars = characters.Distinct().Select(Char.ToUpperInvariant).ToArray();
		_str = new string(_chars);
	}

	public int Length => _chars.Length;

	public int IndexOfIgnoreCase(char c) 
		=> _str.IndexOf(c.ToString(), StringComparison.OrdinalIgnoreCase);

	public char AtMod(int index) => _chars[index switch
	{
		< 0 => (Length - (-index % Length)) % Length,
		_ => index % Length,
	}];
	
	public char[] ToCharArray() => 
		_chars.ToArray();

	public override string ToString() 
		=> _str;

	#region Implementation
	private readonly char[] _chars;
	private readonly string _str;
	#endregion
}