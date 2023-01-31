using UnityEngine.EventSystems;

public class ButtonBehaviour : RectTransformBehaviour
{
	public virtual void OnClick()
	{
		if( UserInterface.Is() && IsAllow() )
		{
			ONCLICK();
		}
	}

	protected virtual bool ONCLICK()
	{
#if UNITY_EDITOR
		UnityEngine.Debug.Log(this, this);
#endif

		return false;
	}

	public virtual bool IsAllow()
	{
		return true;
	}
}