using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CharacterPosition : AssetLoaderBehaviour
{
	private static List<CharacterPosition> CharacterPositions = new List<CharacterPosition>();
	private int index = 0;

	protected override void Awake()
	{
		base.Awake();

		Library.Destroy( GetComponent(typeof(MeshRenderer)) );
		Library.Destroy( GetComponent(typeof(MeshFilter)) );

		CharacterPositions.Add(this);
	}

	void OnDestroy()
	{
		CharacterPositions.Remove(this);
	}

	public override void Initialize( AssetComponent asset, XmlNode pNode )
	{
		if( asset==null ) return;
		if( pNode==null ) return; //(NULL)값을 허용함

		if( Library.IsNumber(asset.value) )
		{
			index = int.Parse(asset.value);
		}
	}

	public static CharacterPosition Find( int index )
	{
		foreach( CharacterPosition characterposition in CharacterPositions )
		{
			if( characterposition.index==index )
			{
				return characterposition;
			}
		}

		return null;
	}
}