using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using UnityEngine;

//아이템의 정보를 처리하기 위한 클래스
public class AppsItemListener : FrameworkBehaviour
{
	private string PathName = null;
	private string ClassName = null;
	private string TypeName = null;

	private List<tagAppsItem> AppsItems = new List<tagAppsItem>();

	//클래스를 초기화 하기 위한 함수
	protected void Initialize( string pathName, string className, string typeName=null )
	{
		if( !Library.Is(pathName) ) return;
		if( !Library.Is(className) ) return;
//		if( !Library.Is(typeName) ) return; //(NULL)값을 허용함

		PathName = pathName;
		ClassName = className;
		TypeName = typeName;

		if( !Apps.GetAppsItemListeners().Contains(this) )
		{
			Apps.AddListener( this );
		}
	}

	public virtual void OnInitialize()
	{
	}

	//캐시 데이타로부터 아이템 정보를 불러오기 위한 함수
	public void LoadCaches()
	{
		if( !Library.IsDir(GetPath()) ) return;

		string[] Directorys = Directory.GetDirectories(GetPath());
		if( Directorys==null ) return;

		string filepath = null;

		foreach( string directory in Directorys )
		{
#if UNITY_EDITOR && !ENCODE_CACHE
			filepath = directory+"/content.xml";
#else
			filepath = directory+"/content.data";
#endif

			if( Library.IsFile(filepath) )
			{
				LoadCache( filepath );
			}
#if UNITY_EDITOR
			else
			{
				Debug.Log("데이타 파일이 존재하지 않음. 서버로부터 업데이트가 필요함 : "+filepath);
			}
#endif
		}
	}

	//아이템 정보를 불러오기 위한 함수
	void LoadCache( string filepath )
	{
		if( !Library.Is(filepath) ) return;

		tagAppsItem appsitem = XmlLoad(filepath);
		if( appsitem!=null )
		{
			tagAppsItem find = FindFromId(appsitem.id);
			if( find!=null )
			{
#if UNITY_EDITOR
				Debug.LogWarning( "중복된 로드: "+appsitem );
#endif
			}
			else
			{
				Register( appsitem );
				OnLoadCache( appsitem );
			}
		}
	}

	//아이템 정보를 처리하기 위한 함수
	public void onItemState( AppsParameter col )
	{
		if( col==null ) return;

		tagAppsItem appsitem = FindFromId( col.Get("id") );
		bool deleted = col.GetBoolean("deleted");

		if( deleted )
		{
			Delete( appsitem );
		}
		else
		if( appsitem!=null )
		{
			update( appsitem, col );
		}
		else
		{
			appsitem = Register( col );
		}

		if( !deleted )
		{
			XmlSave( appsitem );
		}

#if UNITY_EDITOR
		if( !deleted && appsitem!=null && appsitem.IsResourceUpdate() )
		{
			Download( appsitem );
		}
#endif
	}

	//아이템을 검색하기 위한 함수
	public tagAppsItem FindFromId( string id )
	{
		if( !Library.Is(id) ) return null;

		foreach( tagAppsItem appsitem in GetAppsItems() )
		{
			if( appsitem.id==id )
			{
				return appsitem;
			}
		}

		return null;
	}

	//아이템을 검색하기 위한 함수
	public tagAppsItem FindFromTag( string tag )
	{
		if( !Library.Is(tag) ) return null;

		foreach( tagAppsItem appsitem in GetAppsItems() )
		{
			if( appsitem.tag==tag )
			{
				return appsitem;
			}
		}

		return null;
	}

	//아이템을 검색하기 위한 함수
	public tagAppsItem FindFromIndex( string index )
	{
		if( !Library.Is(index) ) return null;

		foreach( tagAppsItem appsitem in GetAppsItems() )
		{
			if( appsitem.index==index )
			{
				return appsitem;
			}
		}

		return null;
	}

	//아이템을 검색하기 위한 함수
	public tagAppsItem FindFromAlias( string alias )
	{
		if( !Library.Is(alias) ) return null;

		foreach( tagAppsItem appsitem in GetAppsItems() )
		{
			if( appsitem.alias==alias )
			{
				return appsitem;
			}
		}

		return null;
	}

