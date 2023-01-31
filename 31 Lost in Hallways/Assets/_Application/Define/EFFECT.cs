public class EFFECT
{
	public static tagEffectStatic Touch = null;
	public static tagEffectStatic StoveChar = null;

	public static void Initialize()
	{
		Touch = Effect.Find("fc8d002d8dbdee2e3f61");
		StoveChar = Effect.Find("a2b4fefga7d10345b9cf");
	}
}