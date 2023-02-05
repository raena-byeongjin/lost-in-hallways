using UnityEngine.EventSystems;
using UnityEngine;

public class CharacterBehaviour : TransformBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Collider m_collider = null;
	private Outline m_outline = null;
	private StickCharacterInfo m_stick = null;

	private float fHeight = 0f;

	protected override void Awake()
	{
		base.Awake();

		m_collider = GetComponentInChildren(typeof(Collider)) as Collider;
		if( m_collider==null )
		{
			m_collider = _GameObject().AddComponent(typeof(CapsuleCollider)) as Collider;
		}

		if( m_collider!=null )
		{
			fHeight = m_collider.bounds.size.y * Transform().localScale.y;
		}

		m_outline = _GameObject().AddComponent(typeof(Outline)) as Outline;
		if( m_outline!=null )
		{
			m_outline.OutlineColor = Library.Color( "FFB100", 0.15f );
			m_outline.OutlineMode = global::Outline.Mode.OutlineVisible;
			MessageQueue.Skip( funcOutline, false, null, 0f, 5 );
		}
	}

	//캐릭터 정보를 설정하기 위한 함수
	public void Set( tagCharacterStatic characterstatic )
	{
		if( characterstatic==null ) return;

		m_stick = ViewStick.ON( "CharacterInfo", Transform(), Height()+0.35f ).GetComponentInChildren(typeof(StickCharacterInfo)) as StickCharacterInfo;
		if( m_stick!=null )
		{
			m_stick.Set( characterstatic );
		}
	}

	public void OnPointerEnter( PointerEventData pointer )
	{
		if( pointer==null ) return;
		ApplicationBehaviour.This.Character.Rollover(this);
	}

	public void OnPointerExit( PointerEventData pointer )
	{
		if( pointer==null ) return;
		ApplicationBehaviour.This.Character.Rollout(this);
	}

	public void Outline( bool option )
	{
		m_outline.enabled = option;

		if( option )
		{
			Library.Active(m_stick._GameObject());
		}
		else
		{
			Library.Inactive(m_stick._GameObject());
		}
	}

	void funcOutline( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(bool) ) return;
		Outline( (bool)wParam );
	}

	float Height()
	{
		return fHeight;
	}
}