public class CBrowser : CBrowserLegacy
{
	public override void ONMULTI_TOUCH_DOWN( tagTouch touch1, tagTouch touch2 )
	{
		if( touch1==null ) return;
		if( touch2==null ) return;

		app.Game.ONMULTI_TOUCH_DOWN( touch1, touch2 );
	}

	public override void ONMULTI_TOUCH_DRAG( tagTouch touch1, tagTouch touch2 )
	{
		if( touch1==null ) return;
		if( touch2==null ) return;

//		app.Game.CameraDrag( touch1.position, touch1 );
//		app.Game.CameraDrag( touch2.position, touch2 );
		app.Game.ONMULTI_TOUCH_DRAG( touch1, touch2 );
	}

	//취소 입력에 반응하기 위한 함수
	public override bool ONCANCEL( CANCEL Cancel )
	{
		if( base.ONCANCEL(Cancel) ) return true;

		switch( play.Scene )
		{
			case SCENE.PLAY:
				if( app.Game.ONCANCEL(Cancel) ) return true;
				break;
		}

		return false;
	}
}