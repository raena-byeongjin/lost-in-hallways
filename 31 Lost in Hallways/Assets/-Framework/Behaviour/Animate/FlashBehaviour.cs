using UnityEngine.UI;
using UnityEngine;

public class FlashBehaviour : MotionBehaviour
{
	private float fStart = 1f;
	private float fEnd = 0f;
	private Image m_image = null;

	protected override void Awake()
	{
		m_image = GetComponentInChildren(typeof(Image)) as Image;
	}

	protected override void Refresh( float value )
	{
		m_image.color = Library.Color( m_image.color, Mathf.Lerp( fStart, fEnd, Mathf.Pow(value, fPow)*Mathf.PI*0.5f ) );
	}
}