using UnityEngine.UI;
using UnityEngine;

public class ImageBehaviour : RectTransformBehaviour
{
	private Image m_image = null;

	protected override void Awake()
	{
		base.Awake();
		m_image = GetComponentInChildren(typeof(Image)) as Image;
	}

	//이미지를 설정하기 위한 함수
	public void Set( Texture2D texture )
	{
		if( texture==null ) return;
		Set( Library.Sprite(texture) );
	}

	//이미지를 설정하기 위한 함수
	public void Set( Sprite sprite )
	{
		if( sprite==null ) return;
		Image().sprite = sprite;
	}

	//색상을 설정하기 위한 함수
	public void Set( Color color )
	{
		Image().color = color;
	}

	//이미지 객체를 얻기 위한 함수
	public Image Image()
	{
		return m_image;
	}

	public void SetNativeSize()
	{
		Image().SetNativeSize();
	}

	//투명도를 설정하기 위한 함수
	public void Alpha( float value )
	{
		Image().color = Library.Color( Image().color, value );
	}

	//투명도를 설정하기 위한 함수
	public float Alpha()
	{
		return Image().color.a;
	}

	//색상을 얻기 위한 함수
	public Color GetColor()
	{
		return Image().color;
	}

	//스프라이트를 얻기 위한 함수
	public Sprite Sprite()
	{
		return Image().sprite;
	}
}