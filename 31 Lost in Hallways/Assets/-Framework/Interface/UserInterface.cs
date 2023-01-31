using UnityEngine;

public class UserInterface
{
	public static void In( Component component )
	{
		if( component==null ) return;
		Debug.Log(component);
	}

	public static void Out( Component component )
	{
		if( component==null ) return;
		Debug.Log(component);
	}

	public static bool Is()
	{
		return true;
	}
}