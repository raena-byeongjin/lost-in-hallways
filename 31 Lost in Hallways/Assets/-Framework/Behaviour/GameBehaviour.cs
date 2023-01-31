using UnityEngine;

public class GameBehaviour : FrameworkBehaviour
{
	//마우스 왼쪽 버튼을 눌렀을 때 반응하기 위한 함수
	public virtual bool ONLBUTTONUP( Vector2 point )
	{
		return false;
	}

	//드래그를 처리하기 위한 함수
	public virtual bool ONDRAG( Vector2 point, tagTouch touch )
	{
		return false;
	}

	public virtual void ONMULTI_TOUCH_DOWN( tagTouch touch1, tagTouch touch2 )
	{
	}

	public virtual void ONMULTI_TOUCH_DRAG( tagTouch touch1, tagTouch touch2 )
	{
	}

	//취소 입력을 처리하기 위한 함수
	public virtual bool ONCANCEL( CANCEL Cancel )
	{
		return false;
	}

	//키 입력을 처리하기 위한 함수
	public virtual void Key()
	{
	}
}