using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

//장면 정보를 처리하기 위한 클래스
public class CSceneStatic : AppsItemListener
{
	public List<tagSceneStatic> Scenes = new List<tagSceneStatic>();

	protected override void Awake()
	{
		base.Awake();
		Initialize( "Scenes", UPDATE_CLASS.Scene.ToString() );
	}

    //객체를 생성하기 위한 함수 
    protected override tagAppsItem New()
    {
        return new tagSceneStatic();
    }

	//객체를 삭제하기 위한 함수 
	protected override void OnDelete( tagAppsItem appsitem )
	{
		if( appsitem==null ) return;
		GetList().Remove( appsitem as tagSceneStatic );
    }

	//리스트를 얻기 위한 함수
	public List<tagSceneStatic> GetList()
	{
		return Scenes;
	}

	//모델 정보를 검색하기 위한 함수
    public tagSceneStatic Find( string id )
    {
		if( !Library.Is(id) ) return null;

		foreach( tagSceneStatic scenestatic in GetList() )
		{
			if( scenestatic.id==id )
			{
				return scenestatic;
			}
        }

		return null;
    }

	public void Load( string id, bool activeScene=true, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( !Library.Is(id) ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		tagSceneStatic scenestatic = Find(id);
		if( scenestatic!=null )
		{
			tagCallback callback = null;
			if( func!=null )
			{
				callback = new tagCallback( func, wParam, lParam );
			}

			app.SceneStatic.Download( scenestatic, funcSceneDownloadEnd, new DescSceneLoad( scenestatic, activeScene ), callback );
		}
	}

	void funcSceneDownloadEnd( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam as DescSceneLoad==null ) return;
//		if( lParam==null || lParam.GetType()!=typeof(tagCallback) ) return; //(NULL)값을 허용함

		DescSceneLoad desc = wParam as DescSceneLoad;
		tagSceneStatic scenestatic = desc.scenestatic;
		if( scenestatic.AssetBundle().IsLoad() )
		{
			string[] Scenes = scenestatic.AssetBundle().bundle.GetAllScenePaths();
			foreach( string sceneName in Scenes )
			{
				desc.sceneName = Library.GetFileName(sceneName);
				AsyncOperation asyncOperation = Library.LoadScene( sceneName, funcSceneLoadEnd, desc, lParam );
				if( asyncOperation!=null )
				{
//					app.Progress.ON( null, asyncOperation ); 
				}

				break;
			}
		}
	}

	void funcSceneLoadEnd( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam as DescSceneLoad==null ) return;
//		if( lParam==null || lParam.GetType()!=typeof(tagCallback) ) return; //(NULL)값을 허용함

		DescSceneLoad desc = wParam as DescSceneLoad;
		Scene scene = Library.GetScene(desc.sceneName);
		if( scene.isLoaded )
		{
			if( desc.activeScene )
			{
				Library.ActiveScene(scene);
			}

			if( RenderSettings.skybox!=null )
			{
				RenderSettings.skybox.shader = Shader.Find(RenderSettings.skybox.shader.name);
			}

			foreach( GameObject gameObject in scene.GetRootGameObjects() )
			{
				Library.MaterialSetup(gameObject);
			}
		}

		if( lParam!=null && lParam.GetType()==typeof(tagCallback) )
		{
			tagCallback callback = lParam as tagCallback;
			if( callback!=null && callback.Is() )
			{
				if( callback.wParam==null ) callback.wParam = scene;
				callback.Call();
			}
		}
	}

	//로컬에 저장된 정보를 불러오기 위한 함수
	public override void OnLoadCache( tagAppsItem appsitem, AppsParameter col=null )
	{
		if( appsitem==null ) return;
//		if( col==null ) return; //(NULL)값을 허용함

		tagSceneStatic scenestatic = appsitem as tagSceneStatic;
		if( scenestatic==null ) return;

		if( col!=null )
		{
		}

		if( !GetList().Contains(scenestatic) )
		{
			GetList().Add(scenestatic);
		}
	}

	//XML 파일을 저장하기 위한 함수
	public override void OnXmlSave( XmlTextWriter xmlWriter, tagAppsItem appsitem )
	{
  		if( xmlWriter==null ) return;
		if( appsitem==null ) return;

		tagSceneStatic scenestatic = appsitem as tagSceneStatic;
		if( scenestatic==null ) return;
	}

	//XML 파일을 불러오기 위한 함수
	public override void OnXmlLoad( XmlNode pNode, tagAppsItem appsitem )
	{
 		if( pNode==null ) return;
		if( appsitem==null ) return;

		tagSceneStatic scenestatic = appsitem as tagSceneStatic;
		if( scenestatic==null ) return;
	}
}