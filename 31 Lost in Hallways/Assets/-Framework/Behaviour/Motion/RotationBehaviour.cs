using UnityEngine;

public class RotationBehaviour : MotionBehaviour
{
	private Quaternion vStart = new Quaternion();
	private Quaternion vEnd = new Quaternion();

	protected override void Refresh( float value )
	{
		Transform().localRotation = Quaternion.Lerp( vStart, vEnd, Mathf.Sin(Mathf.Pow(value, fPow)*Mathf.PI*0.5f) );
	}

	//인터페이스를 활성화 하기 위한 함수
	public void ON( Vector3 vRotation, float transit=-1f, float pow=-1f, float delay=-1f, END_ACTION nEndAction=END_ACTION.NOTHING )
	{
		vStart = Transform().localRotation;
		vEnd = Quaternion.Euler(vRotation);

		base.ON( transit, pow, delay, nEndAction );
	}
}