using UnityEngine;

public class CCharacter : FrameworkBehaviour
{
	private CharacterBehaviour m_rollover = null;

	//캐릭터를 생성하기 위한 함수
	public CharacterBehaviour Create( Vector3 position, Quaternion rotation, tagCharacterStatic characterstatic )
	{
		if( characterstatic==null ) return null;

		Transform transform = Library.Create( position, rotation, characterstatic.GetMainAsset() );
		GameObject gameObject = transform.gameObject;

		Library.MaterialSetup(gameObject);
		AssetLoader.ON( transform, gameObject );

		CharacterBehaviour characterbehaviour = gameObject.AddComponent(typeof(CharacterBehaviour)) as CharacterBehaviour;
		if( characterbehaviour!=null )
		{
			characterbehaviour.Set(characterstatic);
		}

		return characterbehaviour;
	}

	//롤오버를 처리하기 위한 함수
	public void Rollover( CharacterBehaviour characterbehaviour )
	{
//		if( characterbehaviour==null ) return; //(NULL)값을 허용함

		if( m_rollover!=null )
		{
			m_rollover.Outline(false);
		}

		m_rollover = characterbehaviour;

		if( m_rollover!=null )
		{
			m_rollover.Outline(true);
		}
	}

	//롤아웃을 처리하기 위한 함수
	public void Rollout( CharacterBehaviour characterbehaviour )
	{
//		if( characterbehaviour==null ) return; //(NULL)값을 허용함

		if( m_rollover==characterbehaviour )
		{
			m_rollover = null;
		}

		characterbehaviour.Outline(false);
	}
}