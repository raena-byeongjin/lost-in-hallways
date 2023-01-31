using UnityEngine;

public class CCharacter : FrameworkBehaviour
{
	//캐릭터를 생성하기 위한 함수
	public CharacterBehaviour Create( Vector3 position, Quaternion rotation, tagCharacterStatic characterstatic )
	{
		if( characterstatic==null ) return null;

		Transform transform = Library.Create( position, rotation, characterstatic.GetMainAsset() );
		GameObject gameObject = transform.gameObject;

		Library.MaterialSetup(gameObject);
		AssetLoader.ON( transform, gameObject );

		CharacterBehaviour characterbehaviour = transform.GetComponent(typeof(CharacterBehaviour)) as CharacterBehaviour;
		if( characterbehaviour==null )
		{
			characterbehaviour = gameObject.AddComponent(typeof(CharacterBehaviour)) as CharacterBehaviour;
		}

		return characterbehaviour;
	}
}