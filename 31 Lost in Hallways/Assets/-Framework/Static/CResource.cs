using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

//리소스 정보를 처리하기 위한 클래스
public class CResource : AppsItemListener
{
	private List<tagResource> Resources = new List<tagResource>();

	protected override void Awake()
	{
		base.Awake();
		Initialize( "Resources", UPDATE_CLASS.Resource.ToString() );
	}

    //객체를 생성하기 위한 함수 
    protected override tagAppsItem New()
    {
        return new tagResource();
    }

	//객체를 삭제하기 위한 함수 
	protected override void OnDelete( tagAppsItem appsitem )
	{
		if( appsitem==null ) return;
		GetList().Remove( appsitem as tagResource );
    }

	//리스트를 얻기 위한 함수
	public List<tagResource> GetList()
	{
		return Resources;
	}

	//객체를 검색하기 위한 함수
    public tagResource Find( string id )
    {
        if( !Library.Is(id) ) return null;

        foreach( tagResource resource in GetList() )
        {
            if( resource.id==id )
            {
				return resource;
            }
        }

		return null;
    }

	//리소스를 불러오기 위한 함수
	public object Load( tagResource resource, string src, System.Type type )
	{
		if( resource==null ) return null;
		if( !Library.Is(src) ) return null;
//		if( type==null ) return null;		//(NULL)값을 허용함

		if( Library.Is(src) && resource.AssetBundle().bundle!=null )
		{
			return resource.AssetBundle().bundle.LoadAsset( src, type );
		}

		return null;
	}

	//리소스를 불러오기 위한 함수
	public object Load( string resource, string src, System.Type type=null )
	{
		if( !Library.Is(resource) ) return null;
		if( !Library.Is(src) ) return null;
//		if( type==null ) return null; //(NULL)값을 허용함

		return Load( Find(resource), src, type );
	}

	//로컬에 저장된 정보를 불러오기 위한 함수
	public override void OnLoadCache( tagAppsItem appsitem, AppsParameter col=null )
	{
		if( appsitem==null ) return;
//		if( col==null ) return;	//(NULL)값을 허용함

		tagResource resource = appsitem as tagResource;
		if( resource==null ) return;

		if( col!=null )
		{
		}

		if( !GetList().Contains(resource) )
		{
			GetList().Add(resource);
		}
	}

	//XML 파일을 저장하기 위한 함수
	public override void OnXmlSave( XmlTextWriter xmlWriter, tagAppsItem appsitem )
	{
		if( xmlWriter==null ) return;
		if( appsitem==null ) return;

		tagResource resource = appsitem as tagResource;
		if( resource==null ) return;
	}

	//XML 파일을 불러오기 위한 함수
	public override void OnXmlLoad( XmlNode pNode, tagAppsItem appsitem )
	{
		if( pNode==null ) return;
		if( appsitem==null ) return;

		tagResource resource = appsitem as tagResource;
		if( resource==null ) return;
	}
}