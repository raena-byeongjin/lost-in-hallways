﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//-----------------------------------------------------------------------------------------------------------------------------------
// 경영인 정보를 처리하기 위한 클래스
//-----------------------------------------------------------------------------------------------------------------------------------
public class uiPerson : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	[System.Serializable]
	public class tagData
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public object			person			= (null);
#if UNITY_EDITOR
		public string			account_id		= (null);
#endif

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public Transform		transform		= (null);
		public GameObject		gameObject		= (null);
		public objControl		control			= (null);
		public Image			back			= (null);
		public RawImage			image			= (null);
		public objControl		modifier		= (null);
		public Text				name			= (null);
		public Text				level			= (null);
		public uiCompany		company			= (null);
		public uiIcon			nation			= (null);
		public uiAvatar			avatar			= (null);
		public Text				property		= (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	};
	public tagData		data		= new tagData();

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	void Awake()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(data.transform)		= (transform);
		(data.gameObject)		= (gameObject);
		(data.control)			= (GetComponent(typeof(objControl)) as objControl);
		if( Back()==(null) )	(data.back)		= (GetComponent(typeof(Image)) as Image);
		if( Image()==(null) )	(data.image)	= (GetComponent(typeof(RawImage)) as RawImage);
		if( Name()==(null) )	(data.name)		= (GetComponentInChildren(typeof(Text)) as Text);
		if( Company()==(null) )	(data.company)	= (GetComponentInChildren(typeof(uiCompany)) as uiCompany);
		(data.avatar)			= (GetComponentInChildren(typeof(uiAvatar)) as uiAvatar);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 경영인 정보를 설정하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Set( tagPerson person )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (person)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(data.person)	= (person);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(Name().text)	= (person.name);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (data.level)!=(null) )
		{
			SetLevel( person );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// 경영자 이미지
		//---------------------------------------------------------------------------------------------------------------------------
		if( (person.picture)!=(null) && person.picture.IsLoad() && ( (person.avatar)==(null) || !person.avatar.Is() ) )
		{
			(Image().texture)	= person.picture.GetTexture();
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// 기업
		//---------------------------------------------------------------------------------------------------------------------------
		if( Company()!=(null) )
		{
			if( person.Company()!=(null) )
			{
				Company().Set( person.Company() );
			}
			else
			{
				CApp.This.Company.Reserve(person.company as string);
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// 국가
		//---------------------------------------------------------------------------------------------------------------------------
		if( Nation()!=(null) )
		{
			Nation().Set( person.nation );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( Company()!=(null) )
		{
			if( (person.company)==(null) )
			{
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				Func.Inactive( Company()._GameObject() );

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				RectTransform info = (Func.Get( Transform(), "Info" ) as RectTransform);
				if( (info)!=(null) )
				{
					(info.anchoredPosition)	+= new Vector2( (-138), (0) );
				}

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( Avatar()!=(null) )
		{
			if( (person.avatar)!=(null) && person.avatar.Is() )
			{
				Avatar().Set( person.avatar );
				Func.Active( Avatar()._GameObject() );
			}
			else
			{
				(Image().texture)	= (Resources.Load(PATH.DEFAULT_PROFILE_IMAGE) as Texture2D);
				(Image().color)		= (Color.white);
				Func.Inactive( Avatar()._GameObject() );
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 경영인 정보를 설정하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void SetLevel( tagPerson person )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (person)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		bool	isOwner		= CApp.This.Company.IsOwner( person.Company(), (person) );
		bool	isManager	= CApp.This.Company.IsManager( person.Company(), (person) );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (isOwner) && (isManager) )
		{
			(data.level.text)	= CApp.This.Language.Get(TEXT.회장_및_최고_경영자);
		}
		else
		if( isOwner )
		{
			(data.level.text)	= CApp.This.Language.Get(TEXT.회장);
		}
		else
		if( isManager )
		{
			(data.level.text)	= CApp.This.Language.Get(TEXT.최고_경영자);
		}
		else
		{
			(data.level.text)	= CApp.This.Language.Get(TEXT.전문_경영인);
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 경영인 정보를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public tagPerson Get()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return CApp.This.Person.Get( ref data.person );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 트랜스폼 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public Transform Transform()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.transform);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 게임 오브젝트를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public GameObject _GameObject()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.gameObject);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 컨트롤 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public objControl Control()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.control);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 배경 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public Image Back()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.back);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이름 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public Text Name()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.name);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이미지 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public RawImage Image()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.image);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 기업 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public uiCompany Company()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.company);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 국가 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public uiIcon Nation()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.nation);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

#if UNITY_EDITOR
	//-------------------------------------------------------------------------------------------------------------------------------
	// ID를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public string GetAccountID()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.account_id);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}
#endif

	//-------------------------------------------------------------------------------------------------------------------------------
	// 수정 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public objControl Modifier()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.modifier);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 아바타 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public uiAvatar Avatar()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.avatar);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 자산 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public Text Property()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.property);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
}

//-----------------------------------------------------------------------------------------------------------------------------------
// -
//-----------------------------------------------------------------------------------------------------------------------------------