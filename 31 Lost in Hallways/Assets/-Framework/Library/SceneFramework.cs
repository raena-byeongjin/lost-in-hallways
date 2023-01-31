using System.Collections.Generic;
using UnityEngine;

public class SceneFramework : FrameworkBehaviour
{
	public SCENE nScene = SCENE.NOTHING;
	public static List<SceneFramework> SceneFrameworks = new List<SceneFramework>();

	protected override void Awake()
	{
		base.Awake();
		SceneFrameworks.Add(this);
	}

	protected virtual void OnDestroy()
	{
		SceneFrameworks.Remove(this);
	}

	//인터페이스를 활성화 하기 위한 함수
	public virtual void ON()
	{
	}

	//인터페이스를 비활성화 하기 위한 함수
	public virtual void OFF()
	{
	}

	//마우스 왼쪽 버튼을 뗬을 때 반응하기 위한 함수
	public virtual bool ONLBUTTONUP( Vector2 point, tagTouch touch )
	{
		return false;
	}
}