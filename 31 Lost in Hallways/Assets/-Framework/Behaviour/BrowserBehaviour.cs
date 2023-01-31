using UnityEngine;

public class BrowserBehaviour : FrameworkBehaviour
{
	public virtual void ONDRAG( Vector2 point, tagTouch touch )
	{
		switch( play.Scene )
		{
			case SCENE.PLAY:
				ApplicationBehaviour.This.Game.ONDRAG( point, touch );
				return;
		}
	}

	public virtual void ONMULTI_TOUCH_DOWN( tagTouch touch1, tagTouch touch2 )
	{
	}

	public virtual void ONMULTI_TOUCH_DRAG( tagTouch touch1, tagTouch touch2 )
	{
	}

	//취소 입력에 반응하기 위한 함수
	public virtual bool ONCANCEL( CANCEL Cancel )
	{
		if( app.Interface.ONCANCEL(Cancel) ) return true;
		return false;
	}

	//엔터 입력에 반응하기 위한 함수
	public virtual bool ONRETURN()
	{
		if( app.Interface.ONRETURN() ) return true;
		return false;
	}

	//엔터 입력에 반응하기 위한 함수
	public virtual bool ONSKIP()
	{
		if( app.Interface.ONSKIP() ) return true;
		return false;
	}
}