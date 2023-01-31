using UnityEngine.UI;
using UnityEngine;

public class TextBehaviour : RectTransformBehaviour
{
	private Text m_text = null;
	private RectTransform m_raw_transform = null;

	protected override void Awake()
	{
		if( m_transform==null )
		{
			base.Awake();
			m_text = GetComponentInChildren(typeof(Text)) as Text;
		}
	}

	//이미지를 설정하기 위한 함수
	public void Set( string value )
	{
//		if( !Library.Is(value) ) return; //(NULL)값을 허용함
		Text().text = value;
	}

	//문자열 객체를 얻기 위한 함수
	public Text Text()
	{
		if( m_transform==null )
		{
			Awake();
		}

		return m_text;
	}

	public RectTransform GetRawTransform()
	{
		if( m_raw_transform==null && Text()!=null )
		{
			m_raw_transform = Text().transform as RectTransform;
		}

		return m_raw_transform;
	}

	//색상을 설정하기 위한 함수
	public void Set( Color color )
	{
		Text().color = color;
	}

	//색상을 얻기 위한 함수
	public Color Color()
	{
		return Text().color;
	}

	//투명도를 얻기 위한 함수
	public float Alpha()
	{
		return Color().a;
	}

	public float GetPreferredWidth()
	{
		return Text().preferredWidth;
	}
}