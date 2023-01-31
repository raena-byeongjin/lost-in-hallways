using UnityEngine.UI;
using UnityEngine;

//캔버스 정보를 처리하기 위한 클래스
public class CanvasBehaviour : objCanvas
{
	protected override void Awake()
	{
		base.Awake();

		CanvasScaler canvasScaler = GetComponent(typeof(CanvasScaler)) as CanvasScaler;
		if( canvasScaler!=null )
		{
			if( Library.Divide(Screen.width, Screen.height)<0.5f )
			{
				canvasScaler.matchWidthOrHeight = 0f;
			}
			else
			{
				canvasScaler.matchWidthOrHeight = 1f;
			}
		}
	}

	void Start()
	{
		if( allowInactive && Panels.Count<=0 )
		{
			Library.Inactive( _GameObject() );
		}
	}

	public void In( Transform transform )
	{
		if( transform==null ) return;

		transform.SetParent( Transform() );
		Active();
	}
}