using UnityEngine;

public class AssetLoader
{
	public static void ON( Transform transform, GameObject gameObject=null )
	{
		if( transform==null ) return;
//		if( gameObject==null ) return;	//(NULL)값을 허용함

		ApplicationBehaviour.This.AssetLoader.ON( transform, gameObject );
	}
}