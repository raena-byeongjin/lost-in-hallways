using UnityEngine.UI;
using UnityEngine;

public class ButtonCharacterSelect : ButtonBehaviour
{
	private ViewCharacterSelect m_view = null;
	public Image m_image = null;
	public Text m_name = null;
	private ScaleBehaviour m_scalebehaviour = null;
	private FlashBehaviour m_flashbehaviour = null;
	private ShaderAmount m_grayscale = null;

	protected override void Awake()
	{
		base.Awake();

		m_view = GetComponentInParent(typeof(ViewCharacterSelect)) as ViewCharacterSelect;
		m_scalebehaviour = GetComponentInChildren(typeof(ScaleBehaviour)) as ScaleBehaviour;
		m_flashbehaviour = GetComponentInChildren(typeof(FlashBehaviour)) as FlashBehaviour;
		m_grayscale = GetComponentInChildren(typeof(ShaderAmount)) as ShaderAmount;
	}

	void Start()
	{
		Scale().ON( new Vector3( 0.5f, 0.5f, 1f ), 0.35f );
	}

	protected override bool ONCLICK()
	{
		Select();
		return true;
	}

	//캐릭터를 설정하기 위한 함수
	public void Set( tagCharacterStatic characterstatic )
	{
		if( characterstatic==null ) return;

		if( characterstatic.banner!=null )
		{
			m_image.sprite = Library.Sprite(characterstatic.banner.GetTexture());
			m_image.SetNativeSize();

			m_name.text = Language.Get(characterstatic.Name());
		}

		Grayscale();
	}

	void Select()
	{
		if( m_view.IsSelected(this) )
		{
			Scale().ON( new Vector3( 0.5f, 0.5f, 1f ), 0.35f );
			Grayscale().ON(1f);
			m_view.Unselect(this);
		}
		else
		{
			if( m_view.GetSelectCount()>=GAME.CHARACTER_SELECT_MAX )
			{
				SOUND.Failed.ON();
				return;
			}

			Scale().ON( new Vector3( 1f, 1f, 1f ), 0.35f );
			Grayscale().ON(0f);
			m_view.Select(this);

			SOUND.Voice.ON( 0.5f );
		}

		SOUND.CharacterSelect.ON();
		Flash().ON();
	}

	//스케일 객체를 얻기 위한 함수
	ScaleBehaviour Scale()
	{
		return m_scalebehaviour;
	}

	//플래시 객체를 얻기 위한 함수
	FlashBehaviour Flash()
	{
		return m_flashbehaviour;
	}

	//Grayscale 객체를 얻기 위한 함수
	ShaderAmount Grayscale()
	{
		return m_grayscale;
	}
}