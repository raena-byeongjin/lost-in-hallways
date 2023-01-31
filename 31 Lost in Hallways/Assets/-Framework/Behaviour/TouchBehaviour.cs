using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TouchBehaviour : FrameworkBehaviour
{
	private static List<TouchBehaviour> Behaviours = new List<TouchBehaviour>();

	protected override void Awake()
	{
		base.Awake();

		if( !GetList().Contains(this) )
		{
			GetList().Add(this);
		}
	}

	protected virtual void OnDestroy()
	{
		GetList().Remove(this);
	}

	//리스트를 얻기 위한 함수
	public static List<TouchBehaviour> GetList()
	{
		return Behaviours;
	}

	//터치를 눌렀을 때 반응하기 위한 함수
	public virtual bool ONTOUCHDOWN( Vector2 point, tagTouch touch )
	{
		if( touch==null ) return false;
		return false;
	}

	//드래그 입력에 반응하기 위한 함수
	public virtual bool ONDRAG( Vector2 point, tagTouch touch )
	{
		if( touch==null ) return false;
		return false;
	}

	//터치를 눌렀을 때 반응하기 위한 함수
	public virtual bool ONTOUCHUP( Vector2 point, tagTouch touch )
	{
		if( touch==null ) return false;
		return false;
	}

	//마우스 휠에 반응하기 위한 함수
	public virtual bool ONWHEEL( float fWheel )
	{
		return false;
	}

	//인터페이스가 허용되는지 확인하기 위한 함수
	protected virtual bool IsAllow()
	{
		if( EventSystem.current.currentSelectedGameObject!=null )
		{
#if UNITY_EDITOR
			//Debug.Log(EventSystem.current.currentSelectedGameObject, EventSystem.current.currentSelectedGameObject);
#endif
			return false;
		}

		return true;
	}
}