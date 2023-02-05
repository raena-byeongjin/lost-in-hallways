using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CSafeClass : SceneFramework
{
	private List<CameraBehaviour> Cameras = new List<CameraBehaviour>();
	private CameraBehaviour m_camera = null;

	//인터페이스를 활성화 하기 위한 함수
	public override void ON()
	{
		app.CharacterStatic.AllDownload();
		app.SceneStatic.Load( "b2f71d11d5gga376c35a", true, funcSceneLoad );

		BGSOUND.AnguishedSoulV2Pno.ON();
		ViewSafeClass.ON();
	}

	void funcSceneLoad( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(Scene) ) return;

		Scene scene = (Scene)wParam;

		Component[] comArray = null;
		PhysicsRaycaster racaster = null;
		CameraBehaviour camerabehaviour = null;
		GameObject cameraObject = null;

		foreach( GameObject gameObject in scene.GetRootGameObjects() )
		{
			AssetLoader.ON( gameObject.transform, gameObject );

			comArray = gameObject.GetComponentsInChildren( typeof(Camera), true );
			foreach( Camera camera in comArray )
			{
				cameraObject = camera.gameObject;
				camerabehaviour = cameraObject.GetComponent(typeof(CameraBehaviour)) as CameraBehaviour;
				if( camerabehaviour==null )
				{
					camerabehaviour = cameraObject.AddComponent(typeof(CameraBehaviour)) as CameraBehaviour;
				}

				Cameras.Add( camerabehaviour );

				if( m_camera==null && camera.isActiveAndEnabled )
				{
					ChangeCamera( camerabehaviour );
					m_camera = camerabehaviour;
				}

				racaster = camera.GetComponent(typeof(PhysicsRaycaster)) as PhysicsRaycaster;
				if( racaster==null )
				{
					racaster = camera.gameObject.AddComponent(typeof(PhysicsRaycaster)) as PhysicsRaycaster;
				}
			}
		}

		DownloadInterface.SetCallback( funcDownloadEnd );
	}

	void funcDownloadEnd( object wParam=null, object lParam=null )
	{
		CharacterPosition characterposition = null;

		characterposition = CharacterPosition.Find(0);
		if( characterposition!=null )
		{
			app.Character.Create( characterposition.Transform().position, characterposition.Transform().rotation, app.CharacterStatic.Find(CHARACTER.타이치) );
		}

		characterposition = CharacterPosition.Find(1);
		if( characterposition!=null )
		{
			app.Character.Create( characterposition.Transform().position, characterposition.Transform().rotation, app.CharacterStatic.Find(CHARACTER.사토미) );
		}

		characterposition = CharacterPosition.Find(2);
		if( characterposition!=null )
		{
			app.Character.Create( characterposition.Transform().position, characterposition.Transform().rotation, app.CharacterStatic.Find(CHARACTER.아오이) );
		}

		characterposition = CharacterPosition.Find(3);
		if( characterposition!=null )
		{
			app.Character.Create( characterposition.Transform().position, characterposition.Transform().rotation, app.CharacterStatic.Find(CHARACTER.호노카) );
		}
	}

	//마우스 왼쪽 버튼을 뗬을 때 반응하기 위한 함수
	public override bool ONLBUTTONUP( Vector2 point, tagTouch touch )
	{
		ChangeCamera();
		return true;
	}

	void ChangeCamera()
	{
		if( Cameras.Count>=2 )
		{
			bool isSkip = false;

			foreach( CameraBehaviour camerabehaviour in Cameras )
			{
				if( isSkip )
				{
					ChangeCamera( camerabehaviour );
					return;
				}
				else
				if( camerabehaviour==m_camera )
				{
					isSkip = true;
				}
			}

			ChangeCamera( Cameras[0] );
		}
	}

	void ChangeCamera( CameraBehaviour change )
	{
		if( change==null ) return;
		if( m_camera==change ) return;

		if( m_camera!=null )
		{
			Library.Inactive( m_camera.gameObject );
		}

		Library.Active( change.gameObject );

		Framework.Set(change);
		m_camera = change;
	}
}