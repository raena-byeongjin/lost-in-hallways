using System;

public class Confirm
{
	public static objConfirm ON( string value, CONFIRM nType=CONFIRM.OK, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( !Library.Is(value) ) return null;
//		if( func==null ) return null;	//(NULL)값을 허용함
//		if( wParam==null ) return null;	//(NULL)값을 허용함
//		if( lParam==null ) return null;	//(NULL)값을 허용함

		return ApplicationBehaviour.This.Confirm.ON( value, nType, func, wParam, lParam );
	}

	public static objConfirm ON( string value, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( !Library.Is(value) ) return null;
//		if( func==null ) return null;	//(NULL)값을 허용함
//		if( wParam==null ) return null;	//(NULL)값을 허용함
//		if( lParam==null ) return null;	//(NULL)값을 허용함

		return ApplicationBehaviour.This.Confirm.ON( value, CONFIRM.OK, func, wParam, lParam );
	}
}