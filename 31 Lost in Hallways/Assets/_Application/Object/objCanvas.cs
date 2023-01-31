using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

//캔버스 정보를 처리하기 위한 클래스
public class objCanvas : RectTransformBehaviour
{
	[System.Serializable]
	public class tagData
	{
		public RectTransform		rectTransform		= null;
		public Canvas				canvas				= null;
		public CameraBehaviour		camera				= null;
		public GraphicRaycaster		graphicRaycaster	= null;

 		public objForeBack			ForeBack			= null;
	}
	public tagData data = new tagData();

	protected List<objPanel> Panels = new List<objPanel>();
	public bool allowInactive = true;

	protected ApplicationBehaviour app = null;
	protected PlayBehaviour play = null;

	protected override void Awake()
	{
		base.Awake();

		app = ApplicationBehaviour.This;
		play = PlayBehaviour.This;

		data.rectTransform		= GetComponent(typeof(RectTransform)) as RectTransform;
		data.canvas				= GetComponent(typeof(Canvas)) as Canvas;
		data.camera				= GetComponentInChildren(typeof(CameraBehaviour)) as CameraBehaviour;
		data.graphicRaycaster	= GetComponentInChildren(typeof(GraphicRaycaster)) as GraphicRaycaster;
		data.ForeBack			= GetComponentInChildren(typeof(objForeBack)) as objForeBack;

		if( Camera()==null )
		{
			Camera camera = GetComponentInChildren(typeof(Camera)) as Camera;
			if( camera!=null )
			{
				data.camera = camera.gameObject.AddComponent(typeof(CameraBehaviour)) as CameraBehaviour;
			}			
		}

		if( Camera()!=null && Canvas().worldCamera==null )
		{
			Canvas().worldCamera = Camera().Get();
		}

		if( ForeBack()!=null )
		{
			ForeBack().data.canvas = this;
		}

		if( app!=null )
		{
			app.Canvas.Register( this );
		}

		if( Library.Divide(Screen.width, Screen.height)>1.8f )
		{
			CanvasScaler canvasScaler = (GetComponent(typeof(CanvasScaler)) as CanvasScaler);
			if( canvasScaler!=null )
			{
				canvasScaler.matchWidthOrHeight = 1f;
			}
		}
	}

	//트랜스폼 객체를 얻기 위한 함수
	public RectTransform _RectTransform()
	{
		return data.rectTransform;
	}

	//카메라를 설정하기 위한 함수
	public void SetCamera( Camera camera )
	{
		if( camera==null ) return;

		Canvas().worldCamera	= camera;
		Canvas().planeDistance	= camera.farClipPlane - camera.nearClipPlane;
	}

	//캔버스를 얻기 위한 함수
	public Canvas Canvas()
	{
		return data.canvas;
	}

	//카메라 객체를 얻기 위한 함수
	public CameraBehaviour Camera()
	{
		return data.camera;
	}

	//그래픽 레이캐스터 객체를 얻기 위한 함수
	public GraphicRaycaster GraphicRaycaster()
	{
		return data.graphicRaycaster;
	}

	//배경 안막 객체를 얻기 위한 함수
	public objForeBack ForeBack()
	{
		return data.ForeBack;
	}

	void OnDestroy()
	{
		if( app!=null )
		{
			app.Canvas.Release( this );
		}
	}

	//캔버스를 이동하기 위한 함수
	public void Change( ViewPanel panel )
	{
		if( panel==null ) return;

		tagTransform preTransform = new tagTransform();

		if( panel.Panel()!=null )
		{
			preTransform.Set( panel.Panel().Transform() );
			panel.Panel().Transform().SetParent( Transform() );
			preTransform.Get( panel.Panel().Transform() );
		}

		panel.Data().canvas = this as CanvasBehaviour;
	}

	//Depth를 얻기 위한 함수
	public float Depth()
	{
		if( Camera()!=null )
		{
			return Camera().Get().depth;
		}

		return 0f;
	}

	//인터페이스가 활성화 되어 있는지 확인하기 위한 함수
	public bool IsActive()
	{
		if( _GameObject()!=null )
		{
			return _GameObject().activeSelf;
		}

		return false;
	}

	public void Active()
	{
 		if( !IsActive() )
		{
			_GameObject().SetActive( true );
		}
	}

	public void Inactive()
	{
		if( IsActive() )
		{
			_GameObject().SetActive( false );
		}
	}

	//인터페이스를 등록하기 위한 함수
	public void Register( objPanel panel )
	{
		if( panel==null ) return;

		if( !Panels.Contains(panel) )
		{
			Panels.Add( panel );
			Library.Active( _GameObject() );
		}
	}

	//인터페이스를 해제하기 위한 함수
	public void Release( objPanel panel )
	{
		if( panel==null ) return;

		Panels.Remove( panel );

		if( allowInactive && Panels.Count<=0 )
		{
			MessageQueue.Skip( Library.funcInactive, _GameObject() );
		}
	}

	//가로 너비를 얻기 위한 함수
	public override float Width()
	{
		return Transform().sizeDelta.x;
	}

	//세로 높이를 얻기 위한 함수
	public override float Height()
	{
		return Transform().sizeDelta.y;
	}
}