using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class objChubby : MonoBehaviour
{
	protected Transform		m_transform		= null;
	protected GameObject	m_gameObject	= null;
	protected Image			m_image			= null;

	protected Vector3		Scale			= new Vector3();
	public float			Power			= 0.025f;
	public float			fSpeed			= 1f;
	public float			time			= 0;
	public float			delay			= 0;
	public bool				AutoStart		= true;
//	private bool			isEnable		= false;
	public ButtonBehaviour	buttonbehaviour	= null;
	private objDefaultScale	defaultScale	= null;

	void Awake()
	{
		if( m_transform==null )
		{
			m_transform			= transform;
			m_gameObject		= gameObject;
			m_image				= GetComponent(typeof(Image)) as Image;
			buttonbehaviour		= GetComponentInParent(typeof(ButtonBehaviour)) as ButtonBehaviour;
			defaultScale		= GetComponent(typeof(objDefaultScale)) as objDefaultScale;

			Scale = Transform().localScale;
		}
	}

	void Start()
	{
		if( !AutoStart )
		{
			OFF();
		}
		else
		if( AutoStart )
		{
			SetScale( new Vector3() );

			if( delay>0 )
			{
				if( Image()!=null )
				{
					Image().enabled	= false;
				}
			}
		}
	}

	void Update()
	{
		if( delay>0 )
		{
			delay -= Time.unscaledDeltaTime;
			return;
		}

		float fScale = Mathf.Cos(time) * Power;

		time += Time.unscaledDeltaTime*(fSpeed*20f);

		if( time>=Mathf.PI )
		{
			SetScale( GetDefaultScale() );

			time = Mathf.PI;

			if( enabled )
			{
				enabled = false;
			}
		}
		else
		if( enabled )	//버근가?? enabled가 false인데 어떻게 Update가 들어오지?
		{
			SetScale( GetDefaultScale() + new Vector3( fScale, fScale, 0 ) );
		}

		if( Image()!=null && !Image().enabled )
		{
			Image().enabled = true;
		}
	}

	//움직임을 활성화 하기 위한 함수
	public void ON( float delay=0f, bool compulsion=false )
	{
		if( delay>0f )
		{
			MessageQueue.ON( funcON, delay );
			return;
		}

//		isEnable = true;

		if( !IsAction() || compulsion )
		{
			Scale = Transform().localScale;

			time = 0;
			Enable( true );
		}
	}

	//움직임을 활성화 하기 위한 함수
	void OFF()
	{
		Enable( false );
	}

	//인터페이스 입력에 반응하기 위한 함수
	public void OnClick()
	{
		if( UserInterface.Is() && IsActive() )
		{
			if( buttonbehaviour!=null && buttonbehaviour.IsAllow() )
			{
				//외부 컴포넌트에 의해 지연이 발생할 수 있으므로
				//다음 프레임으로 넘겨서 처리함
				MessageQueue.Skip( funcON );
			}
			else
			if( buttonbehaviour==null )
			{
				ON();
			}
		}
		/*
#if UNITY_EDITOR || RUNTIME_LOG
		else
		if( buttonbehaviour==null )
		{
			foreach( Component component in ApplicationBehaviour.This.UserInterface.GetList() )
			{
				Debug.Log( component, ApplicationBehaviour.This.UserInterface );
			}
		}
#endif
		*/
	}

	//트랜스폼 객체를 얻기 위한 함수
	public Transform Transform()
	{
		if( m_transform==null )
		{
			Awake();
		}

		return m_transform;
	}

	//게임 오브젝트를 얻기 위한 함수
	public GameObject _GameObject()
	{
		if( m_transform==null )
		{
			Awake();
		}

		return m_gameObject;
	}

	//이미지 객체를 얻기 위한 함수
	public Image Image()
	{
		return m_image;
	}

	//활성화를 설정하기 위한 함수
	void Enable( bool enable )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}

	//인터페이스를 활성화 하기 위한 함수
	public void funcON( object wParam=null, object lParam=null )
	{
//		if( lParam==null || lParam.GetType()!=typeof(bool) ) return;	//(NULL)값을 허용함
		if( this==null ) return;

		bool compulsion = false;
		if( lParam!=null && lParam.GetType()==typeof(bool) )
		{
			compulsion = (bool)lParam;
		}

		ON( 0f, compulsion );
	}

	//원래 크기를 얻기 위한 함수
	public Vector3 GetDefaultScale()
	{
		if( defaultScale!=null )
		{
			return defaultScale.GetDefaultScale();
		}

		return Scale;
	}

	//크기를 설정하기 위한 함수
	void SetScale( Vector3 scale )
	{
		Transform().localScale = scale;
	}

	//활성화 되어 있는지 확인하기 위한 함수
	bool IsActive()
	{
		if( _GameObject()!=null )
		{
			//Chubby 컴포넌트는 enabled일 경우, 작동 준비상대이므로
			//오브젝트만 활성화되어 있어도 활성화 상태인 것으로 판단함
			return _GameObject().activeInHierarchy;
		}

		return false;
	}

	//활성화 되어 있는지 확인하기 위한 함수
	bool IsAction()
	{
		return enabled;
	}
}