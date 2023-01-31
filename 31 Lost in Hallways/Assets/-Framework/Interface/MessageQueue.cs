using System;

public class MessageQueue
{
	public static tagMessageQueue ON( Action<object, object> func, object wParam=null, object lParam=null, float delay=0f )
	{
		if( func==null ) return null;
//		if( wParam==null ) return null;	//(NULL)값을 허용함
//		if( lParam==null ) return null;	//(NULL)값을 허용함

		return ApplicationBehaviour.This.MessageQueue.ON( func, wParam, lParam, delay );
	}

	public static tagMessageQueue ON( Action<object, object> func, float delay )
	{
		if( func==null ) return null;
		return ON( func, null, null, delay );
	}

	public static bool Is( Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( func==null ) return false;
//		if( wParam==null ) return false; //(NULL)값을 허용함
//		if( lParam==null ) return false; //(NULL)값을 허용함

		return ApplicationBehaviour.This.MessageQueue.Is( func, wParam, lParam );
	}

	public static tagMessageQueue Skip( Action<object, object> func, object wParam=null, object lParam=null, float delay=0f )
	{
		if( func==null ) return null;
//		if( wParam==null ) return null;	//(NULL)값을 허용함
//		if( lParam==null ) return null;	//(NULL)값을 허용함

		return ApplicationBehaviour.This.MessageQueue.Skip( func, wParam, lParam, delay );
	}

	public static void Cancel( Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( func==null ) return;
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		ApplicationBehaviour.This.MessageQueue.Cancel( func, wParam, lParam );
	}
}