	//아이템을 검색하기 위한 함수
	public tagAppsItem FindFromName( string name )
	{
		if( !Library.Is(name) ) return null;

		foreach( tagAppsItem appsitem in GetAppsItems() )
		{
			if( appsitem.name==name )
			{
				return appsitem;
			}
		}

		return null;
	}

	//아이템 정보를 등록하기 위한 함수
	tagAppsItem Register( AppsParameter col )
	{
		if( col==null ) return null;

		tagAppsItem appsitem = New();
		if( appsitem!=null )
		{
			appsitem.id = col.Get("id");
		}

		update( appsitem, col );
		Register( appsitem );

		return appsitem;
	}

	//아이템을 등록하기 위한 함수
	void Register( tagAppsItem appsitem )
	{
		if( appsitem==null ) return;

		appsitem.eventListener = this;
		GetAppsItems().Add( appsitem );
	}

	//아이템 정보를 삭제하기 위한 함수
	void Delete( tagAppsItem appsitem )
	{
		if( appsitem==null ) return;

		OnDelete( appsitem );
		Library.Delete( GetPath(appsitem.id) );

		GetAppsItems().Remove( appsitem );

		if( appsitem.AssetBundle()!=null )
		{
			if( Platform.IsEditor() )
			{
				Debug.Log( "(StreamingAsset-Delete) "+Library.GetFileName(appsitem.AssetBundle().url)+".asset" );
				Library.Delete( CEngine.GetStreamingPath( (appsitem.AssetBundle().url), (false) ) );
			}

			if( Library.IsFile( CEngine.GetCachePath(appsitem.AssetBundle().url) ) )
			{
#if UNITY_EDITOR
				Debug.Log( "(Cache-Delete) "+CEngine.GetCachePath(appsitem.AssetBundle().url) );
#endif
				Library.Delete( CEngine.GetCachePath(appsitem.AssetBundle().url) );
			}
		}

		if( appsitem.CorverTexture!=null )
		{
			if( Platform.IsEditor() && Library.IsFile( CEngine.GetStreamingPath( appsitem.CorverTexture.url, false ) ) )
			{
				Debug.Log( "(StreamingAsset-Delete) "+Library.GetFileName(appsitem.CorverTexture.url) );
				Library.Delete( CEngine.GetStreamingPath( appsitem.CorverTexture.url, false ) );
			}

			if( Library.IsFile( CEngine.GetCachePath(appsitem.CorverTexture.url) ) )
			{
#if UNITY_EDITOR
				Debug.Log( "(Cache-Delete) "+CEngine.GetCachePath(appsitem.CorverTexture.url) );
#endif
				Library.Delete( CEngine.GetCachePath(appsitem.CorverTexture.url) );
			}
		}

		appsitem.AssetBundle().Unload();
	}

	//아이템 정보를 삭제하기 위한 함수
	protected virtual void OnDelete( tagAppsItem appsitem )
	{
	}

	//경로 이름을 얻기 위한 함수
	private string GetPathName()
	{
		return PathName;
	}

	//클래스 이름을 얻기 위한 함수
	public string GetClassName()
	{
		return ClassName;
	}

	//타입 이름을 얻기 위한 함수
	public string GetTypeName()
	{
		return TypeName;
	}

	//캐시 데이타의 경로를 얻기 위한 함수
	public string GetPath()
	{
		return Application.persistentDataPath+"/"+GetPathName();
	}

	//아이템 데이타의 경로를 얻기 위한 함수
	public string GetPath( string id )
	{
		if( !Library.Is(id) ) return null;
		return GetPath()+"/"+id;
	}

	//객체를 생성하기 위한 함수 
	protected virtual tagAppsItem New()
	{
		return new tagAppsItem();
	}

	//아이템 리스트를 얻기 위한 함수
	public List<tagAppsItem> GetAppsItems()
	{
		return AppsItems;
	}

	//로컬 정보를 불러오기 위한 함수
	public virtual void OnLoadCache( tagAppsItem appsitem, AppsParameter col=null )
	{
	}

