using UnityEngine;

//프레임워크 정보를 처리하기 위한 클래스
public abstract class FrameworkBehaviour : TransformBehaviour
{
	protected ApplicationBehaviour app = null;
	protected CPlay play = null;

	protected override void Awake()
	{
		base.Awake();

		app = ApplicationBehaviour.This;
		play = CPlay.This;
	}
}