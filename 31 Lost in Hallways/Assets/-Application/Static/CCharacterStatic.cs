using System.Collections.Generic;
using System.Xml;
using System;

//식재료 정보를 처리하기 위한 클래스
public class CCharacterStatic : AppsItemListener
{
	public List<tagCharacterStatic> Characters = new List<tagCharacterStatic>();

	protected override void Awake()
	{
		base.Awake();
		Initialize( "Characters", UPDATE_CLASS.Character.ToString() );
	}

    //객체를 생성하기 위한 함수 
    protected override tagAppsItem New()
    {
        return new tagCharacterStatic();
    }

	//객체를 삭제하기 위한 함수 
	protected override void OnDelete( tagAppsItem appsitem )
	{
		if( appsitem==null ) return;
		GetList().Remove( appsitem as tagCharacterStatic );
    }

	//리스트를 얻기 위한 함수
	public List<tagCharacterStatic> GetList()
	{
		return Characters;
	}

	//모델 정보를 검색하기 위한 함수
    public tagCharacterStatic Find( string id )
    {
		if( !Library.Is(id) ) return null;

		foreach( tagCharacterStatic characterstatic in GetList() )
		{
			if( characterstatic.id==id )
			{
				return characterstatic;
			}
        }

		return null;
    }

	//리소스를 다운로드하기 위한 함수
	public override void Download( tagAppsItem appsitem, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( appsitem==null ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		base.Download( appsitem, func, wParam, lParam );

		tagCharacterStatic characterstatic = appsitem as tagCharacterStatic;
		if( characterstatic==null ) return;

		if( characterstatic.banner!=null )
		{
			characterstatic.banner.Download();
		}
	}

	//로컬에 저장된 정보를 불러오기 위한 함수
	public override void OnLoadCache( tagAppsItem appsitem, AppsParameter col=null )
	{
		if( appsitem==null ) return;
//		if( col==null ) return; //(NULL)값을 허용함

		tagCharacterStatic characterstatic = appsitem as tagCharacterStatic;
		if( characterstatic==null ) return;

		if( col!=null )
		{
			characterstatic.banner = AppsFunc.Parse( characterstatic.banner, col.Get("banner") );
		}

		if( !GetList().Contains(characterstatic) )
		{
			GetList().Add(characterstatic);
		}
	}

	//XML 파일을 저장하기 위한 함수
	public override void OnXmlSave( XmlTextWriter xmlWriter, tagAppsItem appsitem )
	{
  		if( xmlWriter==null ) return;
		if( appsitem==null ) return;

		tagCharacterStatic characterstatic = appsitem as tagCharacterStatic;
		if( characterstatic==null ) return;

		CXml.WriteNodeValue( xmlWriter, "banner", characterstatic.banner );
	}

	//XML 파일을 불러오기 위한 함수
	public override void OnXmlLoad( XmlNode pNode, tagAppsItem appsitem )
	{
 		if( pNode==null ) return;
		if( appsitem==null ) return;

		tagCharacterStatic characterstatic = appsitem as tagCharacterStatic;
		if( characterstatic==null ) return;

		characterstatic.banner = CXml.GetChildValue( pNode, characterstatic.banner, "banner" );
	}
}