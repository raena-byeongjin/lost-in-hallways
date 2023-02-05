public class ButtonExplore : ButtonBehaviour
{
	protected override bool ONCLICK()
	{
		ViewCharacterSelect.ON();
		return true;
	}
}