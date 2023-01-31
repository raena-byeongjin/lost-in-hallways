using UnityEngine;

public class InterfaceBehaviour : objPanel
{
	private float fSince = 0f;

	protected override void Awake()
	{
		base.Awake();

		if( ApplicationBehaviour.IsStartup() )
		{
			ApplicationBehaviour.This.Interface.Register(this);
		}

		fSince = Time.time;
	}

	protected override void OnDestroy()
	{
		ApplicationBehaviour.This.Interface.Release(this);
		base.OnDestroy();
	}

	//인터페이스를 비활성화 하기 위한 함수
	public override void OFF()
	{
		GameObject.Destroy( _GameObject() );
	}

	//취소 입력에 반응하기 위한 함수
	public virtual bool ONCANCEL( CANCEL Cancel )
	{
		return false;
	}

	//리턴 입력에 반응하기 위한 함수
	public virtual bool ONRETURN()
	{
		return false;
	}

	//리턴 입력에 반응하기 위한 함수
	public virtual bool ONSKIP()
	{
		return false;
	}

	public float Since()
	{
		return Time.time-fSince;
	}
}