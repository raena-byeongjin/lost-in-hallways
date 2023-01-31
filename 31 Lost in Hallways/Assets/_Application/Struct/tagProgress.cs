using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//진행 정보를 처리하기 위한 구조체
[System.Serializable]
public class tagProgress
{
	public string			text		= null;
	public float			value		= 0f;
	public AsyncOperation	operation	= null;

	public Component		component	= null;
	public bool				occupy		= true;

	//진행중인지 확인하기 위한 함수
	public bool IsProgress()
	{
		if( ( GetValue()<1f && operation==null ) || ( operation!=null && operation.isDone ) )
		{
			return true;
		}

		return false;
	}

	//진행도를 얻기 위한 함수
	public float GetValue()
	{
		if( operation!=null )
		{
			return operation.progress;
		}

		return this.value;
	}
}