using System.Collections.Generic;
using System.Xml;

//이펙트 정보를 처리하기 위한 클래스
public class CEffectStatic : AppsItemListener
{
	private List<tagEffectStatic> Effects = new List<tagEffectStatic>();

	protected override void Awake()
	{
		base.Awake();
		Initialize( "Effects", UPDATE_CLASS.Effect.ToString() );
	}

	public override void OnInitialize()
	{
		EFFECT.Initialize();
	}

    //객체를 생성하기 위한 함수 
    protected override tagAppsItem New()
    {
        return new tagEffectStatic();
    }

	//리소스를 삭제하기 위한 함수 
	protected override void OnDelete( tagAppsItem appsItem )
	{
		if( appsItem==null ) return;
		GetList().Remove( appsItem as tagEffectStatic );
    }

	//리스트를 얻기 위한 함수
	public List<tagEffectStatic> GetList()
	{
		return Effects;
	}

	//객체를 검색하기 위한 함수
    public tagEffectStatic Find( string id )
    {
        if( !Library.Is(id) ) return null;

        foreach( tagEffectStatic effect in GetList() )
        {
            if( effect.id==id )
            {
				return effect;
            }
        }

		return null;
    }

	//로컬에 저장된 정보를 불러오기 위한 함수
	public override void OnLoadCache( tagAppsItem appsItem, AppsParameter col=null )
	{
		if( appsItem==null ) return;
//		if( col==null ) return;	//(NULL)값을 허용함

		tagEffectStatic effect = appsItem as tagEffectStatic;
		if( effect==null ) return;

		if( col!=null )
		{
		}

		if( !GetList().Contains(effect) )
		{
			GetList().Add(effect);
		}
	}

	//XML 파일을 저장하기 위한 함수
	public override void OnXmlSave( XmlTextWriter xmlWriter, tagAppsItem appsItem )
	{
		if( xmlWriter==null ) return;
		if( appsItem==null ) return;

		tagEffectStatic effect = appsItem as tagEffectStatic;
		if( effect==null ) return;

		/*
		CXml.WriteNodeValue( (xmlWriter), "position",		(stage.vPos) );
		CXml.WriteNodeValue( (xmlWriter), "prev",			(stage.prev) );
		CXml.WriteNodeValue( (xmlWriter), "event_start",	(stage.event_start) );
		CXml.WriteNodeValue( (xmlWriter), "event_end",		(stage.event_end) );
		CXml.WriteNodeValue( (xmlWriter), "road_texture",	(stage.road_texture) );
		CXml.WriteNodeValue( (xmlWriter), "road_mask",		(stage.road_mask) );
		CXml.WriteNodeValue( (xmlWriter), "road_position",	(stage.road_position) );
		*/
	}

	//XML 파일을 불러오기 위한 함수
	public override void OnXmlLoad( XmlNode pNode, tagAppsItem appsItem )
	{
		if( pNode==null ) return;
		if( appsItem==null ) return;

		tagEffectStatic effect = appsItem as tagEffectStatic;
		if( effect==null ) return;

		/*
		(stage.vPos)			= CXml.GetChildValueVector2( (pNode), "position" );
		(stage.prev)			= CXml.GetChildValue( (pNode), "prev" );
		(stage.event_start)		= CXml.GetChildValue( (pNode), "event_start" );
		(stage.event_end)		= CXml.GetChildValue( (pNode), "event_end" );
		CXml.GetChildValue( (pNode), (stage.road_texture), "road_texture" );
		CXml.GetChildValue( (pNode), (stage.road_mask), "road_mask" );
		(stage.road_position)	= CXml.GetChildValueVector2( (pNode), "road_position" );
		*/
	}
}