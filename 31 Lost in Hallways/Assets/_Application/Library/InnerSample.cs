﻿using UnityEngine;
using System.Collections;

//-----------------------------------------------------------------------------------------------------------------------------------
// 클리핑 뷰의 스크롤 아이템 정보를 처리하기 위한 클래스
//-----------------------------------------------------------------------------------------------------------------------------------
public class InnerSample : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
    public class _tagData
    {
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		public long					Sort				= (0);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		public Transform			transform			= (null);
		public RectTransform		rectTransform		= (null);
		public GameObject			gameObject			= (null);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		public Transform			panelTransform		= (null);
		public BoxCollider			panelCollider		= (null);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
//		public CComFadeAlpha		fadeAlpha			= (null);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
    };
    public _tagData		_data		= new _tagData();

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	protected CApp		app			= (null);
	protected CPlay		play		= (null);

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	void Awake()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		(_data.transform)		= (transform);
		(_data.rectTransform)	= (GetComponent(typeof(RectTransform)) as RectTransform);
		(_data.gameObject)		= (gameObject);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		(app)	= (CApp.This);
		(play)	= (CPlay.This);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Awake( _tagData data )
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		(this._data)	= (data);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		Awake();

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 인터페이스를 활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void ON()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		_GameObject().SetActive( true );

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//인터페이스를 비활성화 하기 위한 함수
	public void OFF()
	{
		_GameObject().SetActive( false );
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 게임 오브젝트를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public Transform Transform()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return (_data.transform);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 게임 오브젝트를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public GameObject _GameObject()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return (_data.gameObject);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 게임 오브젝트를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public RectTransform _RectTransform()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return (Data().rectTransform);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 너비를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public float Width()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( _RectTransform()!=(null) )
		{
			return (_RectTransform().sizeDelta.x);
		}

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( (_data.panelTransform)!=(null) )
		{
			return (_data.panelTransform.localScale.x);
		}

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return (0);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 높이를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public float Height()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( _RectTransform()!=(null) )
		{
			return (_RectTransform().sizeDelta.y);
		}

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( (_data.panelTransform)!=(null) )
		{
			return (_data.panelTransform.localScale.y);
		}

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return (0);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 픽킹을 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool Pick( objCamera camera )
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( (camera)==(null) ) return false;

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( Func.Pick( (_data.panelCollider), (camera) ) )
		{
			return true;
		}

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 활성화 되어 있는지 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsActive()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return (_GameObject().activeInHierarchy);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}
	
	//-------------------------------------------------------------------------------------------------------------------------------
	// 정렬 값을 설정하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void SetSort( long Sort )
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		(_data.Sort)		= (Sort);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 푸쉬되었는지 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public virtual bool IsPush()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( TouchInterface.Push().Is(this) )
		{
			return true;
		}

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public _tagData Data()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return (_data);

	    //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
}

//-----------------------------------------------------------------------------------------------------------------------------------
// -
//-----------------------------------------------------------------------------------------------------------------------------------