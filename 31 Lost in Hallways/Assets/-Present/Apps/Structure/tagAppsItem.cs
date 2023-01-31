using UnityEngine;
using System;

//아이템 정보를 처리하기 위한 클래스
public class tagAppsItem
{
	public string				name					= null;
	public string				displayName				= null;

	public string				id						= null;
	public UPDATE_CLASS			Class;
	public UPDATE_TYPE			Type;

	public string				parent					= null;
	public string				resource				= null;
	public string				tag						= null;
	public string				index					= null;
	public string				alias					= null;

	public int					point					= 0;
	public int					cash					= 0;
	public int					price					= 0;
	public int					value					= 0;
	public string				description				= null;

	public bool					nonstorage				= false;
	public bool					no_bundle				= false;
	public bool					visible					= false;
	public bool					sale					= false;
	public bool					unavailable				= false;

	public tagAssetBundle		assetBundleAndroid		= new tagAssetBundle();
	public tagAssetBundle		assetBundleWindows		= new tagAssetBundle();
	public tagAssetBundle		assetBundleIOS			= new tagAssetBundle();
	public tagTexture			CorverTexture			= new tagTexture();

	public bool					Update					= false;

	public AppsItemListener		eventListener			= null;

	//이름을 얻기 위한 함수
	public string Name()
	{
		return Library.CH( name, displayName );
	}

	public override string ToString()
	{
		return name+" ("+id+", "+base.ToString()+")";
	}

	// 애셋번들을 얻기 위한 함수
	public tagAssetBundle AssetBundle()
	{
#if UNITY_IOS
		return assetBundleIOS;
#elif UNITY_STANDALONE_WIN
		return assetBundleWindows;
#endif

		return assetBundleAndroid;
	}

	//메인 에셋을 얻기 위한 함수
	public GameObject GetMainAsset()
	{
		if( AssetBundle()!=null && AssetBundle().IsLoad() )
		{
			return AssetBundle().GetMainAsset();
		}

		return null;
	}

	//업데이트된 리소스를 확인하기 위한 함수
	public bool IsResourceUpdate()
	{
		if( AssetBundle()!=null && IsResourceUpdate(AssetBundle()) )
		{
			return true;
		}

		if( CorverTexture!=null && CorverTexture.Is() && IsResourceUpdate(CorverTexture) )
		{
			return true;
		}

		return false;
	}

	//리소스가 업데이트되었는지 확인하기 위한 함수
	public bool IsResourceUpdate( tagAssetBundle assetBundle )
	{
		if( assetBundle==null ) return false;

		if( assetBundle.Is() )
		{
			if( assetBundle.Update )
			{
				return true;
			}

#if UNITY_EDITOR
			string filepath = CEngine.GetStreamingPath( assetBundle.url, false );
			if( !Library.IsFile(filepath) )
			{
				return true;
			}
#endif
		}

		return false;
	}

	//리소스가 업데이트되었는지 확인하기 위한 함수
	public bool IsResourceUpdate( tagTexture texture )
	{
		if( texture==null ) return false;

		if( texture.Is() )
		{
			if( texture.Update )
			{
				return true;
			}

#if UNITY_EDITOR
			string filepath = CEngine.GetStreamingPath( texture.url, false );
			if( !Library.IsFile(filepath) )
			{
				return true;
			}
#endif
		}

		return false;
	}

	//텍스쳐를 얻기 위한 함수
	public Texture2D GetTexture()
	{
		if( CorverTexture!=null )
		{
			return CorverTexture.GetTexture();
		}

		return null;
	}

	//다운로드를 처리하기 위한 함수
	public virtual void Download( Action<object, object> func=null, object wParam=null, object lParam=null )
	{
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( eventListener!=null )
		{
			eventListener.Download( this, func, wParam, lParam );
		}
	}
}