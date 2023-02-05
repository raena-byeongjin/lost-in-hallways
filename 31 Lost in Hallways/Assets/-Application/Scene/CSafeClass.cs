<<<<<<< .mine
﻿using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CSafeClass : SceneFramework
{
	private List<Camera> Cameras = new List<Camera>();
	private Camera m_camera = null;

	//인터페이스를 활성화 하기 위한 함수
	public override void ON()
	{
		app.CharacterStatic.AllDownload();
		app.SceneStatic.Load( "b2f71d11d5gga376c35a", true, funcSceneLoad );

		ViewSafeClass.ON();
	}

	void funcSceneLoad( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(Scene) ) return;

		Scene scene = (Scene)wParam;
		Component[] comArray = null;
		foreach( GameObject gameObject in scene.GetRootGameObjects() )
		{
			AssetLoader.ON( gameObject.transform, gameObject );

			comArray = gameObject.GetComponentsInChildren( typeof(Camera), true );
			foreach( Camera camera in comArray )
			{
				Cameras.Add( camera );

				if( m_camera==null && camera.isActiveAndEnabled )
				{
					m_camera = camera;
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
			foreach( Camera camera in Cameras )
			{
				if( isSkip )
				{
					ChangeCamera( m_camera, camera );
					return;
				}
				else
				if( camera==m_camera )
				{
					isSkip = true;
				}
			}

			ChangeCamera( m_camera, Cameras[0] );
		}
	}

	void ChangeCamera( Camera pre, Camera change )
	{
		if( pre==null ) return;
		if( change==null ) return;
		if( pre==change ) return;

		Library.Inactive( pre.gameObject );
		Library.Active( change.gameObject );

		m_camera = change;
	}
||||||| .r91
﻿using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CSafeClass : SceneFramework
{
	private List<Camera> Cameras = new List<Camera>();
	private Camera m_camera = null;

	//인터페이스를 활성화 하기 위한 함수
	public override void ON()
	{
		app.CharacterStatic.AllDownload();
		app.SceneStatic.Load( "b2f71d11d5gga376c35a", true, funcSceneLoad );

		ViewSafeClass.ON();
	}

	void funcSceneLoad( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(Scene) ) return;

		Scene scene = (Scene)wParam;
		Component[] comArray = null;
		foreach( GameObject gameObject in scene.GetRootGameObjects() )
		{
			AssetLoader.ON( gameObject.transform, gameObject );

			comArray = gameObject.GetComponentsInChildren( typeof(Camera), true );
			foreach( Camera camera in comArray )
			{
				Cameras.Add( camera );

				if( m_camera==null && camera.isActiveAndEnabled )
				{
					m_camera = camera;
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
			foreach( Camera camera in Cameras )
			{
				if( isSkip )
				{
					ChangeCamera( m_camera, camera );
					return;
				}
				else
				if( camera==m_camera )
				{
					isSkip = true;
				}
			}

			ChangeCamera( m_camera, Cameras[0] );
		}
	}

	void ChangeCamera( Camera pre, Camera change )
	{
		if( pre==null ) return;
		if( change==null ) return;
		if( pre==change ) return;

		Library.Inactive( pre.gameObject );
		Library.Active( change.gameObject );

		m_camera = change;
	}
=======
﻿using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CSafeClass : SceneFramework
{
	private List<Camera> Cameras = new List<Camera>();
	private Camera m_camera = null;

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
		foreach( GameObject gameObject in scene.GetRootGameObjects() )
		{
			AssetLoader.ON( gameObject.transform, gameObject );

			comArray = gameObject.GetComponentsInChildren( typeof(Camera), true );
			foreach( Camera camera in comArray )
			{
				Cameras.Add( camera );

				if( m_camera==null && camera.isActiveAndEnabled )
				{
					m_camera = camera;
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
			foreach( Camera camera in Cameras )
			{
				if( isSkip )
				{
					ChangeCamera( m_camera, camera );
					return;
				}
				else
				if( camera==m_camera )
				{
					isSkip = true;
				}
			}

			ChangeCamera( m_camera, Cameras[0] );
		}
	}

	void ChangeCamera( Camera pre, Camera change )
	{
		if( pre==null ) return;
		if( change==null ) return;
		if( pre==change ) return;

		Library.Inactive( pre.gameObject );
		Library.Active( change.gameObject );

		m_camera = change;
	}
>>>>>>> .r99
}