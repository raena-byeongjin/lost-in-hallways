using System;
using UnityEngine;

public class CameraBehaviour : objCamera
{
	private Camera m_camera = null;
	private Transform m_rawcamera_transform = null;
//	private GameObject m_rawcamera_gameObject = null;
	private TranslateBehaviour m_translate = null;
//	private VibrateBehaviour m_vibrate = null;
//	private FieldOfViewBehaviour m_fieldOfView = null;
//	private ZoomBehaviour m_zoom = null;

	private tagCameraTransform m_pre_transform = new tagCameraTransform();
	public Action<CameraBehaviour> onChange = null;

	protected override void Awake()
	{
		base.Awake();

		m_camera = GetComponentInChildren(typeof(Camera)) as Camera;
		m_translate = GetComponentInChildren(typeof(TranslateBehaviour)) as TranslateBehaviour;
//		m_vibrate = GetComponentInChildren(typeof(VibrateBehaviour)) as VibrateBehaviour;
//		m_fieldOfView = GetComponentInChildren(typeof(FieldOfViewBehaviour)) as FieldOfViewBehaviour;
//		m_zoom = GetComponentInChildren(typeof(ZoomBehaviour)) as ZoomBehaviour;

		if( Get()!=null )
		{
			m_rawcamera_transform	= Get().transform;
//			m_rawcamera_gameObject	= Get().gameObject;

			if( PlayBehaviour.This!=null && Get().tag=="MainCamera" )
			{
				PlayBehaviour.This.mainCamera = this;
			}
		}
	}

	protected virtual void Start()
	{
		m_pre_transform.Set( Get(), Transform() );
	}

	protected virtual void LateUpdate()
	{
		if( !m_pre_transform.Contains( Get(), Transform() ) )
		{
			if( onChange!=null )
			{
				onChange(this);
			}
		}

		m_pre_transform.Set( Get(), Transform() );
	}

	//카메라 객체를 얻기 위한 함수
	public Camera Get()
	{
		return m_camera;
	}

	//카메라 트랜스폼을 얻기 위한 함수
	public Transform GetRawCameraTransform()
	{
		return m_rawcamera_transform;
	}

	//활성화를 설정하기 위한 함수
	protected void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}

	//위치를 얻기 위한 함수
	public override Vector3 GetPosition()
	{
		return Transform().position;
	}

	//이동 객체를 얻기 위한 함수
	public TranslateBehaviour Translate()
	{
		if( m_translate==null )
		{
			m_translate = GetComponentInChildren(typeof(TranslateBehaviour)) as TranslateBehaviour;
			if( m_translate==null )
			{
				m_translate = _GameObject().AddComponent(typeof(TranslateBehaviour)) as TranslateBehaviour;
			}
		}

		return m_translate;
	}

	/*
	//진동 객체를 얻기 위한 함수
	public VibrateBehaviour Vibrate()
	{
		if( m_vibrate==null )
		{
			m_vibrate = GetComponentInChildren(typeof(VibrateBehaviour)) as VibrateBehaviour;
			if( m_vibrate==null )
			{
				m_vibrate = m_rawcamera_gameObject.AddComponent(typeof(VibrateBehaviour)) as VibrateBehaviour;
			}
		}

		return m_vibrate;
	}

	//FieldOfView 객체를 얻기 위한 함수
	public FieldOfViewBehaviour FieldOfView()
	{
		if( m_fieldOfView==null )
		{
			m_fieldOfView = GetComponentInChildren(typeof(FieldOfViewBehaviour)) as FieldOfViewBehaviour;
			if( m_fieldOfView==null )
			{
				m_fieldOfView = _GameObject().AddComponent(typeof(FieldOfViewBehaviour)) as FieldOfViewBehaviour;
			}
		}

		return m_fieldOfView;
	}

	//FieldOfView 객체를 얻기 위한 함수
	public ZoomBehaviour Zoom()
	{
		if( m_zoom==null )
		{
			m_zoom = GetComponentInChildren(typeof(ZoomBehaviour)) as ZoomBehaviour;
			if( m_zoom==null )
			{
				m_zoom = _GameObject().AddComponent(typeof(ZoomBehaviour)) as ZoomBehaviour;
			}
		}

		return m_zoom;
	}
	*/

	//Ray를 업데이트 하기 위한 함수
	public static void Ray( Vector2 point )
	{
	}

	/*
	//Ray를 업데이트 하기 위한 함수
	public Ray GetRay( Vector2 point )
	{
		return Get().ScreenPointToRay(point);
	}

	//시야 거리를 얻기 위한 함수
	public float Distance()
	{
		return Get().farClipPlane - Get().nearClipPlane;
	}

	//움직임을 확인하기 위한 함수
	public bool IsAction()
	{
		if( FieldOfView()!=null && FieldOfView().IsAction() )
		{
			return true;
		}

		if( Translate()!=null && Translate().IsAction() )
		{
			return true;
		}

		return false;
	}
	*/
}