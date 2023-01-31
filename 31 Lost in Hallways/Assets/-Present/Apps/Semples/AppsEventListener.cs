using UnityEngine;
using System.Collections;

//이벤트 정보를 처리하기 위한 클래스
public class AppsEventListener : FrameworkBehaviour
{
	public string AppName = null;
	public string ServerAddr = null;
	public string Root = null;

	[System.NonSerialized]
	public string ServerRoot = null;

	public float fVersion = 0.0001f;

	public virtual void OnInitialize()
	{
	}
}