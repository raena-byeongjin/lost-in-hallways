using System.Collections.Generic;
using UnityEngine;

public class ViewStick : ViewBehaviour
{
	private static ViewStick This = null;
	private List<StickBehaviour> Sticks = new List<StickBehaviour>();

	protected override void Awake()
	{
		This = this;
		base.Awake();

		Framework.Camera().onChange += onChange;
	}

	//인터페이스를 활성화 하기 위한 함수
	public static StickBehaviour ON( string value, Transform target, float height )
	{
		if( !Library.Is(value) ) return null;
		if( target==null ) return null;

		if( This==null )
		{
			MainUI.Create("Stick");
		}

		return This.Create( value, target, height );
	}

	//인터페이스를 활성화 하기 위한 함수
	StickBehaviour Create( string value, Transform target, float height )
	{
		if( !Library.Is(value) ) return null;
		if( target==null ) return null;

		StickBehaviour stickbehaviour = MainUI.Create( Transform(), value ).GetComponent(typeof(StickBehaviour)) as StickBehaviour;
		if( stickbehaviour!=null )
		{
			stickbehaviour.Set( target, height );
			GetList().Add(stickbehaviour);
		}

		return stickbehaviour;
	}

	//리스트를 얻기 위한 함수
	public List<StickBehaviour> GetList()
	{
		return Sticks;
	}

	void onChange( CameraBehaviour camerabehaviour )
	{
		if( camerabehaviour==null ) return;

		foreach( StickBehaviour stickbehaviour in GetList() )
		{
			stickbehaviour.Refresh();
		}
	}

	//객체를 해제하기 위한 함수
	public void Release( StickBehaviour stickbehaviour )
	{
		if( stickbehaviour==null ) return;
		GetList().Remove( stickbehaviour );
	}
}