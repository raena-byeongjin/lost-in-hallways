using UnityEngine;

public class ShaderAmount : MotionBehaviour
{
	public string propertyName = "_Amount";
	private Material m_material = null;

	private float fStart = 0f;
	private float fEnd = 1f;

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

	//활성화를 설정하기 위한 함수
	void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
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
		Material().SetFloat( propertyName, value );
	}
}