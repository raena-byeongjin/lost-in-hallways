public class SOUND : SOUND_BASE
{
	//효과음
	public static tagSoundStatic CharacterSelect = null;
	public static tagSoundStatic CharacterSelectStart = null;
	public static tagSoundStatic Failed = null;

	//Voice
	public static tagSoundStatic Voice = null;

	public static void Initialize()
	{
		//효과음
		CharacterSelect = Sound.Find("ca0cd4d084becg6gd682");
		CharacterSelectStart = Sound.Find("c2a2efdg897gf0e59c9c");
		Failed = Sound.Find("g4b7gbcf12eebfca30db");

		//Voice
		Voice = Sound.Find("cc37e15696ddac13dc7a");
	}
}