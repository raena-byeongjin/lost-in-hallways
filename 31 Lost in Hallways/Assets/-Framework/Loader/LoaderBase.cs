using UnityEngine.SceneManagement;
using UnityEngine;

//어플리케이션 정보를 불러오기 위한 클래스
public class LoaderBase : MonoBehaviour
{
	void Awake()
	{
		if( Platform.IsRuntime() && Application.platform==RuntimePlatform.WindowsPlayer )
		{
			Screen.SetResolution( 600, 840, FullScreenMode.Windowed );
		}

		if( Platform.IsAndroid() )
		{
			AndroidChromeInitialize();
		}
		/*
		if( Library.IsAndroid() )//&& Func.GetAndroidOSVersion()>=(10) )
		{
			//(X) 안드로이드 10 버전일 경우에만 화면 높이를 계산함
			//(X) 이전 버전에선 아예 오류 현상이 없으므로 작업을 진행할 필요가 없음
			//프로필 커버에서 Fullscreen을 사용하기 때문에, Fullscreen일 경우의 화면 높이 계산이 필요함
			(new GameObject()).AddComponent(typeof(NativeScreen));
		}
		*/

//		Instantiate( Func.Get( null, "ApplicationLoader/Canvas" ), Resources.Load("ApplicationLoader/Scene/SceneLogo") as GameObject );
	}

	void Start()
	{
		if( SceneManager.sceneCount>1 )
		{
			for( int i=SceneManager.sceneCount-1; i>0; i-- )
			{
				Scene scene = SceneManager.GetSceneAt(i);
				if( scene.buildIndex!=(int)SceneLevel.Startup )
				{
					SceneManager.UnloadSceneAsync(scene.buildIndex);
//					SceneManager.UnloadScene(scene.buildIndex);
				}
			}
		}
	}

	void LateUpdate()
	{
		if( enabled )
		{
			Load();

			GameObject.Destroy( gameObject );
			enabled = false;
		}
	}

	void Load()
	{
		Instantiate( null, Resources.Load("Application") );

		CPlay.This._System	= Instantiate( null, Resources.Load( "System", typeof(GameObject) ) );
		CPlay.This._Plugins	= Instantiate( null, Resources.Load( "Plugins", typeof(GameObject) ) );
		CPlay.This._Scene	= Instantiate( null, Resources.Load( "Scene", typeof(GameObject) ) );
		CPlay.This._UIRoot	= Instantiate( null, Resources.Load( "UIRoot", typeof(GameObject) ) );

		CPlay.This.layerMaps		= Func.Get( CPlay.This._Scene, "Maps" );
		CPlay.This.layerPlayers		= Func.Get( CPlay.This._Scene, "Players" );
		CPlay.This.layerCharacters	= Func.Get( CPlay.This._Scene, "Characters" );
		CPlay.This.layerStructures	= Func.Get( CPlay.This._Scene, "Structures" );
		CPlay.This.layerObjects		= Func.Get( CPlay.This._Scene, "Objects" );
		CPlay.This.layerTrees		= Func.Get( CPlay.This._Scene, "Trees" );
		CPlay.This.layerRocks		= Func.Get( CPlay.This._Scene, "Rocks" );
		CPlay.This.layerCoins		= Func.Get( CPlay.This._Scene, "Coins" );
		CPlay.This.layerEffects		= Func.Get( CPlay.This._Scene, "Effects" );

		Initialize();

#if UNITY_EDITOR
		CPlay.This._UIRoot.localPosition = new Vector3( 0f, 0f, -10000f );
#endif
	}

	void Initialize()
	{
		CPlay.This.mainCamera		= Get( CPlay.This._System, "mainCamera", typeof(CameraBehaviour) ) as CameraBehaviour;

		CPlay.This.foreCanvas		= Get( CPlay.This._UIRoot, "foreCanvas", typeof(CanvasBehaviour) ) as CanvasBehaviour;
		CPlay.This.forwardCanvas	= Get( CPlay.This._UIRoot, "forwardCanvas", typeof(CanvasBehaviour) ) as CanvasBehaviour;
		CPlay.This.uiCanvas			= Get( CPlay.This._UIRoot, "uiCanvas", typeof(CanvasBehaviour) ) as CanvasBehaviour;
		CPlay.This.backCanvas		= Get( CPlay.This._UIRoot, "backCanvas", typeof(CanvasBehaviour) ) as CanvasBehaviour;
		CPlay.This.sceneCanvas		= Get( CPlay.This._UIRoot, "sceneCanvas", typeof(CanvasBehaviour) ) as CanvasBehaviour;
	}

	public static Transform Instantiate( Transform parent, Object sample )
	{
//		if( parent==null ) return null;		//(NULL)값을 허용함
		if( sample==null ) return null;

		GameObject	sampleObject	= sample as GameObject;
		Transform	sampleTransform	= sampleObject.transform;

        GameObject	gameObject		= GameObject.Instantiate(sampleObject) as GameObject;
		Transform	transform		= gameObject.transform;

		transform.SetParent( parent );

		gameObject.name = sampleObject.name;
		return transform;
	}

	public static Component Instantiate( Transform parent, Object gameObject, System.Type type )
	{
		Transform transform = Instantiate( parent, gameObject );
		if( transform==null ) return null;

		return transform.GetComponent(type);
	}

	protected virtual void AndroidChromeInitialize()
	{
		//안드로이드 10 버전일 경우에만 화면 높이를 계산함
		//이전 버전에선 아예 오류 현상이 없으므로 작업을 진행할 필요가 없음
//		(new GameObject()).AddComponent(typeof(NativeScreen));

		Screen.fullScreen = false;
		ApplicationChrome.statusBarState		= ApplicationChrome.States.Visible;
		ApplicationChrome.navigationBarState	= ApplicationChrome.States.Visible;
		ApplicationChrome.navigationBarColor	= 0xff010101;
		ApplicationChrome.statusBarColor		= 0xff010101;
	}

	//컴포넌트를 얻기 위한 함수
	Component Get( Transform parent, string name, System.Type type )
	{
		if( parent==null ) return null;
		if( !Library.Is(name) ) return null;
		if( type==null ) return null;

		Transform transform = Func.Get( parent, name );
		if( transform!=null )
		{
			return transform.GetComponentInChildren(type);
		}

		return null;
	}
}