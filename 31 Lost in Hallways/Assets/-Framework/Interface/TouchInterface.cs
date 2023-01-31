using UnityEngine.EventSystems;

public class TouchInterface
{
	public static tagTouch[] touch = new tagTouch[(int)TOUCH_INDEX.END];

	//터치 정보를 얻기 위한 함수
	public static tagTouch GetTouch( int touchIndex )
	{
		if( touchIndex<0 || touchIndex>=Touches().Length )
		{
			return null;
		}

		return Touches()[touchIndex];
	}

	//터치 정보를 얻기 위한 함수
	public static tagTouch GetTouch( TOUCH_INDEX touchIndex )
	{
		return GetTouch((int)touchIndex);
	}

	//터치 정보를 얻기 위한 함수
	public static tagTouch GetTouch( PointerEventData pointer )
	{
		if( pointer==null ) return null;

		if( pointer.pointerId<0 )
		{
			return GetTouch((int)pointer.button);
		}

		return GetTouch(pointer.pointerId);
	}

	public static tagTouch[] Touches()
	{
		return touch;
	}

	public static tagTouch Push()
	{
		return GetTouch(TOUCH_INDEX.FIRST);
	}

	public static tagTouch Context()
	{
		return GetTouch(TOUCH_INDEX.CONTEXT);
	}

	public static tagTouch Wheel()
	{
		return GetTouch(TOUCH_INDEX.WHEEL);
	}

	public static tagTouch Inverse( tagTouch touch )
	{
		if( touch==null ) return null;

		if( touch==Push() )
		{
			return Context();
		}
		else
		if( touch==Context() )
		{
			return Push();
		}

		return null;
	}
}