	//XML 파일을 저장하기 위한 함수
	public virtual void OnXmlSave( XmlTextWriter xmlWriter, tagAppsItem appsitem )
	{
	}

	//XML 파일을 불러오기 위한 함수
	public virtual void OnXmlLoad( XmlNode pNode, tagAppsItem appsitem )
	{
	}

	//이름을 얻기 위한 함수
	public string GetRawName( tagAppsItem appsitem )
	{
		if( appsitem==null ) return "(NOTHING)";
		return Library.CH( appsitem.name, appsitem.displayName );
	}

	//리소스를 다운로드하기 위한 함수
	public virtual void Download( tagAppsItem appsitem, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( appsitem==null ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( lParam==null )
		{
			lParam = appsitem;
		}
		else
		if( wParam==null )
		{
			wParam = appsitem;
		}

		if( appsitem.AssetBundle()!=null && appsitem.AssetBundle().Is() && ( !Library.IsAssetBundle(appsitem.AssetBundle()) || appsitem.AssetBundle().IsVersion() ) && !appsitem.AssetBundle().IsLoad() )
		{
			tagDownload download = app.Download.ON( appsitem.Name(), appsitem.AssetBundle(), funcDownloadAsset, appsitem );
			if( download!=null && ( appsitem.Update || ( appsitem.AssetBundle()!=null && appsitem.AssetBundle().Update ) ) )
			{
				//온라인에서 업데이트된 것이기 때문에,
				//스트리밍 다운로드를 사용하지 않음
				download.allowStreaming = false;
			}
		}

		if( appsitem.CorverTexture!=null && appsitem.CorverTexture.Is() && !appsitem.CorverTexture.IsLoad() )
		{
			tagDownload download = app.Download.ON( appsitem.Name(), appsitem.CorverTexture );
			if( download!=null && ( appsitem.Update || ( appsitem.CorverTexture!=null && appsitem.CorverTexture.Update ) ) )
			{
				//온라인에서 업데이트된 것이기 때문에,
				//스트리밍 다운로드를 사용하지 않음
				download.allowStreaming = false;
			}
		}

		if( func!=null )
		{
			app.Download.SetCallback( func, wParam, lParam );
		}

		if( app.Download.Is() && app.ViewLoading.Is() )
		{
			app.Download.Loading();
		}
	}

	//리소스를 다운로드하기 위한 함수
	public void Download( string id, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( !Library.Is(id) ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		Download( FindFromId(id), func, wParam, lParam );
	}

	protected virtual void funcDownloadAsset( object wParam=null, object lParam=null )
	{
	}

	//메인 에셋을 얻기 위한 함수
	public GameObject GetMainAsset( tagAppsItem appsitem )
	{
		if( appsitem==null ) return null;

		if( appsitem.AssetBundle()!=null && appsitem.AssetBundle().Is() && appsitem.AssetBundle().IsLoad() )
		{
			return appsitem.AssetBundle().GetMainAsset();
		}

		return null;
	}

	//모든 컨텐츠를 다운로드 하기 위한 함수
	public void AllDownload()
	{
		foreach( tagAppsItem appsitem in GetAppsItems() )
		{
			appsitem.Download();
		}
	}

	//모든 커버텍스쳐를 로드하기 위한 함수
	public void CorverTextureAllDownload()
	{
		foreach( tagAppsItem appsitem in GetAppsItems() )
		{
			if( appsitem.CorverTexture!=null && appsitem.CorverTexture.Is() && !appsitem.CorverTexture.IsLoad() )
			{
				CorverTextureDownload( appsitem );
			}
		}
	}

	//커버 텍스쳐를 로드하기 위한 함수
	public void CorverTextureDownload( tagAppsItem appsitem, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( (appsitem)==null ) return;
//		if( (func)==null ) return;		//(NULL)값을 허용함
//		if( (wParam)==null ) return;	//(NULL)값을 허용함
//		if( (lParam)==null ) return;	//(NULL)값을 허용함

		if( appsitem.CorverTexture!=null && appsitem.CorverTexture.Is() && !appsitem.CorverTexture.IsLoad() )
		{
			app.Download.ON( appsitem.Name(), appsitem.CorverTexture, func, wParam, lParam );
		}
		else
		if( func!=null )
		{
			func( wParam, lParam );
		}
	}

	//아이템 정보를 업데이트 하기 위한 함수
	void update( tagAppsItem appsitem, AppsParameter col )
	{
		if( appsitem==null ) return;
		if( col==null ) return;

		appsitem.name			= col.Get("name");
		appsitem.displayName	= Library.CH( name, col.Get("displayName") );

		appsitem.Class			= AppsFunc.GetUpdateClass( col.Get("class") );
		appsitem.Type			= AppsFunc.GetUpdateType( col.Get("type") );

		appsitem.parent			= col.Get("parent");
		appsitem.resource		= col.Get("resource");
		appsitem.tag			= col.Get("tag");
		appsitem.index			= col.Get("index");
		appsitem.alias			= col.Get("alias");

		appsitem.point			= col.GetInt("point");
		appsitem.cash			= col.GetInt("cash");
		appsitem.price			= col.GetInt("price");
		appsitem.value			= col.GetInt("value");
		appsitem.description	= col.Get("description");

		appsitem.nonstorage		= col.GetBoolean("nonstorage");
		appsitem.no_bundle		= col.GetBoolean("no_bundle");
		appsitem.visible		= col.GetBoolean("visible");
		appsitem.sale			= col.GetBoolean("sale");
		appsitem.unavailable	= col.GetBoolean("unavailable");

		appsitem.assetBundleAndroid = AppsFunc.Parse( appsitem.assetBundleAndroid, col.Get("AssetBundleAndroid") );
		appsitem.assetBundleWindows = AppsFunc.Parse( appsitem.assetBundleWindows, col.Get("AssetBundleWindows") );
		appsitem.assetBundleIOS		= AppsFunc.Parse( appsitem.assetBundleIOS, col.Get("AssetBundleIOS") );
		appsitem.CorverTexture		= AppsFunc.Parse( appsitem.CorverTexture, col.Get("CorverTexture") );

		appsitem.Update = true;
		OnLoadCache( appsitem, col );
	}

	//XML파일을 저장하기 위한 함수
	void XmlSave( tagAppsItem appsitem )
	{
		if( appsitem==null ) return;

		Directory.CreateDirectory( GetPath() );
		Directory.CreateDirectory( GetPath(appsitem.id) );

		string filepath = GetPath(appsitem.id)+"/content.xml";

		XmlTextWriter xmlWriter = new XmlTextWriter( filepath, null );
		xmlWriter.Formatting = Formatting.Indented;
		xmlWriter.Indentation = 4;
		xmlWriter.WriteStartDocument();
		xmlWriter.WriteStartElement( GetClassName() );
		{
			CXml.WriteNodeValue( xmlWriter, "id",					appsitem.id );
			CXml.WriteNodeValue( xmlWriter, "name",					appsitem.name );
			CXml.WriteNodeValue( xmlWriter, "displayName",			appsitem.displayName );

			CXml.WriteNodeValue( xmlWriter, "Class",				appsitem.Class.ToString() );
			CXml.WriteNodeValue( xmlWriter, "Type",					appsitem.Type.ToString() );

			CXml.WriteNodeValue( xmlWriter, "parent",				appsitem.parent );
			CXml.WriteNodeValue( xmlWriter, "resource",				appsitem.resource );
			CXml.WriteNodeValue( xmlWriter, "tag",					appsitem.tag );
			CXml.WriteNodeValue( xmlWriter, "index",				appsitem.index );
			CXml.WriteNodeValue( xmlWriter, "alias",				appsitem.alias );

			CXml.WriteNodeValue( xmlWriter, "point",				appsitem.point );
			CXml.WriteNodeValue( xmlWriter, "cash",					appsitem.cash );
			CXml.WriteNodeValue( xmlWriter, "price",				appsitem.price );
			CXml.WriteNodeValue( xmlWriter, "value",				appsitem.value );
			CXml.WriteNodeValue( xmlWriter, "description",			appsitem.description );

			CXml.WriteNodeValue( xmlWriter, "nonstorage",			appsitem.nonstorage );
			CXml.WriteNodeValue( xmlWriter, "no_bundle",			appsitem.no_bundle );
			CXml.WriteNodeValue( xmlWriter, "visible",				appsitem.visible );
			CXml.WriteNodeValue( xmlWriter, "sale",					appsitem.sale );
			CXml.WriteNodeValue( xmlWriter, "unavailable",			appsitem.unavailable );

			CXml.WriteNodeValue( xmlWriter, "AssetBundleAndroid",	appsitem.assetBundleAndroid );
			CXml.WriteNodeValue( xmlWriter, "AssetBundleWindows",	appsitem.assetBundleWindows );
			CXml.WriteNodeValue( xmlWriter, "AssetBundleIOS",		appsitem.assetBundleIOS );
			CXml.WriteNodeValue( xmlWriter, "CorverTexture",		appsitem.CorverTexture );

			OnXmlSave( xmlWriter, appsitem );
		}
		xmlWriter.WriteEndElement();
		xmlWriter.WriteEndDocument();
		xmlWriter.Close();

#if !UNITY_EDITOR || ENCODE_CACHE
		CXml.Encode( filepath );
#endif
	}

	//XML 파일 정보를 불러오기 위한 함수
	tagAppsItem XmlLoad( string filepath )
	{
		if( !Library.Is(filepath) ) return null;

#if UNITY_EDITOR && !ENCODE_CACHE
		XmlDocument		xmlDoc			= CXml.Load(filepath);
#else
		TextAsset		textAsset		= new TextAsset(CXml.Decode(CXml.EncodeXmlPath(filepath)));
		XmlDocument		xmlDoc			= CXml.Load( textAsset );
#endif
		if( xmlDoc==null ) return null;

		tagAppsItem appsitem = New();

		XmlNode pNode = xmlDoc.SelectSingleNode( GetClassName() );
		if( pNode!=null )
		{
			appsitem.id					= CXml.GetChildValue( pNode, "id" );
			appsitem.name				= CXml.GetChildValue( pNode, "name" );
			appsitem.displayName		= CXml.GetChildValue( pNode, "displayName" );

			appsitem.Class				= AppsFunc.GetUpdateClass( CXml.GetChildValue( pNode, "Class" ) );
			appsitem.Type				= AppsFunc.GetUpdateType( CXml.GetChildValue( pNode, "Type" ) );

			appsitem.parent				= CXml.GetChildValue( pNode, "parent" );
			appsitem.resource			= CXml.GetChildValue( pNode, "resource" );
			appsitem.tag				= CXml.GetChildValue( pNode, "tag" );
			appsitem.index				= CXml.GetChildValue( pNode, "index" );
			appsitem.alias				= CXml.GetChildValue( pNode, "alias" );

			appsitem.point				= CXml.GetChildInt( pNode, "point" );
			appsitem.cash				= CXml.GetChildInt( pNode, "cash" );
			appsitem.price				= CXml.GetChildInt( pNode, "price" );
			appsitem.value				= CXml.GetChildInt( pNode, "value" );
			appsitem.description		= CXml.GetChildValue( pNode, "description" );

			appsitem.nonstorage			= CXml.GetChildBoolean( pNode, "nonstorage" );
			appsitem.no_bundle			= CXml.GetChildBoolean( pNode, "no_bundle" );
			appsitem.visible			= CXml.GetChildBoolean( pNode, "visible" );
			appsitem.sale				= CXml.GetChildBoolean( pNode, "sale" );
			appsitem.unavailable		= CXml.GetChildBoolean( pNode, "unavailable" );

			appsitem.assetBundleAndroid	= CXml.GetChildValue( pNode, appsitem.assetBundleAndroid, "AssetBundleAndroid" );
			appsitem.assetBundleWindows	= CXml.GetChildValue( pNode, appsitem.assetBundleWindows, "AssetBundleWindows" );
			appsitem.assetBundleIOS		= CXml.GetChildValue( pNode, appsitem.assetBundleIOS, "AssetBundleIOS" );
			appsitem.CorverTexture		= CXml.GetChildValue( pNode, appsitem.CorverTexture, "CorverTexture" );

			OnXmlLoad( pNode, appsitem );
		}

		return appsitem;
	}
}