using System;
using UnityEngine;

public class ScreenCapture : MonoBehaviour
{
	private tagCallback callback = null;
	private float delay = 0f;

	void OnDestroy()
	{
		UserInterface.Out(this);
	}

	void OnGUI()
	{
		if( delay>0f )
		{
			delay -= Time.deltaTime;
			return;
		}

		if( enabled )
		{
			Texture2D texture = new Texture2D( Screen.width, Screen.height );
			texture.ReadPixels( new Rect( 0f, 0f, Screen.width, Screen.height ), 0, 0, false );
			texture.Apply();

			if( callback.Is() )
			{
				if( callback.wParam==null )callback.wParam = texture;
				callback.Call();
			}

			Component.Destroy(this);
			Enable( false );
		}
	}

	public void SetCallback( Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( func==null ) return;
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( callback!=null )
		{
			callback.Set( func, wParam, lParam );
		}
		else
		{
			callback = new tagCallback( func, wParam, lParam );
		}
	}

	//지연 시간을 설정하기 위한 함수
	public void SetDelay( float delay )
	{
		this.delay = delay;
	}

	//활성화를 설정하기 위한 함수
	void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}

	//움직임을 확인하기 위한 함수
	public bool IsAction()
	{
		return isActiveAndEnabled;
	}
}