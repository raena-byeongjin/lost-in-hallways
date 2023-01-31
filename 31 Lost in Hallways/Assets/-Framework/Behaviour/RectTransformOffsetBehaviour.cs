using UnityEngine;

public class RectTransformOffsetBehaviour : MotionBehaviour
{
	private RectTransform m_rectTransform = null;

	private Vector2 vMinStart = new Vector2();
	private Vector2 vMaxStart = new Vector2();
	private Vector2 vMinEnd = new Vector2();
	private Vector2 vMaxEnd = new Vector2();

	protected override void Awake()
	{
		base.Awake();
		m_rectTransform = Transform() as RectTransform;
	}

	protected override void Refresh( float value )
	{
		_RectTransform().offsetMin = Vector2.Lerp( vMinStart, vMinEnd, Mathf.Sin(Mathf.Pow(value, fPow)*Mathf.PI*0.5f) );
		_RectTransform().offsetMax = Vector2.Lerp( vMaxStart, vMaxEnd, Mathf.Sin(Mathf.Pow(value, fPow)*Mathf.PI*0.5f) );
	}

	//인터페이스를 활성화 하기 위한 함수
	public void ON( Vector2 vMin, Vector2 vMax, float transit=-1f, float pow=-1f, float delay=-1f, END_ACTION nEndAction=END_ACTION.NOTHING )
	{
		vMinStart = _RectTransform().offsetMin;
		vMaxStart = _RectTransform().offsetMax;
		vMinEnd = vMin;
		vMaxEnd = vMax;

		base.ON( transit, pow, delay, nEndAction );
	}

	//트랜스폼을 얻기 위한 함수
	RectTransform _RectTransform()
	{
		return m_rectTransform;
	}
}