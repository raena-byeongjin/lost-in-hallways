public class SOUND : SOUND_BASE
{
	//효과음
	public static tagSoundStatic	ResourceTake	= null;
	public static tagSoundStatic	Boil			= null;
	public static tagSoundStatic	StoveComplete	= null;
	public static tagSoundStatic	StoveFailed		= null;

	//Lagecy
	public static tagSoundStatic	CoinPour	= null;
	public static tagSoundStatic	CoinTake	= null;

	public static void Initialize()
	{
		ResourceTake	= Sound.Find("b168ebc7653dcg833fa6");
		Boil			= Sound.Find("c0d7gfcc7d2f3483fba3");
		StoveComplete	= Sound.Find("d29d793496f7c4dcaca9");
		StoveFailed		= Sound.Find("gf04aee78007g9g75a25");

		//Lagecy
		BUTTON			= Sound.Find("a6c718f68b72841d57a5");
		POPUP			= Sound.Find("f0e43gf0b48g087eac66");
		CoinPour		= Sound.Find("gaf81g9e3cdgff5f15fe");
		CoinTake		= Sound.Find("g59a41337aab70ab91g3");
	}
}