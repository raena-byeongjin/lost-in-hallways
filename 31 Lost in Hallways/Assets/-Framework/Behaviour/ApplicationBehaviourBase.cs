using System.Collections;
using System;
using UnityEngine;

//어플리케이션 정보를 처리하기 위한 클래스
public abstract class ApplicationBehaviourBase : FrameworkBehaviour
{
	protected virtual void Start()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	//어플리케이션이 시작되었는지 확인하기 위한 함수
	public static bool IsStartup()
	{
		if( This!=null )
		{
			return true;
		}

		return false;
	}

	//코루틴을 실행하기 위한 함수
	public void Coroutine( YieldInstruction yieldInstruction, Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( yieldInstruction==null ) return;
//		if( func==null ) return;		//(NULL)값을 허용함
//		if( wParam==null ) return;		//(NULL)값을 허용함
//		if( lParam==null ) return;		//(NULL)값을 허용함

		StartCoroutine( Coroutine_( yieldInstruction, func, wParam, lParam ) );
	}

	//코루틴을 실행하기 위한 함수
	IEnumerator Coroutine_( YieldInstruction yieldInstruction, Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( yieldInstruction==null ) yield break;
//		if( func==null ) return;		//(NULL)값을 허용함
//		if( wParam==null ) return;		//(NULL)값을 허용함
//		if( lParam==null ) return;		//(NULL)값을 허용함

		yield return yieldInstruction;

		if( func!=null )
		{
			func( wParam, lParam );
		}
	}

	public static ApplicationBehaviour This = null;

	[NonSerialized] public CBrowser Browser = null;
	[NonSerialized] public CAssetLoader AssetLoader = null;
	[NonSerialized] public CBgsound Bgsound = null;
	[NonSerialized] public CInterface Interface = null;

	[NonSerialized] public CSceneStatic SceneStatic = null;
	[NonSerialized] public CModelStatic ModelStatic = null;
	[NonSerialized] public CMotionStatic MotionStatic = null;
	[NonSerialized] public CSoundStatic SoundStatic = null;
	[NonSerialized] public CBgsoundStatic BgsoundStatic = null;
	[NonSerialized] public CEffectStatic EffectStatic = null;
	
	protected override void Awake()
	{
		This = this as ApplicationBehaviour;
		base.Awake();

		Browser = GetComponentInChildren(typeof(CBrowser)) as CBrowser;
		AssetLoader = GetComponentInChildren(typeof(CAssetLoader)) as CAssetLoader;
		Bgsound = GetComponentInChildren(typeof(CBgsound)) as CBgsound;
		Interface = GetComponentInChildren(typeof(CInterface)) as CInterface;

		SceneStatic = GetComponentInChildren(typeof(CSceneStatic)) as CSceneStatic;
		ModelStatic = GetComponentInChildren(typeof(CModelStatic)) as CModelStatic;
		MotionStatic = GetComponentInChildren(typeof(CMotionStatic)) as CMotionStatic;
		SoundStatic = GetComponentInChildren(typeof(CSoundStatic)) as CSoundStatic;
		BgsoundStatic = GetComponentInChildren(typeof(CBgsoundStatic)) as CBgsoundStatic;
		EffectStatic = GetComponentInChildren(typeof(CEffectStatic)) as CEffectStatic;
	}
}