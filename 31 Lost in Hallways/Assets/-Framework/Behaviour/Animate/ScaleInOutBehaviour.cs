using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ScaleInOutBehaviour : DelayBehaviour
{
	public  float					fSpeed					= 1f;
	private Vector3					vDefault				= new Vector3();
	private tagAi					ai						= new tagAi();
	private float					fLerp					= 0f;
	public  float					fBounce					= 0.5f;
	private float					defaultDelay			= 0f;
	public  Vector2					random_range			= new Vector2();
	public  bool					AutoStart				= true;
	public  bool					defaultInactive			= false;
	public  bool					allowDestroy			= false;
	public  bool					allowComponentDestroy	= false;
	private bool					isInitialize			= false;
//	public  BJSoundInfo				soundIn					= new BJSoundInfo();
//	public  BJSoundInfo				soundOut				= new BJSoundInfo();
	private float					pre_delay				= 0f;
//	private BJAlign					m_align					= null;
	private float					fSince					= 0f;
	private DefaultScaleBehaviour	m_defaultScale			= null;
	private ChubbyBehaviour			m_chubby				= null;
	private tagCallback				callback				= null;
	public float					fPow					= 2f;
	public bool						UnscaledDeltaTime		= false;
	public Vector3					vScale					= new Vector3( 1f, 1f, 1f );

	protected override void Awake()
	{
		base.Awake();

		m_defaultScale	= GetComponent(typeof(DefaultScaleBehaviour)) as DefaultScaleBehaviour;
		m_chubby		= GetComponent(typeof(ChubbyBehaviour)) as ChubbyBehaviour;

		pre_delay		= delay;

		if( random_range!=new Vector2() )
		{
			delay = Library.Randomf(random_range);
		}

		defaultDelay = delay;
	}

	void Start()
	{
		if( !isInitialize )
		{
			//isInitialize가 가장 먼저 입력되어야
			//재귀 오류가 발생하지 않음
			isInitialize = true;

			SetDefaultScale( Transform().localScale );

			if( defaultInactive )
			{
				SetScale( new Vector3( 0, 0, Transform().localScale.z ) );
			}

			if( AutoStart && IsEnable() && !IsAction() )
			{
				if( fSpeed>0 )
				{
					In( Library.Divide(1f, fSpeed), delay, true );  
				}
				else
				{
					Out( Library.Divide(1f, Mathf.Abs(fSpeed)), delay, true );  
				}
			}

			if( !AutoStart && GetAction(Ai())==ACTION.NOTHING )
			{
				Enable( false );
			}
		}
	}

	public void ON( bool compulsion=false )
	{
		In( -1f, delay, compulsion );
	}

	void Update()
	{
		if( UnscaledDeltaTime && GetDeltaTime()>=0.1f )
		{
			return;
		}
		/*
		else
		{
			Debug.Log(GetDeltaTime());
		}
		*/

		Action( Ai() );

		if( delay>0 )
		{
			delay -= GetDeltaTime();
		}
	}

	void Action( tagAi ai )
	{
		if( ai==null ) return;

		switch( GetAction(ai) )
		{
			case ACTION.IN:
				if( delay<=0 )
				{
					ActionFadeIn(ai);
				}
				break;
					
			case ACTION.OUT:
				if( delay<=0 )
				{
					ActionFadeOut(ai);
				}
				break;
		}
	}
		
	void ActionFadeIn( tagAi ai )
	{
		if( ai==null ) return;

		fLerp += GetDeltaTime() * fSpeed;

		float lerp = fLerp;
		if( fPow!=1 )
		{
			lerp = Mathf.Min( 1, Mathf.Pow( fLerp, fPow ) );
		}

		if( float.IsNaN(lerp) )
		{
			lerp = 0;
		}

		Vector3 vScale = new Vector3();
		if( this.vScale.x!=0f )
		{
			vScale.x = lerp*GetDefaultScale().x+Mathf.Sin(lerp*Mathf.PI)*GetDefaultScale().x*fBounce*this.vScale.x;
		}
		else
		{
			vScale.x = GetDefaultScale().x;
		}

		if( this.vScale.y!=0f )
		{
			vScale.y = lerp*GetDefaultScale().y+Mathf.Sin(lerp*Mathf.PI)*GetDefaultScale().y*fBounce*this.vScale.y;
		}
		else
		{
			vScale.y = GetDefaultScale().y;
		}

		if( this.vScale.x!=0f )
		{
			vScale.z = lerp*GetDefaultScale().x+Mathf.Sin(lerp*Mathf.PI)*GetDefaultScale().x*fBounce*this.vScale.x;
		}
		else
		{
			vScale.z = GetDefaultScale().z;
		}

		SetScale( vScale );

		if( fLerp>=1f )
		{
			SetAction( ai, ACTION.NOTHING );
			SetScale( GetDefaultScale() );

			OFF();

			if( allowComponentDestroy )
			{
				Component.Destroy( this );
			}

			if( callback!=null && callback.Is() )
			{
				//콜백을 실행함
				callback.Call();
			}
		}
	}

	void ActionFadeOut( tagAi ai )
	{
		if( ai==null ) return;

		fLerp += GetDeltaTime() * fSpeed;
		float lerp = fLerp;
		if( fPow!=1 )
		{
			lerp = Mathf.Min( 1, Mathf.Pow( fLerp, fPow ) );
		}

		if( float.IsNaN(lerp) )
		{
			lerp = 0;
		}

		SetScale( new Vector3( (1-lerp)*GetDefaultScale().x+Mathf.Sin(lerp*Mathf.PI)*GetDefaultScale().x*fBounce, (1-lerp)*GetDefaultScale().y+Mathf.Sin(lerp*Mathf.PI)*GetDefaultScale().y*fBounce, (1-lerp)*GetDefaultScale().z+Mathf.Sin(lerp*Mathf.PI)*GetDefaultScale().z*fBounce ) );
		/*
		Vector3 vScale = Vector3.Lerp( GetDefaultScale(), new Vector3(), Mathf.Sin(Mathf.Pow(fLerp, 2)*Mathf.PI*0.5f) );
		SetScale( vScale );
		*/

		if( fLerp>=1 )
		{
			SetScale( new Vector3() );
			SetAction( ai, ACTION.NOTHING );

			Library.Inactive( _GameObject() );
			OFF();

			if( allowDestroy )
			{
				GameObject.Destroy( _GameObject() );
			}
			else
			if( allowComponentDestroy )
			{
				Component.Destroy( this );
			}

			if( callback!=null && callback.Is() )
			{
				//콜백을 실행함
				callback.Call();
			}
		}
	}

	public void In( float time=-1f, float delay=0, bool compulsion=false )
	{
		if( Transform()==null || !isInitialize )
		{
			Awake();
			Start();
		}

		if( !IsEnable() || !_GameObject().activeSelf || compulsion )
		{
			/*
			if( m_align!=null && m_align.enabled )
			{
				m_align.Align();
				m_align.enabled = false;
			}
			*/

			if( time>0 )
			{
				fSpeed = Library.Divide(1f, time);
			}

			if( IsAction() && GetAction()==ACTION.OUT )
			{
				fLerp = 1f-fLerp;
			}
			else
			{
				fLerp = 0;
			}

			SetAction( ACTION.IN );

			/*
			if( soundIn!=null && soundIn.Is() )
			{
				BJSound.ON( soundIn, delay );
			}
			*/

			ON();

			//스케일을 편집하는 다른 객체에서 Default 스케일을 필요로 할 수 있기 때문에
			//스케일을 초기화 하는 작업은 반드시 ON() 다음에 처리해야 함
			SetScale( new Vector3( 0, 0, Transform().localScale.z ) );

			if( delay>0 )
			{
				SetDelay( delay );
			}
		}
	}

	public void In( bool compulsion )
	{
		In( -1f, 0f, compulsion );
	}

	public void Out( float time=-1, float delay=0f, bool compulsion=false, bool allowDestroy=false )
	{
		if( compulsion || ( ( !IsEnable() || GetAction()!=ACTION.OUT ) && ( _GameObject().activeSelf || ( _GameObject().activeSelf && ( Transform().localScale.x!=0 || Transform().localScale.y!=0 ) ) ) ) )
		{
			if( time>=0 )
			{
				fSpeed	= Library.Divide( 1f, time );
			}

			/*
			if( IsAction() && GetAction()==BJ_OBJECT_ACTION.IN )
			{
				fLerp = 1-fLerp;
			}
			else
			{
				fLerp   = 0;
			}
			*/
			fLerp = 1 - Library.Divide( Transform().localScale.x, GetDefaultScale().x );

			SetAction( ACTION.OUT );

			/*
			if( soundOut!=null && soundOut.Is() )
			{
				BJSound.ON( soundOut, delay );
			}
			*/

			this.allowDestroy = allowDestroy;
				
			ON();

			if( delay>0 )
			{
				SetDelay( delay );
			}
		}
	}

	public void Out( bool compulsion, bool allowDestroy )
	{
		Out( -1f, 0f, compulsion, allowDestroy );
	}

	public void Out( bool compulsion )
	{
		Out( -1f, 0f, compulsion );
	}

	public void funcOut( object wParam=null, object lParam=null )
	{
		if( _GameObject()!=null && _GameObject().activeSelf )
		{
			Out();
		}
	}

	//객체가 활성화되어 있는지 확인하기 위한 함수
	public bool IsActive()
	{
		return _GameObject().activeSelf;
	}

	//컴포넌트가 활성화되어 있는지 확인하기 위한 함수
	public bool IsEnable()
	{
		return enabled;
	}
		
	public void SetAction( ACTION Action )
	{
		SetAction( Ai(), Action );
	}
		
	public void SetAction( tagAi ai, ACTION Action )
	{
		if( ai==null ) return;

		ai.SetAction( Action );
	}

	public tagAi Ai()
	{
		return ai;
	}

	//움직임을 얻기 위한 함수
	public ACTION GetAction( tagAi ai )
	{
		if( ai==null ) return ACTION.NOTHING;			
		return (ACTION)ai.Action;
	}

	//움직임을 얻기 위한 함수
	public ACTION GetAction()
	{
		return GetAction(Ai());
	}
		
	//인터페이스를 활성화 하기 위한 함수
	void ON()
	{
		if( Transform()==null )
		{
			Awake();
			Start();
		}

		if( !_GameObject().activeSelf )
		{
			Library.Active( _GameObject() );
		}

		fSince = Time.time;

		Enable( true );
	} 

	//인터페이스를 비활성화 하기 위한 함수
	public void OFF()
	{
		if( IsEnable() )
		{
			Enable( false );
		}
	}

	//활성화를 설정하기 위한 함수
	void Enable( bool enable )
	{
		if( enabled!=enable )
		{
			enabled     = enable;
		}
	} 

	//지연 시간을 설정하기 위한 함수
	public void SetDelay( float delay )
	{
		this.delay	= delay;
	}

	//동작이 종료될때 오브젝트를 파기할지 설정하기 위한 함수
	public void SetDestroy( bool option )
	{
		allowDestroy = option;
	}

	public float GetPreDelay()
	{
		if( Transform()==null || delay>0 )
		{
			//아직 초기화되지 못했기 때문에, delay의 값을 반환함
			return delay;
		}

		return pre_delay;
	}
		
	//기본 크기를 설정하기 위한 함수
	public void SetDefaultScale( Vector3 vScale )
	{
		vDefault = vScale;
	}

	public float GetDefaultDelay()
	{
		return defaultDelay;
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

	public float Since()
	{
		return Time.time - fSince;
	}

	//크기를 설정하기 위한 함수
	void SetScale( Vector3 vScale )
	{
		Transform().localScale = vScale;
	}

	//원래 크기를 얻기 위한 함수
	Vector3 GetDefaultScale()
	{
		if( m_defaultScale!=null )
		{
			return m_defaultScale.GetDefaultScale();
		}
		else
		if( m_chubby!=null )
		{
			return m_chubby.GetDefaultScale();
		}

		return vDefault;
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

	float GetDeltaTime()
	{
		if( UnscaledDeltaTime )
		{
			return Time.unscaledDeltaTime;
		}

		return Time.deltaTime;
	}
}