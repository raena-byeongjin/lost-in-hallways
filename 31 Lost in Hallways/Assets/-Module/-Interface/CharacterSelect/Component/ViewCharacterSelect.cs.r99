using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCharacterSelect : ViewBehaviour
{
	private List<ButtonCharacterSelect> Characters = new List<ButtonCharacterSelect>();
	private List<ButtonCharacterSelect> Selects = new List<ButtonCharacterSelect>();

	//인터페이스를 활성화 하기 위한 함수
	public static void ON()
	{
		MainUI.Create( "CharacterSelect" );
	}

	protected override void Awake()
	{
		base.Awake();

		Component[] comArray = GetComponentsInChildren(typeof(ButtonCharacterSelect));
		foreach( ButtonCharacterSelect characterselect in comArray )
		{
			GetList().Add(characterselect);
		}
	}

	void Start()
	{
		int i = 0;
		foreach( tagCharacterStatic characterstatic in app.CharacterStatic.GetList() )
		{
			if( i<GetList().Count )
			{
				characterstatic.Download( funcDownloadEnd, GetList()[i] );
			}

			i += 1;
		}

		SOUND.CharacterSelectStart.ON();
	}

	//리스트를 얻기 위한 함수
	List<ButtonCharacterSelect> GetList()
	{
		return Characters;
	}

	void funcDownloadEnd( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam as ButtonCharacterSelect==null ) return;
		if( lParam==null || lParam as tagCharacterStatic==null ) return;

		ButtonCharacterSelect characterselect = wParam as ButtonCharacterSelect;
		tagCharacterStatic characterstatic = lParam as tagCharacterStatic;

		characterselect.Set(characterstatic);
	}

	//캐릭터 선택 취소를 처리하기 위한 함수
	public int GetSelectCount()
	{
		return Selects.Count;
	}

	//캐릭터 선택 취소를 처리하기 위한 함수
	public void Unselect( ButtonCharacterSelect characterselect )
	{
		if( characterselect==null ) return;
		Selects.Remove(characterselect);
	}

	//캐릭터 선택을 처리하기 위한 함수
	public void Select( ButtonCharacterSelect characterselect )
	{
		if( characterselect==null ) return;

		if( !Selects.Contains(characterselect) )
		{
			Selects.Add(characterselect);
		}
	}

	//캐릭터가 선택되어 있는지 확인하기 위한 함수
	public bool IsSelected( ButtonCharacterSelect characterselect )
	{
		if( characterselect==null ) return false;

		if( Selects.Contains(characterselect) )
		{
			return true;
		}

		return false;
	}
}
