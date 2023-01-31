using System;
using UnityEngine;

public class FadeBehaviour : GraphicBehaviour
{
	public bool AutoStart = true;
	private float defaultAlpha = 0f;
	private bool isInitialize = false;

	private tagAi ai = new tagAi();
	private float fStart = 0f;
	private float fEnd = 0f;
	private float fLerp = 0f;
	public float fSpeed = 1f;
	private float fPow = 2f;
	public float delay = 0f;

	private tagCallback callback = null;
	private bool allowDestroy = false;

	protected override void Awake()
	{
		base.Awake();

		Initialize();
		if( !AutoStart )
		{
			Enable( false );
		}
	}

	void Start()
	{
		if( !IsAction() && AutoStart )
		{
			Alpha( 0f );
			In();
		}
	}

	void Update()
	{
		if( delay>0f )
		{
			delay -= Time.deltaTime;
			return;
		}

		fLerp += Time.deltaTime * fSpeed;

		Alpha( Mathf.Lerp( fStart, fEnd, Mathf.Sin(Mathf.Pow(fLerp, fPow)*Mathf.PI*0.5f) )*defaultAlpha );

		if( fLerp>=1f )
		{
			if( callback!=null && callback.Is() )
			{
				callback.Call();
			}

			if( GetAction()==ACTION.OUT && allowDestroy )
			{
				GameObject.Destroy(_GameObject());				
			}

			Alpha( fEnd*defaultAlpha );
			SetAction( Ai(), ACTION.NOTHING );
			Enable( false );
		}
	}

	//객체를 초기화 하기 위한 함수
	void Initialize()
	{
		defaultAlpha = Alpha();
		isInitialize = true;
	}

	//페이드-인을 설정하기 위한 함수
	public void In( float transit=-1f )
	{
		if( !isInitialize )
		{
			Initialize();	
		}
		
		if( transit==0f )
		{
			Alpha( defaultAlpha );
		}
		else
		{
			fStart = Library.Divide( Alpha(), defaultAlpha );
			fEnd = 1f;
			fLerp = 0f;

			if( transit>0f )
			{
				fSpeed = Library.Divide( 1f, transit );
			}

			SetAction( Ai(), ACTION.IN );
			Enable();
		}
	}

	// 페이드-아웃을 설정하기 위한 함수
	public void Out( float transit=-1f, float pow=-1f, bool allowDestroy=false  )
	{
		if( !isInitialize )
		{
			Initialize();	
		}

		if( transit==0f )
		{
			Alpha( 0f );
		}
		else
		{
			fStart = Library.Divide( Alpha(), defaultAlpha );
			fEnd = 0f;
			fLerp = 0f;

			if( transit>0f )
			{
				fSpeed = Library.Divide( 1f, transit );
			}

			if( pow>0f )
			{
				fPow = pow;
			}

			this.allowDestroy = allowDestroy;

			SetAction( Ai(), ACTION.OUT );
			Enable();
		}
	}

	// 페이드-아웃을 설정하기 위한 함수
	public void Out( float transit, bool allowDestroy  )
	{
		Out( transit, -1f, allowDestroy );
	}

	//활성화를 설정하기 위한 함수
	void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}

	//움직임 정보를 얻기 위한 함수
	tagAi Ai()
	{
		return ai;
	}

	//움직임을 설정하기 위한 함수
	void SetAction( ACTION nAction )
	{
		SetAction( Ai(), nAction );
	}

	//움직임을 설정하기 위한 함수
	void SetAction( tagAi ai, ACTION nAction )
	{
		if( ai==null ) return;
		ai.SetAction( nAction );
	}

	//움직임을 확인하기 위한 함수
	ACTION GetAction( tagAi ai )
	{
		if( ai==null ) return ACTION.NOTHING;
		return (ACTION)ai.Action;
	}

	//움직임을 얻기 위한 함수
	ACTION GetAction()
	{
		return GetAction(Ai());
	}

	//움직임을 확인하기 위한 함수
	public bool IsAction()
	{
		if( GetAction()!=ACTION.NOTHING )
		{
			return true;
		}

		return false;
	}

	//콜백을 설정하기 위한 함수
	public void SetCallback( Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( func==null ) return;
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( callback!=null )
		{
			callback.Set( func, wParam, lParam );
		}
		else
		{
			callback = new tagCallback( func, wParam, lParam );
		}
	}
}