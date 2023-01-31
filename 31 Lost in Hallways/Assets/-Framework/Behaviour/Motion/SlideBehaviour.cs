using UnityEngine;

public class SlideBehaviour : RectTransformBehaviour
{
	public bool AutoStart = true;
	private Vector2 vDefault = new Vector2();
	private bool isInitialize = false;

	private tagAi ai = new tagAi();
	private Vector2 vStart = new Vector2();
	private Vector2 vEnd = new Vector2();
	private float fLerp = 0f;
	private float fSpeed = 1f;
	public float fPow = 2f;
	public float delay = 0f;

	public Vector2 vOut = new Vector2();

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
			Transform().anchoredPosition = vOut;
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

		Transform().anchoredPosition = Vector2.Lerp( vStart, vEnd, Mathf.Sin(Mathf.Pow(fLerp, fPow)*Mathf.PI*0.5f) );

		if( fLerp>=1f )
		{
			Transform().anchoredPosition = vEnd;
			SetAction( Ai(), ACTION.NOTHING );
			Enable( false );
		}
	}

	//객체를 초기화 하기 위한 함수
	void Initialize()
	{
		vDefault = Transform().anchoredPosition;
		isInitialize = true;
	}

	//페이드-인을 설정하기 위한 함수
	public void In( float transit=1f )
	{
		if( !isInitialize )
		{
			Initialize();	
		}

		vStart = Transform().anchoredPosition;
		vEnd = vDefault;
		fLerp = 0f;
		fSpeed = Library.Divide( 1f, transit );

		SetAction( Ai(), ACTION.IN );
		Enable();
	}

	// 페이드-아웃을 설정하기 위한 함수
	public void Out( float transit=1f, float fPow=0f )
	{
		vStart = Transform().anchoredPosition;
		vEnd = vOut;
		fLerp = 0f;
		fSpeed = Library.Divide( 1f, transit );

		if( fPow!=0f )
		{
			this.fPow = fPow;
		}

		SetAction( Ai(), ACTION.OUT );
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
}