﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------------------------------------------------------------------------------
// 도시 정보를 처리하기 위한 클래스
//-----------------------------------------------------------------------------------------------------------------------------------
public class CCity : MonoBehaviour
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
		public tagCityStatic	city		= (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	};
	public tagData		data		= new tagData();

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	protected CApp		app			= (null);
	protected CPlay		play		= (null);

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	void OnEnable()
	{
		(this.app)		= (CApp.This);
		(this.play)		= (CPlay.This);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 도시를 설정하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Set( tagCityStatic citystatic )
	{
	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( (citystatic)==(null) ) return;

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		(data.city)		= (citystatic);

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		app.ViewTop.Set( citystatic );

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 도시를 이동하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Transfer( tagCityStatic citystatic )
	{
	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( (citystatic)==(null) ) return;

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		app.ViewIndigator.ON( (app.City), Func.Script( app.Language.Get(TEXT.__로_이동중입니다), app.Language.Get(app.City.GetName(citystatic)) ) );
//		LoveSignalHash.Transfer( citystatic );

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 현재 도시를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public tagCityStatic Get()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return (data.city);

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 현재 도시를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public string GetName( tagCityStatic citystatic )
	{
	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( (citystatic)==(null) ) return (null);

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return app.Language.Get(citystatic.Name());
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 리스트를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public List<tagCityStatic> GetList()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return app.CityStatic.GetList();

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 도시를 검색하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public tagCityStatic Find( string id )
	{
	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( !Library.Is(id) ) return (null);

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		foreach( tagCityStatic citystatic in GetList() )
		{
			if( (citystatic.id)==(id) )
			{
				return (citystatic);
			}
		}

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		return (null);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 이동할 수 있는지 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsTransfer( tagCityStatic citystatic )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (citystatic)==(null) ) return false;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
#if UNITY_EDITOR
		if( citystatic.openmarket )
		{
			return true;
		}
#endif

		/*
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( citystatic.Nation()==app.Identity.Nation() || citystatic.Nation()==app.Player.Nation() )
		{
			return true;
		}
		*/

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 현재 도시인지 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool Is( tagCityStatic citystatic )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (citystatic)==(null) ) return false;

		/*
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( app.Player.City()==(citystatic) )
		{
			return true;
		}
		*/

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
}

//-----------------------------------------------------------------------------------------------------------------------------------
// -
//-----------------------------------------------------------------------------------------------------------------------------------