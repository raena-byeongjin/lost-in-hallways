using System.Collections.Generic;
using UnityEngine;

//인터페이스 정보를 처리하기 위한 클래스
public class objPanel : RectTransformBehaviour
{
	[System.Serializable]
	public class tagData
	{
 		public ViewPanel			reciveComponent		= null;
		public List<objControl>		Controls			= new List<objControl>();
		public objControl			Close				= null;

		public objForeBack			foreback			= null;

		public bool					allowSound			= true;
	}
	public tagData data = new tagData();

	protected ApplicationBehaviour app = null;
	protected PlayBehaviour play = null;

	protected override void Awake()
	{
		base.Awake();

		app		= ApplicationBehaviour.This;
		play	= PlayBehaviour.This;

		data.foreback = GetComponent(typeof(objForeBack)) as objForeBack;

		if( m_transform!=null )
		{
			m_transform.anchoredPosition = new Vector2();
		}
	}

	//인터페이스를 활성화 하기 위한 함수
	public bool _ON()
	{
		app.Panel.Fore( this );

		if( !_GameObject().activeSelf )
		{
			_GameObject().SetActive( true );
			return true;
		}

		return false;
	}

	//인터페이스를 비활성화 하기 위한 함수
	public virtual void OFF()
	{
		if( _GameObject()!=null )
		{
			_GameObject().SetActive( false );
		}
	}

	//활성화 되어 있는지 확인하기 위한 함수
	public bool IsPanel()
	{
		if( _GameObject()!=null )
		{
			return _GameObject().activeInHierarchy;
		}

		return false;
	}

	//패널 컴포넌트를 설정하기 위한 함수
	public void Set( ViewPanel panel )
	{
		if( panel==null ) return;
		data.reciveComponent = panel;
	}

	//컴포넌트를 얻기 위한 함수
	public ViewPanel ReciveComponent()
	{
		return data.reciveComponent;
	}

	//컴포넌트를 얻기 위한 함수
	public void Register( objControl control )
	{
 		if( control==null ) return;

		if( !data.Controls.Contains(control) )
		{
			data.Controls.Add( control );
		}
	}

	protected virtual void OnDestroy()
	{
		if( app!=null && app.Panel.Is(this) )
		{
			app.Panel.Release( this );
		}
	}

	public objForeBack ForeBack()
	{
		return data.foreback;
	}

	public bool IsForeBack()
	{
 		if( ForeBack()!=null && ForeBack().IsActive() )
		{
			return true;
		}

		return false;
	}

	//Depth를 얻기 위한 함수
	public float Depth()
	{
 		if( ReciveComponent()!=null )
		{
			return ReciveComponent().Depth();
		}

		return 0f;
	}

	//닫기 버튼 객체를 얻기 위한 함수
	public objControl CloseButton()
	{
		return data.Close;
	}

	//사운드가 허용되어 있는지 확인하기 위한 함수
	public bool IsAllowSound()
	{
 		return data.allowSound;
	}
}