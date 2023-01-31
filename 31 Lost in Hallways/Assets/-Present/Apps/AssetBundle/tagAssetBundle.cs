﻿using System.Collections.Generic;
using UnityEngine;

//애셋번들 정보를 처리하기 위한 구조체
[System.Serializable]
public class tagAssetBundle
{
    public string			id				= null;
    public string			Name			= null;
    public string			url				= null;
	public ulong			Size			= 0;
    public tagTexture		CorverTexture	= null;
    public AssetBundle		bundle			= null;
	public byte[]			bytes			= null;
    public int				cache			= 0;
	public int				loadCache		= 0;
	public string			UnityVersion	= null;
	public bool				Update			= false;
	public bool				Downloaded		= false;

	public string			oldUrl			= null;

	public List<GameObject>	Samples			= null;

	public List<object>		Takes			= new List<object>();

	public bool Is()
	{
		if( Library.Is(url) )
		{
			return true;
		}

		return false;
	}

	//로드되었는지 확인하기 위한 함수
	public bool IsLoad()
	{
		if( ( bytes!=null && bytes.Length>0 ) || bundle!=null || ( cache>0 && cache<=loadCache ) )
		{
			return true;
		}

		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 유니티 버전을 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsVersion()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// 현재 유니티 버전은 상수로 선언되어 있지 않아 Switch문을 못쓰고 If문으로 확인함
		//---------------------------------------------------------------------------------------------------------------------------
		if( (this.UnityVersion)==(SYSTEM.UNITY_VERSION) )
		{
			return true;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return true;	//버전이 다르더라도 일단 허용함
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public UnityEngine.Object Get()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return Func.GetMainAsset(bundle);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public UnityEngine.Object Get( System.Type type )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (type)==(null) ) return (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		object[]	objarray	= bundle.LoadAllAssets(type);
		if( (objarray.Length)>(0) )
		{
			return (objarray[0] as UnityEngine.Object);
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (null);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public UnityEngine.Object Find( string name, System.Type type=(null) )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (name)==(null) ) return (null);
		if( (bundle)==(null) ) return (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (type)!=(null) )
		{
			return bundle.LoadAsset( (name), (type) );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return bundle.LoadAsset(name);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Unload( bool unloadAllLoadedObjects=(true) )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (this.bundle)!=(null) )
		{
			this.bundle.Unload( unloadAllLoadedObjects );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(this.CorverTexture)	= (null);
		(this.Downloaded)		= (false);
		(this.bundle)			= (null);
		(this.loadCache)		= (0);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (this.Samples)!=(null) )
		{
			this.Samples.Clear();
			(this.Samples)		= (null);
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//메인 에셋을 얻기 위한 함수
	public GameObject GetMainAsset()
	{
		if( Samples==null && bundle!=null )
		{
			Samples = new List<GameObject>();

			string[] Names = bundle.GetAllAssetNames();
			foreach( string path in Names )
			{
				if( Library.Ext(path)=="prefab" )
				{
					this.Samples.Add( bundle.LoadAsset( path, typeof(GameObject) ) as GameObject );
				}
			}
		}
#if UNITY_EDITOR
		else
		if( Samples==null )
		{
			Debug.Log("번들이 로드되지 못했습니다. : "+url);
		}
#endif

		if( Samples!=null && Samples.Count>0 )
		{
			return Samples[ Library.Random(0, Samples.Count-1) ];
		}

		return null;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 리소스를 점유하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Take( object obj )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (obj)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( !Takes.Contains(obj) )
		{
			Takes.Add(obj);
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 리소스 점유를 해제하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool Untake( object obj )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (obj)==(null) ) return false;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Takes.Remove(obj);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( (Takes.Count)<=(0) )
		{
//			Debug.Log("Unload : "+(Url));
			Unload();
			return true;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	public override string ToString()
	{
		if( Library.Is(url) )
		{
			return Library.GetFileNameExt(url)+" ("+base.ToString()+")";
		}

		return base.ToString();
	}
}