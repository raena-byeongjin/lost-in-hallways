using UnityEngine;

public class TranslateBehaviour : TransformBehaviour
{
	private RectTransform m_rectTransform = null;

	private Vector3 vStart = new Vector3();
	private Vector3 vEnd = new Vector3();
	private float fLerp = 0f;
	public float fSpeed = 1f;
	public float fPow = 2f;
	private float delay = 0f;

	public TRANSFORM_SPACE nSpace = TRANSFORM_SPACE.NOTHING;

	protected override void Awake()
	{
		base.Awake();

		m_rectTransform = Transform() as RectTransform;
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

		if( fLerp>=1f )
		{
			SetPosition( vEnd );
			Enable( false );
		}
		else
		{
			SetPosition( Vector3.Lerp( vStart, vEnd, Mathf.Sin(Mathf.Pow(fLerp, fPow)*Mathf.PI*0.5f)) );
		}
	}

	public void ON( Vector3 position, float transit=-1f, float pow=-1f )
	{
		if( transit==0f )
		{
			SetPosition( position );
		}
		else
		{
			vStart = GetPosition();
			vEnd = position;
			fLerp = 0f;

			if( transit>0f )
			{
				fSpeed = Library.Divide( 1f, transit );
			}

			if( pow>0f )
			{
				fPow = pow;
			}

			Enable();
		}
	}

	//활성화를 설정하기 위한 함수
	void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}

	//위치를 얻기 위한 함수
	RectTransform _RectTransform()
	{
		return m_rectTransform;
	}

	//위치를 얻기 위한 함수
	void SetPosition( Vector3 position )
	{
		switch( nSpace )
		{
			case TRANSFORM_SPACE.ANCHORED:
				_RectTransform().anchoredPosition = position;
				break;

			case TRANSFORM_SPACE.LOCAL:
				Transform().localPosition = position;
				break;

			default:
				Transform().position = position;
				break;
		}
	}

	//위치를 얻기 위한 함수
	Vector3 GetPosition()
	{
		switch( nSpace )
		{
			case TRANSFORM_SPACE.ANCHORED:
				return _RectTransform().anchoredPosition;

			case TRANSFORM_SPACE.LOCAL:
				return Transform().localPosition;
		}

		return Transform().position;
	}

	//움직임을 확인하기 위한 함수
	public bool IsAction()
	{
		return isActiveAndEnabled;
	}

	//지연 시간을 설정하기 위한 함수
	public void SetDelay( float delay )
	{
		this.delay = delay;
	}

	//지연 시간을 얻기 위한 함수
	public float GetDelay()
	{
		return delay;
	}

	//목표 위치를 얻기 위한 함수
	public Vector3 GetTargetPosition()
	{
		return vEnd;
	}

	public void funcON( object wParam=null, object lParam=null )
	{
//		if( wParam==null || wParam.GetType()!=typeof(Vector2) ) return;	//(NULL)값을 허용함
//		if( lParam==null || lParam.GetType()!=typeof(float) ) return;	//(NULL)값을 허용함

		if( wParam!=null && wParam.GetType()==typeof(Vector3) )
		{
			Vector3 position = (Vector3)wParam;

			if( lParam!=null && lParam.GetType()==typeof(float) )
			{
				ON( position, (float)lParam );
			}
			else
			{
				ON( position );
			}
		}
		else
		if( wParam!=null && wParam.GetType()==typeof(Vector2) )
		{
			Vector2 position = (Vector2)wParam;

			if( lParam!=null && lParam.GetType()==typeof(float) )
			{
				ON( position, (float)lParam );
			}
			else
			{
				ON( position );
			}
		}
	}
}