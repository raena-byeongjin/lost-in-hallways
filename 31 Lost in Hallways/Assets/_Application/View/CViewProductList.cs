﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

//-----------------------------------------------------------------------------------------------------------------------------------
// 상품 리스트를 처리하기 위한 클래스
//-----------------------------------------------------------------------------------------------------------------------------------
public class CViewProductList : ViewPanel
{
	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	[System.Serializable]
	public class tagData : _tagData
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public tagProductStatic		productstatic		= (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public List<uiProduct>		Products			= new List<uiProduct>();
		public IListInterface		listInterface		= (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public int					listPage			= (0);
		public bool					allowListExpand		= (false);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	};
	public tagData		data		= new tagData();

	//-------------------------------------------------------------------------------------------------------------------------------
	// 인터페이스를 활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool ON( tagProductStatic productstatic, IListInterface listInterface )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
//		if( (productstatic)==(null) ) return;		//(NULL)값을 허용함
//		if( (productListCheck)==(null) ) return;	//(NULL)값을 허용함

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(data.productstatic)	= (productstatic);
		(data.listInterface)	= (listInterface);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( base.ON(data) )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			ForeBack();
			Indigator( app.Language.Get(TEXT.상품_정보를_불러오는_중입니다) );

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			(data.listPage)			= (0);
			(data.allowListExpand)	= (true);

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			LoadList( data.listPage );

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}
		else
		{
			ForeBack();
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return base.ON();
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 인터페이스를 활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public override bool ON()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return ON( (null), (null) );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 인터페이스를 비활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public override bool OFF()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( Is(true) )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			(data.listInterface) = (null);

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			GetList().Clear();

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			return base.OFF();
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 상품 정보를 등록하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Register( tagProductStatic productstatic, AppsParameter col )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (productstatic)==(null) ) return;
		if( (col)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		uiProduct icon = FindFromID(col.Get("id"));
		if( (icon)==(null) )
		{
			(icon) = (Func.Create( ScrollView().Content(), (Resources.Load(PATH.UI_PRODUCT_LIST_ITEM) as GameObject) ).GetComponent(typeof(uiProduct)) as uiProduct);
			if( (icon)==(null) ) return;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		icon.Set( (productstatic), (col) );
		if( icon.Business()!=(null) && app.Business.Is(icon.Business().Get()) )
		{
			if( icon.City()!=(null) )	icon.City().Set( icon.Business().Get().citystatic );
			if( icon.Nation()!=(null) ) icon.Nation().Set( icon.Business().Get().citystatic.Nation() );
		}
		else
		{
			if( icon.City()!=(null) )	Func.Inactive( icon.City()._GameObject() );
			if( icon.Nation()!=(null) ) Func.Inactive( icon.Nation()._GameObject() );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( true )
		{
			string		business_id = col.Get("business");
			tagBusiness business	= app.Business.Find(business_id);
			if( (business)!=(null) )
			{
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				if( icon.Business()!=(null) )
				{
					icon.Business().Set( business );
				}

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				if( icon.Company()!=(null) )
				{
					if( business.Company()!=(null) )
					{
						icon.Company().Set( business.Company() );
					}
					else
					{
//						Debug.Log("(1) => "+(business_id));
						(icon.Business().data.business) = (business);
						app.Company.Reserve( business.company as string );
					}
				}

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
			}
			else
			if( icon.Business()!=(null) )
			{
//				Debug.Log("(2) => "+(business_id));
				(icon.Business().data.business) = (business_id);
				app.Business.Reserve( business_id );
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// 그래프 : 순이익
		//---------------------------------------------------------------------------------------------------------------------------
		icon.Graph().Set( (IDENTITY.PRODUCT), col.Get("id") );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( icon.Excute()!=(null) && (data.listInterface)==(null) )
		{
			icon.Excute().OFF();
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
//		Func.TextWidthPerfect(icon.City().Text());
		Func.TextWidthPerfect(icon.Text());

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		GetList().Add( icon );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (data.listInterface)!=(null) )
		{
			data.listInterface.IList_OnList( (this), (icon) );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 사업체 정보를 업데이트하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void update( tagBusiness business )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (business)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( uiProduct icon in GetList() )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( icon.Business()!=(null) && ( icon.Business().Get()==(business) || (icon.Business().data.business as string)==(business.id) ) )
			{
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				if( business.Company()!=(null) )
				{
					icon.Company().Set( business.Company() );
				}
				else
				if( (business.company)!=(null) && business.company.GetType()==typeof(string) )
				{
					(icon.Company().data.company)	= (business.company);
					app.Company.Reserve(business.company as string);
				}
				else
				{
					Debug.Log(business);
				}

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				if( icon.Business()!=(null) )
				{
					//---------------------------------------------------------------------------------------------------------------
					// -
					//---------------------------------------------------------------------------------------------------------------
					icon.Business().Set( business );

					//---------------------------------------------------------------------------------------------------------------
					// -
					//---------------------------------------------------------------------------------------------------------------
					if( icon.City()!=(null) )
					{
						icon.City().Set( business.citystatic );
						Func.Active( icon.City()._GameObject() );
					}

					//---------------------------------------------------------------------------------------------------------------
					// -
					//---------------------------------------------------------------------------------------------------------------
					if( icon.Nation()!=(null) )
					{
						icon.Nation().Set( business.citystatic.Nation() );
						Func.Active( icon.Nation()._GameObject() );
					}

					//---------------------------------------------------------------------------------------------------------------
					// -
					//---------------------------------------------------------------------------------------------------------------
				}

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 사업체 정보를 업데이트하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void update( tagCompany company )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (company)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( uiProduct icon in GetList() )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( icon.Company()==(null) ) continue;

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( icon.Company().Get()==(company) || ( icon.Company().Business()!=(null) && icon.Company().Business().Company()==(company) ) || (icon.Company().data.company as string)==(company.id) )
			{
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				icon.Company().Set( company );

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 리스트를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public List<uiProduct> GetList()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.Products);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 리스트를 새로고침 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Refresh()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		ScrollView().Init();

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		int	i = (0);
		int nPattern = (0);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (ScrollView().Content().sizeDelta.y)<ScrollView().Height() )
		{
			(nPattern)	= (GetList().Count)%(2);
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( uiProduct icon in GetList() )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( (i+nPattern)%(2)==(0) )
			{
				(icon.Back().color)	= Func.Color( (32), (46), (69) );
			}
			else
			{
				(icon.Back().color)	= Func.Color( (24), (34), (52) );
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			(i)	++;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 상품을 검색하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public uiProduct FindFromID( string id )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( !Library.Is(id) ) return (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( uiProduct icon in GetList() )
		{
			if( icon.ID()==(id) )
			{
				return (icon);
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (null);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	void LoadList( int page )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (data.listInterface)!=(null) )
		{
			data.listInterface.IList_LoadList( (page), (data.productstatic) );
		}
		else
		{
//			LoveSignalHash.LoadProductList( (page), (data.productstatic) );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 스크롤 입력을 처리하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public override bool ONSCROLLCHANGE( objScrollView scrollView, Vector2 value )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (scrollView)==(null) ) return false;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (data.allowListExpand) && (value.y)<=(0) )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			Indigator( app.Language.Get(TEXT.상품_정보를_불러오는_중입니다) );
			LoadList( data.listPage );

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			(data.allowListExpand) = (false);

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return base.ONSCROLLCHANGE( (scrollView), (value) );
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 취소 입력에 반응하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public override bool ONCANCEL( CANCEL Cancel )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( Is() )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			GetList().Clear();

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			return base.ONCANCEL( Cancel );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 컨트롤 입력에 반응하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public override bool ONCONTROL( objControl control, CONTROL_ACTION Action, tagFocus focus )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (control)==(null) ) return false;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( uiProduct icon in GetList() )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( (control)==icon.Icon() && (Action)==(CONTROL_ACTION.SELECT) )
			{
//				app.ViewProduct.ON( icon.Get(), icon.ID() );
				app.ViewBusiness.ON( icon.Business().Get() );
				return true;
			}
			else
			if( icon.Business()!=(null) && (control)==icon.Business().Control() && (Action)==(CONTROL_ACTION.SELECT) )
			{
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				app.ViewBusiness.ON( icon.Business().Get() );

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				return true;
			}
			else
			if( icon.Company()!=(null) && (control)==icon.Company().Control() && (Action)==(CONTROL_ACTION.SELECT) )
			{
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				app.ViewCompany.ON( icon.Company().Get() );

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				return true;
			}
			else
			if( ( (control)==icon.Control() || (control)==icon.Excute() )&& (Action)==(CONTROL_ACTION.SELECT) )
			{
				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				if( (data.listInterface)!=(null) )
				{
					data.listInterface.IList_Select( (this), (icon) );
				}
				else
				{
					app.ViewBusiness.ON( icon.Business().Get() );
				}

				//-------------------------------------------------------------------------------------------------------------------
				// -
				//-------------------------------------------------------------------------------------------------------------------
				return true;
			}
			else
			if( (control)==icon.Graph().Control() && (Action)==(CONTROL_ACTION.SELECT) )
			{
				app.ViewProduct.ON( icon.Get(), icon.ID() );
				return true;
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 메모리를 초기화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public override void Unloader()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 인터페이스를 불러오기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public override void Loader()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		base.Loader( Resources.Load("ApplicationLoader/View/ViewGenericList"), (play.uiCanvas) );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Text subject = (Func.Get( Transform(), "Subject" ).GetComponentInChildren(typeof(Text)) as Text);
		if( (subject)!=(null) )
		{
			(subject.text)	= app.Language.Get(TEXT.상품);
		}

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