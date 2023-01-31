using System;

public class DownloadInterface
{
	//다운로드를 요청하기 위한 함수
	public static void ON( string url, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( !Library.Is(url) ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		ApplicationBehaviour.This.Download.ON( url, func, wParam, lParam );
	}

	//다운로드를 요청하기 위한 함수
	public static void ON( tagTexture texture, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( texture==null ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		ApplicationBehaviour.This.Download.ON( texture, func, wParam, lParam );
	}

	//콜백을 설정하기 위한 함수
	public static void SetCallback( Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( func==null ) return;
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		ApplicationBehaviour.This.Download.SetCallback( func, wParam, lParam );
	}

	//인디게이터를 활성화 하기 위한 함수
	public static void Indigator()
	{
		ApplicationBehaviour.This.Download.Indigator();
	}
}