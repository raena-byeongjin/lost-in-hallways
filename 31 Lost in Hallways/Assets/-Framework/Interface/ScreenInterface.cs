using UnityEngine;

public class ScreenInterface : MonoBehaviour
{
	private const float fStandardWidth = 1024f;
	private const float fStandardHeight = 2048f;
	private static float fScreenSize = 0f; //기준 화면 대비 배율


	//기준 화면 가로 너비를 얻기 위한 함수
	public static float GetStandardWidth()
	{
		return fStandardWidth;
	}

	//기준 화면 세로 높이를 얻기 위한 함수
	public static float GetStandardHeight()
	{
		return fStandardHeight;
	}

	//화면 가로 너비를 얻기 위한 함수
	public static float Width()
	{
		if( ApplicationBehaviour.IsStartup() )
		{
			return PlayBehaviour.This.uiCanvas.Width();
		}

		return fStandardWidth;
	}

	//화면 세로 높이를 얻기 위한 함수
	public static float Height()
	{
		if( ApplicationBehaviour.IsStartup() )
		{
			return PlayBehaviour.This.uiCanvas.Height();
		}

		return fStandardHeight;
	}

	//화면 크기를 얻기 위한 함수
	public static float GetScreenSize()
	{
		if( fScreenSize==0f )
		{
			return Mathf.Min( Library.Divide( fStandardWidth, (float)Screen.width ), Library.Divide( fStandardHeight, (float)Screen.height ) );
		}

		return fScreenSize;
	}
}