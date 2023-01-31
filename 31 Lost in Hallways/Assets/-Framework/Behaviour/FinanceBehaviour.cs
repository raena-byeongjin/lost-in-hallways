using UnityEngine.UI;

public abstract class FinanceBehaviour : RectTransformBehaviour
{
	private Text m_value = null;
	private IconBehaviour m_icon = null;

	protected override void Awake()
	{
		base.Awake();
		m_value = GetComponentInChildren(typeof(Text)) as Text;
		m_icon = GetComponentInChildren(typeof(IconBehaviour)) as IconBehaviour;
	}

	public void Set( ulong value )
	{
		Value().text = Library.NumberFormat(value);
	}

	//Value 객체를 얻기 위한 함수
	public Text Value()
	{
		return m_value;
	}

	//아이콘 객체를 얻기 위한 함수
	public IconBehaviour Icon()
	{
		return m_icon;
	}
}