using System;
using UnityEngine;

public class MotionBehaviour : DelayBehaviour
{
	private float fLerp = 0f;
	public float fSpeed = 1f;
	public float fPow = 2f;

	private END_ACTION nEndAction = END_ACTION.NOTHING;
	private tagCallback callback = null;

	protected override void Awake()
	{
		base.Awake();
		Enable( false );
	}

	void Update()
	{
		if( delay>0f )
		{
			delay -= Time.deltaTime;
			return;
		}

		fLerp += Time.deltaTime * fSpeed;
		Refresh( Mathf.Min( 1f, fLerp ) );

		if( fLerp>=1f )
		{
			switch( nEndAction )
			{
				case END_ACTION.DESTROY:
					GameObject.Destroy(_GameObject());
					break;

				case END_ACTION.COMPONENT_DESTROY:
					Component.Destroy(this);
					break;
			}

			if( callback!=null )
			{
				callback.Call();
			}

			Enable( false );
		}
	}

	protected virtual void Refresh( float value )
	{
	}

	//인터페이스를 활성화 하기 위한 함수
	public void ON( float transit=-1f, float pow=-1f, float delay=-1f, END_ACTION nEndAction=END_ACTION.NOTHING )
	{
		fLerp = 0f;

		if( transit>0f )
		{
			fSpeed = Library.Divide( 1f, transit );
		}

		if( pow>0f )
		{
			fPow = pow;
		}

		if( delay>0f )
		{
			this.delay = delay;
		}

		this.nEndAction = nEndAction;

		Enable();
	}

	//활성화를 설정하기 위한 함수
	void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}

	//Callback을 설정하기 위한 함수
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

	//움직임을 확인하기 위한 함수
	public bool IsAction()
	{
		return isActiveAndEnabled;
	}
}