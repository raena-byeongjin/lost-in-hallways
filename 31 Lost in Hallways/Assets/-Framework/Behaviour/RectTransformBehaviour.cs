using UnityEngine;

public abstract class RectTransformBehaviour : GameObjectBehaviour
{
	protected RectTransform m_transform = null;

	protected override void Awake()
	{
		if( m_transform==null )
		{
			base.Awake();
			m_transform = transform as RectTransform;
		}
	}

	//트랜스폼을 얻기 위한 함수
	public RectTransform Transform()
	{
		if( m_transform==null )
		{
			Awake();
		}

		return m_transform;
	}

	//가로 너비를 얻기 위한 함수
	public virtual float Width()
	{
		return Transform().sizeDelta.x * Transform().localScale.x;
	}

	//세로 높이를 얻기 위한 함수
	public virtual float Height()
	{
		return Transform().sizeDelta.y * Transform().localScale.y;
	}
}