﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//-----------------------------------------------------------------------------------------------------------------------------------
// 이벤트 정보를 처리하기 위한 클래스
//-----------------------------------------------------------------------------------------------------------------------------------
public class CEvent : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	[System.Serializable]
	public class tagData
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public List<tagEvent>   Events			= new List<tagEvent>();
		public tagEvent			currentEvent	= (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public bool				wait			= (false);
		public bool				Skip			= (false);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}
	public tagData		data		= new tagData();

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	protected CApp		app			= (null);
	protected CPlay		play		= (null);

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	void OnEnable()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(this.app)		= (CApp.This);
		(this.play)		= (CPlay.This);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이벤트를 활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool ON()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		Wait( false );

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		tagEvent	Event	= Get();
		if( (Event)!=(null) )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			data.Events.Remove( Event );

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( !app.EventQuery.ON(Event) )
			{
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				if( Is() )
				{
					Wait( true );
				}

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				return false;
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			return true;
		}
		else
		{
			OFF();
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 인터페이스를 비활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void OFF()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		(data.currentEvent)		= (null);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
//		app.ViewSprite.OFF();
//		app.ViewFlash.OFF();

		/*
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (play.Scene)==(SCENE.PLAY) && app.ViewVictory.IsLoad() )
		{
			app.MessageQueue.ON( (WM.CALL), (null), (null), (2f), (app.ViewVictory.Active) );
		}
		*/

		/*
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (play.Scene)==(SCENE.PLAY) )
		{
			if( app.ViewTutorial.Is() )
			{
				//튜토리얼이 완료될때까지 대기함
			}
			else
			if( app.ViewReady.IsLoad() )
			{
				app.MessageQueue.ON( (WM.CALL), (null), (null), (0.5f), (app.ViewReady.Active) );
			}
		}
		*/

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이벤트 타입을 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public EVENT_TYPE GetType( string type )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (type)==(null) ) return (EVENT_TYPE.NOTHING);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		int		End		= (int)(EVENT_TYPE.END);
		for( int i=0; i<(End); i++ )
		{
			if( ((EVENT_TYPE)(i)).ToString().ToUpper()==type.ToUpper() )
			{
				return ((EVENT_TYPE)(i));
			}
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return (EVENT_TYPE.NOTHING);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이벤트 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public tagEvent Get()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (data.Events.Count)>(0) )
		{
			return (data.Events[0]);
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return (null);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이벤트가 진행중인지 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool Is()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (data.Events.Count)>(0) )
		{
			return true;
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이벤트를 초기화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Clear()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		data.Events.Clear();

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public TextAsset LoadAsset( string Event )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (Event)==(null) ) return (null);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		TextAsset	eventAsset	= (Resources.Load("Event/"+(Event)) as TextAsset);
#if UNITY_EDITOR
		if( (eventAsset)==(null) )
		{
	#if MERGE
			if( Library.Is(Event) ) Debug.LogError( "대체 이벤트를 실행함: "+(Event) );
	#endif
			(eventAsset)		= (Resources.Load("Event/Basic") as TextAsset);
		}
#endif

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return (eventAsset);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 스킵을 처리하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Skip()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		tagEvent		Event		= (null);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		(data.Skip)		= (true);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		while( Is() )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			(Event)	= Get();
			if( (Event)!=(null) )
			{
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				ON();

				/*
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				switch( Event.Type )
				{
					case (EVENT_TYPE.FIELD):
					case (EVENT_TYPE.SCENE):
					case (EVENT_TYPE.ROUND):
					case (EVENT_TYPE.VICTORY):
					case (EVENT_TYPE.GAMEOVER):
					case (EVENT_TYPE.TUTORIAL):
						Wait( true );
						(data.Skip)	= (false);
						break;
				}
				*/

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
			}
			
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( !(data.Skip) ) break;

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( !app.Event.Is() )
		{
			app.Event.OFF();
			Wait( false );
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		(data.Skip)		= (false);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsWait()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return (data.wait);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 스킵을 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsSkip()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return (data.Skip);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이벤트를 활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void funcON( object wParam=(null), object lParam=(null) )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		ON();

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이벤트의 메모리 인덱스를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public string GetMemorizeId( string Event )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return "Event_"+(Event);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 대기 상태를 설정하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Wait( bool value=(true) )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		(data.wait)		= (value);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 현재 이벤트가 대화 이벤트인지 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsConverseEvent()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (data.currentEvent)!=(null) )
		{
			switch( data.currentEvent.Type )
			{
				case (EVENT_TYPE.CONVERSE):
				case (EVENT_TYPE.RELENA):
					return true;
			}
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Relena( string text )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (text)==(null) ) return;

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		tagEvent	Event	= new tagEvent();

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		(Event.Type)	= (EVENT_TYPE.RELENA);
		(Event.text)	= (text);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		data.Events.Add( Event );

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Function( Action<object, object> func, object wParam=(null), object lParam=(null), float delay=(0f) )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (func)==(null) ) return;
//		if( (wParam)==(null) ) return;	//(NULL)값을 허용함
//		if( (lParam)==(null) ) return;	//(NULL)값을 허용함

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		tagEvent	Event	= new tagEvent();

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		(Event.Type)	= (EVENT_TYPE.FUNCTION);
		(Event.delay)	= (delay);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		Event.callback.Set( (func), (wParam), (lParam) );

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		data.Events.Add( Event );

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Function( Action<object, object> func, float delay )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (func)==(null) ) return;

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		Function( (func), (null), (null), (delay) );

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Memorize( string id, string value )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( !Library.Is(id) ) return;
//		if( (value)==(null) ) return;	//(NULL)값을 허용함

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		tagEvent	Event	= new tagEvent();

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		(Event.Type)	= (EVENT_TYPE.MEMORIZE);
		(Event.text)	= (id);
		(Event.obj)		= (value);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		data.Events.Add( Event );

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