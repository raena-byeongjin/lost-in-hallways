using UnityEngine;

public class IndigatorInterface
{
	//인디게이터를 활성화 하기 위한 함수
	public static void ON( Component component, string description=null )
	{
		if( component==null ) return;
//		if( !Library.Is(description) ) return; //(NULL)값을 허용함

		ApplicationBehaviour.This.ViewIndigator.ON( component, description );
	}

	//인디게이터를 활성화 하기 위한 함수
	public static void OFF( Component component )
	{
		if( component==null ) return;

		ApplicationBehaviour.This.ViewIndigator.OFF( component );
	}
}