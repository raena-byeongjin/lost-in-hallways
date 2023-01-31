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

	public void Refresh()
	{
		Transform().position = Library.WorldToCanvasPoint(Target().position+vAdd);
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