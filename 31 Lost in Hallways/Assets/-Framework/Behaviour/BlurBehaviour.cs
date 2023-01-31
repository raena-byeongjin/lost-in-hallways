using UnityEngine;

public class BlurBehaviour : ImageBehaviour
{
	private float fStart	= 0f;
	private float fEnd		= 0f;
	private float fLerp		= 0f;
	private float fSpeed	= 1f;
	private float fPow		= 2f;

	protected override void Awake()
	{
		base.Awake();
		Enable( false );
	}

	void Update()
	{
		fLerp += Time.deltaTime * fSpeed;

		Set( Mathf.Lerp( fStart, fEnd, Mathf.Sin(Mathf.Pow(fLerp, fPow)*Mathf.PI*0.5f) ) );

		if( fLerp>=1f )
		{
			Image().color = Library.Color( Image().color, fEnd );
			Enable( false );
		}
	}

	public void ON( float value, float transit=1f )
	{
		if( value==Alpha() ) return;

		fStart = Alpha();
		fEnd = Library.Limit( 0f, 1f, value );
		fLerp = 0f;
		fSpeed = Library.Divide( 1f, transit );

		Enable();
	}

	public void In( float transit=1f )
	{
		ON( 1f, transit );
	}

	public void Out( float transit=1f )
	{
		ON( 0f, transit );
	}

	//값을 설정하기 위한 함수
	public void Set( float value )
	{
		Image().color = Library.Color( Image().color, Library.Limit( 0f, 1f, value ) );
	}

	//활성화를 설정하기 위한 함수
	void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}

	//움직임을 확인하기 위한 함수
	public bool IsAction()
	{
		return isActiveAndEnabled;
	}

	//움직임을 확인하기 위한 함수
	public bool IsInAction()
	{
		if( IsAction() && fEnd>Alpha() )
		{
			return true;
		}

		return false;
	}

	//움직임을 확인하기 위한 함수
	public bool IsOutAction()
	{
		if( IsAction() && fEnd<Alpha() )
		{
			return true;
		}

		return false;
	}

	//완전히 아웃되었는지 확인하기 위한 함수
	public bool IsOutComplete()
	{
		if( !IsAction() && Alpha()<=0f )
		{
			return true;
		}

		return false;
	}
}