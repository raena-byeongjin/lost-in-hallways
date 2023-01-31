using UnityEngine.UI;

public class GraphicBehaviour : RectTransformBehaviour
{
	private Graphic m_graphic = null;

	protected override void Awake()
	{
		if( m_transform==null )
		{
			base.Awake();
			m_graphic = GetComponentInChildren(typeof(Graphic)) as Graphic;
		}
	}

	//이미지 객체를 얻기 위한 함수
	public Graphic Graphic()
	{
		if( m_transform==null )
		{
			Awake();
		}

		return m_graphic;
	}

	//투명도를 설정하기 위한 함수
	public void Alpha( float value )
	{
		Graphic().color = Library.Color( Graphic().color, value );
	}

	//투명도를 설정하기 위한 함수
	public float Alpha()
	{
		return Graphic().color.a;
	}
}