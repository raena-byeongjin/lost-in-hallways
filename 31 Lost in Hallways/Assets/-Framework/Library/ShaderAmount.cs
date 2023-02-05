using UnityEngine.UI;
using UnityEngine;

public class ShaderAmount : MotionBehaviour
{
	public string propertyName = "_Amount";
	private Material m_material = null;
	private Mask m_mask = null;

	private float fStart = 0f;
	private float fEnd = 1f;

	protected override void Awake()
	{
		base.Awake();
		m_mask = GetComponentInParent(typeof(Mask)) as Mask;
	}

	protected override void Refresh( float value )
	{
		Set( Mathf.Lerp( fStart, fEnd, Mathf.Sin(Mathf.Pow(value, fPow)*Mathf.PI*0.5f) ) );
	}

	//인터페이스를 활성화 하기 위한 함수
	public void ON( Material material, float value, float transit=-1f, float pow=-1f, float delay=-1f, END_ACTION nEndAction=END_ACTION.NOTHING )
	{
		m_material = material;

		fStart = Get();
		fEnd = value;

		base.ON( transit, pow, delay, nEndAction );
	}

	//인터페이스를 활성화 하기 위한 함수
	public void ON( float value, float transit=-1f, float pow=-1f, float delay=-1f, END_ACTION nEndAction=END_ACTION.NOTHING )
	{
		ON( FindMaterial(), value, transit, pow, delay, nEndAction );
	}

	//메터리얼을 얻기 위한 함수
	Material Material()
	{
		return m_material;
	}

	//Amount를 얻기 위한 함수
	float Get()
	{
		return Material().GetFloat(propertyName);
	}

	//Amount를 설정하기 위한 함수
	void Set( float value )
	{
		if( m_mask!=null )
		{
			m_mask.enabled = false;
			m_mask.enabled = true;
		}

		Material().SetFloat( propertyName, value );
	}

	//메터리얼을 검색하기 위한 함수
	Material FindMaterial()
	{
		Graphic graphic = GetComponent(typeof(Graphic)) as Graphic;
		if( graphic!=null && !Library.IsDefaulUIMaterial(graphic.material) )
		{
			return graphic.material;
		}

		Renderer renderer = GetComponent(typeof(Renderer)) as Renderer;
		if( renderer!=null )
		{
			return renderer.material;
		}

		return null;
	}
}