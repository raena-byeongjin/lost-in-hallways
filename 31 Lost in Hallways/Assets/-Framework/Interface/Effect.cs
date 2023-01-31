using UnityEngine;

public class Effect
{
	public static Transform ON( Vector3 position, UnityEngine.Object sample, Transform parent=null )
	{
		if( sample==null ) return null;
//		if( parent==null ) return null; //(NULL)값을 허용함

		Transform transform = Library.Create( parent, sample );
		if( transform!=null )
		{
			transform.position = position;
			ApplicationBehaviour.This.AssetLoader.ON( transform, transform.gameObject );
		}

		return transform;
	}

	public static tagEffectStatic Find( string value )
	{
		if( !Library.Is(value) ) return null;
		return ApplicationBehaviour.This.EffectStatic.Find(value);
	}
}