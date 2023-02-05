using UnityEngine.UI;

public class StickCharacterInfo : StickBehaviour
{
	private Text m_text = null;

	protected override void Awake()
	{
		base.Awake();
		m_text = GetComponentInChildren(typeof(Text)) as Text;
	}

	//캐릭터 정보를 설정하기 위한 함수
	public void Set( tagCharacterStatic characterstatic )
	{
		if( characterstatic==null ) return;
		m_text.text = Language.Get(characterstatic.Name());
	}
}