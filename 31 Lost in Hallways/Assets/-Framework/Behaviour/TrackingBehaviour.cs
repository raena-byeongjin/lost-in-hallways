using UnityEngine;

public class TrackingBehaviour : TransformBehaviour
{
	public Transform	m_target	= null;
	public bool			LookAt		= false;
	public Vector3		vAdded		= new Vector3();
	public float		fSpeed		= 1f;

	protected override void Awake()
	{
		base.Awake();
		Enable( false );
	}

	void Update()
	{
		if( Get()!=null && Transform().position!=Get().position+vAdded )
		{
			UpdateCommit();
		}
	}

	//타깃을 설정하기 위한 함수
	public void ON( Transform target )
	{
//		if( target==null ) return; //(NULL)값을 허용함

		m_target = target;

		if( Get()!=null )
		{
			Enable( true );
		}
		else
		{
			Enable( false );
		}
	}

	// 활성화를 설정하기 위한 함수
	void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}

	void UpdateCommit()
	{
		if( LookAt && Transform().position!=Get().position )
		{
			Transform().LookAt( Get().position, Transform().up );
		}

		Transform().position = Vector3.Lerp( Transform().position, Get().position, Time.deltaTime*fSpeed );
	}

	//타깃 객체를 얻기 위한 함수
	public Transform Get()
	{
		return m_target;
	}

	//활성화되어 있는지 확인하기 위한 함수
	public bool IsActive()
	{
		return isActiveAndEnabled;
	}

	//속도를 설정하기 위한 함수
	public void SetSpeed( float speed )
	{
		fSpeed = speed;
	}

	//객체를 초기화 하기 위한 함수
	public void Reset()
	{
		ON(null);
	}
}