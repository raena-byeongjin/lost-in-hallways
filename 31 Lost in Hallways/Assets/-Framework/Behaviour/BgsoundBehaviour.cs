using UnityEngine;

public class BgsoundBehaviour : SoundBehaviour
{
 	private tagAi ai = new tagAi();
	public float fVolume = 1f;

	private float fStart = 0f;
	private float fEnd = 0f;
	private float fLerp = 0f;
	private float fSpeed = 1f;
	private float fPow = 2f;

	protected override void Awake()
	{
		base.Awake();
		Enable( false );
	}

	void Update()
	{
		fLerp += Time.deltaTime * fSpeed;

		Volume( Mathf.Lerp( fStart, fEnd, Mathf.Sin(Mathf.Pow(fLerp, fPow)*Mathf.PI*0.5f) )*fVolume );

		if( fLerp>=1f )
		{
			if( fEnd<=0f )
			{
				GameObject.Destroy(gameObject);
			}

			Volume( fEnd*fVolume );

			SetAction( ACTION.NOTHING );
			Enable( false );
		}
	}

	//페이드-인을 설정하기 위한 함수
	public void In( float transit=1f )
	{
		fStart = Library.Divide( Volume(), fVolume );
		fEnd = 1f;
		fLerp = 0f;
		fSpeed = Library.Divide( 1f, transit );

		SetAction( ACTION.IN );
		Enable();
	}

	// 페이드-아웃을 설정하기 위한 함수
	public void Out( float transit=1f )
	{
		fStart = Library.Divide( Volume(), fVolume );
		fEnd = 0f;
		fLerp = 0f;
		fSpeed = Library.Divide( 1f, transit );

		SetAction( ACTION.OUT );
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

	protected override void OnDestroy()
	{
		ApplicationBehaviour.This.Bgsound.Release( this );
	}

	//현재 볼륨을 얻기 위한 함수
	float Volume()
	{
		return AudioSource().volume;
	}

	//볼륨을 설정하기 위한 함수
	void Volume( float value )
	{
		AudioSource().volume = value;
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

	//페이드 아웃 중인지 확인하기 위한 함수
	public bool IsOut()
	{
		if( GetAction()==ACTION.OUT )
		{
			return true;
		}

		return false;
	}
}