﻿using UnityEngine;

public class FlickerBehaviour : MotionBehaviour
{
	private Vector3 vStart = new Vector3();
	private Vector3 vEnd = new Vector3();

	protected override void Refresh( float value )
	{
		Transform().position = Vector3.Lerp( vStart, vEnd, Mathf.Sin(Mathf.Pow(value, fPow)*Mathf.PI*0.5f) );
	}

	//인터페이스를 활성화 하기 위한 함수
	public void ON( Vector3 positin, float transit=-1f, float pow=-1f, float delay=-1f, END_ACTION nEndAction=END_ACTION.NOTHING )
	{
		vStart = Transform().position;
		vEnd = positin;

		base.ON( transit, pow, delay, nEndAction );
	}
}