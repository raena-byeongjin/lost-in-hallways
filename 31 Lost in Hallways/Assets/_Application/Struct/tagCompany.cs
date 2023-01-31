﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------------------------------------------------------------------------------
// 회사 정보를 처리하기 위한 구조체
//-----------------------------------------------------------------------------------------------------------------------------------
[System.Serializable]
public class tagCompany
{
	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public string			id			= (null);
	public string			name		= (null);

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public tagNationStatic	nation		= (null);
	public tagTexture		picture		= (null);
	public tagIdentity		owner		= (null);	//소유주
	public object			manager		= (null);	//CEO
	public int				since		= (0);		//창립이래
	public Color			color		= new Color();

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public List<tagCash>	Cashes		= new List<tagCash>();
	public tagStock			stock		= new tagStock();

	//-------------------------------------------------------------------------------------------------------------------------------
	// CEO 정보를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public tagPerson Manager()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return CApp.This.Person.Get( ref manager );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public override string ToString()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (name)+" : "+(id)+" ("+base.ToString()+")";

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