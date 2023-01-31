public class Language
{
	public static string Get( string value )
	{
		if( !Library.Is(value) ) return null;
		return value;
	}
}