using UnityEngine.UI;

public class StickCharacterInfo : StickBehaviour
{
	private Text m_text = null;

	protected override void Awake()
	{
		base.Awake();
		m_text = GetComponentInChildren(typeof(Text)) as Text;
	}

	//ĳ���� ������ �����ϱ� ���� �Լ�
	public void Set( tagCharacterStatic characterstatic )
	{
		if( characterstatic==null ) return;
		m_text.text = Language.Get(characterstatic.Name());
	}
}