using UnityEngine;

public class ProcBehaviour : FrameworkBehaviour
{
	protected override void Awake()
	{
		base.Awake();

		for( int i=0; i<TouchInterface.Touches().Length; i++ )
		{
			TouchInterface.Touches()[i] = new tagTouch();
			TouchInterface.Touches()[i].index = (TOUCH_INDEX)i;
		}
	}

	protected virtual void Update()
	{
		if( UserInterface.Is() )
		{
			if( Platform.IsDesktop() )
			{
				int End = (int)TOUCH_INDEX.WHEEL;
				for( int i=0; i<=End; i++ )
				{
					Touch( i );
				}

				//마우스 휠
				float fWheel = Input.GetAxis("Mouse ScrollWheel");
				if( fWheel!=0f )
				{
					ONWHEEL( fWheel );
				}
			}
			else
			{
				Touch();
			}

			Key();
		}
	}

	//터치 입력을 처리하기 위한 함수
	void Touch()
	{
		tagTouch touch = null;
		foreach( Touch _touch in Input.touches )
		{
			touch = TouchInterface.GetTouch(_touch.fingerId);
			if( touch==null ) continue;

			switch( _touch.phase )
			{
				case TouchPhase.Began:
					touch.is_push	= true;
					touch.position	= _touch.position;
					ONTOUCHDOWN( _touch.position, touch );
					break;

				case TouchPhase.Moved:
					touch.position	= _touch.position;
					ONDRAG( _touch.position, touch );
					break;

				case TouchPhase.Ended:
					touch.is_push	= false;
					touch.position	= _touch.position;
					ONTOUCHUP( _touch.position, touch );
					break;
			}
		}
	}

	protected virtual void Touch( int touchIndex )
	{
		tagTouch touch = TouchInterface.GetTouch(touchIndex);

		if( Input.GetMouseButtonDown(touchIndex) )
		{
			touch.is_push	= true;
			touch.position	= Input.mousePosition;
			ONTOUCHDOWN( Input.mousePosition, touch );
		}

		if( Input.GetMouseButton(touchIndex) )
		{
			touch.position	= Input.mousePosition;
			ONDRAG( Input.mousePosition, touch );
		}

		if( Input.GetMouseButtonUp(touchIndex) )
		{
			touch.is_push	= false;
			touch.position	= Input.mousePosition;
			ONTOUCHUP( Input.mousePosition, touch );
		}
		else
		if( touch.is_push && !Input.GetMouseButton(touchIndex) )
		{
			touch.is_push	= false;
			ONTOUCHUP( Input.mousePosition, touch );
		}
	}

	//멀티 터치를 처리하기 위한 함수
	void ONTOUCHDOWN( Vector2 point, tagTouch touch )
	{
		if( touch==null ) return;

		if( touch.time!=Time.time )
		{
			touch.Reset();
		}

		touch.point = point;
		touch.start = touch.point;
		touch.length = 0;

		foreach( TouchBehaviour touchbehaviour in TouchBehaviour.GetList() )
		{
			if( touchbehaviour.ONTOUCHDOWN( point, touch ) )
			{
				break;
			}
		}

		if( touch==TouchInterface.Context() && TouchInterface.Push().is_push )
		{
			tagTouch inverse = TouchInterface.Inverse(touch);
			if( inverse!=null && inverse.is_push )
			{
				ApplicationBehaviour.This.Browser.ONMULTI_TOUCH_DOWN( touch, inverse );
			}
		}
	}

	//마우스 드래그에 반응하기 위한 함수
	void ONDRAG( Vector2 point, tagTouch touch )
	{
		if( touch==null ) return;

		if( touch.point!=point )
		{
			CameraBehaviour.Ray( point );

			if( touch.point!=new Vector2() )
			{
				touch.length += Vector2.Distance( touch.point, point );
			}

			foreach( TouchBehaviour touchbehaviour in TouchBehaviour.GetList() )
			{
				if( touchbehaviour.ONDRAG( point, touch ) )
				{
					break;
				}
			}

			tagTouch inverse = TouchInterface.Inverse(touch);
			if( inverse!=null && inverse.is_push )
			{
				ApplicationBehaviour.This.Browser.ONMULTI_TOUCH_DRAG( touch, inverse );
			}
			else
			{
				ApplicationBehaviour.This.Browser.ONDRAG( point, touch );
			}

			touch.point	= point;
		}
	}

	//멀티 터치를 처리하기 위한 함수
	void ONTOUCHUP( Vector2 point, tagTouch touch )
	{
		if( touch==null ) return;

		if( touch.point!=new Vector2() )
		{
			touch.length += Vector2.Distance( touch.point, point );
		}

		while( true )
		{
			/*
			if( !touch.Is() )
			{
				if( Confirm.Is() && ViewConfirm.This.nType!=CONFIRM.YESNO )
				{
					ViewConfirm.This.Yes();
					break;
				}
			}
			*/

			foreach( TouchBehaviour touchbehaviour in TouchBehaviour.GetList() )
			{
				if( touchbehaviour.ONTOUCHUP( point, touch ) )
				{
					break;
				}
			}

			break;
		}

		touch.Reset();
	}

	//키 입력을 처리하기 위한 함수
	protected virtual void Key()
	{
		//Escape
		if( Input.GetKeyUp(KeyCode.Escape) )
		{
			app.Browser.ONCANCEL( CANCEL.ESCAPE );
		}

		//Backspace
		if( Input.GetKeyUp(KeyCode.Backspace) )
		{
			app.Browser.ONCANCEL( CANCEL.BACKSPACE );
		}

		//Backspace
		if( Input.GetMouseButtonUp(1) )
		{
			app.Browser.ONCANCEL( CANCEL.MOUSE_RBUTTON );
		}

		//Return
		if( Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter) )
		{
			app.Browser.ONRETURN();
		}

		//Space
		if( Input.GetKeyUp(KeyCode.Space) )
		{
			app.Browser.ONSKIP();
		}

		switch( play.Scene )
		{
			case SCENE.PLAY:
				app.Game.Key();
				break;
		}
	}

	//휠 입력을 처리하기 위한 함수
	protected virtual void ONWHEEL( float value )
	{
	}
}