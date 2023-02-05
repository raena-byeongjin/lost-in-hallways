using UnityEngine;

public class StickBehaviour : RectTransformBehaviour
{
	private Transform m_target = null;
	private Vector3 vAdd = new Vector3();

	protected virtual void OnDestroy()
	{
		ViewStick view = GetComponentInParent(typeof(ViewStick)) as ViewStick;
		if( view!=null )
		{
			view.Release(this);
		}
	}

	protected virtual void Start()
	{
		Refresh();
	}

	public void Refresh( CameraBehaviour camerabehaviour=null )
	{
//		if( camerabehaviour==null ) return; //(NULL)값을 허용함

		if( camerabehaviour==null )
		{
			camerabehaviour = Framework.Camera();
		}

		Transform().position = Library.WorldToCanvasPoint( camerabehaviour, Target().position+vAdd );
	}

	//타깃을 설정하기 위한 함수
	public void Set( Transform target, float height )
	{
		if( target==null ) return;

		m_target = target;
		vAdd = new Vector3( 0f, height, 0f );
	}

	//타깃을 얻기 위한 함수
	Transform Target()
	{
		return m_target;
	}
}