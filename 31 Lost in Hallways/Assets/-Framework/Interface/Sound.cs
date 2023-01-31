public class Sound
{
	public static tagSoundStatic Find( string value )
	{
		if( !Library.Is(value) ) return null;
		return ApplicationBehaviour.This.SoundStatic.FindFromId(value) as tagSoundStatic;
	}
}