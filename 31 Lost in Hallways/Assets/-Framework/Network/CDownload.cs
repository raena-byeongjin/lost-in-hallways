using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

//다운로드를 처리하기 위한 클래스
public class CDownload : FrameworkBehaviour
{
    [System.Serializable]
    public class tagData
    {
        public List<tagDownload>	Reserves		= new List<tagDownload>();
        public List<tagDownload>	Downloads		= new List<tagDownload>();
        public List<tagDownload>	Completes		= new List<tagDownload>();

		public tagLoadingTask		task			= null;

		public List<tagCallback>	Callbacks		= new List<tagCallback>();
    };
    public tagData data = new tagData();

	void Update()
	{
		if( Reserves().Count>0 && data.Downloads.Count<3 )
		{
			tagDownload download = Reserves()[0];
			Reserves().Remove( download );
			if( !data.Downloads.Contains(download) )
			{
				data.Downloads.Add( download );
				StartCoroutine( Download(download) );
			}
		}
		else
		if( !Is() )
		{
			Complete();
		}

		ulong transBytes = GetBytes();
		if( transBytes>0 )
		{
			app.ViewIndigator.Set( Func.NumberFormat(transBytes)+" bytes" );
		}
	}

	//다운로드를 설정하기 위한 함수
	public tagDownload ON( string Name, tagTexture texture, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
//		if( !Libary.Is(Name) ) return null;	//(NULL)값을 허용함
		if( texture==null ) return null;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( lParam==null ) lParam = texture;

		//텍스쳐 정보가 비어있을 경우
		if( !texture.Is() )
		{
			if( func!=null ) func( wParam, lParam );
			Release( null );
			return null;
		}

		//중복된 다운로드가 있는지 확인함
		tagDownload download = Find(texture);
		if( download!=null && !download.IsLoad() )
		{
//			Debug.Log("이미 다운로드가 예약됨 => "+(Name));
			download.SetCallback( func, wParam, lParam );
			return download;
		}

		if( !texture.Update && texture.texture!=null )
		{
			if( func!=null ) func( wParam, lParam );
			Release( null );
			return null;
		}

		//업데이트 된 항목이 이미 로드되어 있을 경우, 로드된 항목을 초기화 함
		if( texture.Update && texture.texture!=null )
		{
			texture.Unload();
		}

		if( !texture.Update && texture.IsLoad() )
		{
			Debug.LogWarning("이미 로드되어있는 텍스쳐입니다.");
			if( func!=null ) func( wParam, lParam );
			Release( null );
			return null;
		}

		download = new tagDownload();
		{
			download.Name		= Name;
			download.texture	= texture;
			download.SetCallback( func, wParam, lParam );
		}

		return Register( download );
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 다운로드를 설정하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void ON( string Url, Action<object, object> func=(null), object wParam=(null), object lParam=(null) )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (Url)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		ON( (null), (Url), (func), (wParam), (lParam) );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//다운로드를 설정하기 위한 함수
	public tagDownload ON( tagTexture texture, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( texture==null ) return null;
//		if( func==null ) return null;		//(NULL)값을 허용함
//		if( wParam==null ) return null;		//(NULL)값을 허용함
//		if( lParam==null ) return null;		//(NULL)값을 허용함

		return ON( null, texture, func, wParam, lParam );
	}

	//다운로드를 설정하기 위한 함수
	public void ON( tagAssetBundle assetBundle, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( assetBundle==null ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		ON( null, assetBundle, func, wParam, lParam );
	}

	//다운로드를 설정하기 위한 함수
	public void ON( string Name, string Url, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
//		if( !Library(Name) ) return;	//(NULL)값을 허용함
		if( !Library.Is(Url) ) return;
//		if( func==null ) return;		//(NULL)값을 허용함
//		if( wParam==null ) return;		//(NULL)값을 허용함
//		if( lParam==null ) return;		//(NULL)값을 허용함

		tagDownload download = new tagDownload();
		{
			download.Name	= Name;
			download.url	= Url;
			download.SetCallback( func, wParam, lParam );
		}
		Register( download );
	}

	//다운로드를 처리하기 위한 함수
	public tagDownload ON( string Name, tagAssetBundle assetBundle, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
//		if( !Library.Is(Name) ) return null;	//(NULL)값을 허용함
		if( assetBundle==null ) return null;
		if( !Library.Is(assetBundle.url) ) return null;

		if( lParam==null ) lParam = assetBundle;

		//중복된 다운로드가 있는지 확인함
		tagDownload download = Find(assetBundle);
		if( download!=null && !download.IsLoad() )
		{
//			Debug.Log("이미 다운로드가 예약됨 => "+Name);
			download.SetCallback( func, wParam, lParam );
			return download;
		}

		if( assetBundle.IsLoad() )
		{
			//업데이트 된 항목이 이미 로드되어 있을 경우
			if( assetBundle.Update )
			{
				//로드된 항목을 초기화 함
				assetBundle.Unload();
			}
			else
			{
				if( func!=null ) func( wParam, lParam );
				Release( null );
				return null;
			}
		}

		//애셋번들 버전이 달라서 불러올 수 없습니다.
		if( Library.IsAssetBundle(assetBundle) && !assetBundle.IsVersion() )
		{
			Debug.LogWarning("애셋번들 버전이 달라서 불러올 수 없습니다. : "+assetBundle.UnityVersion+" != "+SYSTEM.UNITY_VERSION+" => "+assetBundle.url );			Release( null );
			return null;
		}

		if( !assetBundle.Update && assetBundle.IsLoad() )
		{
			Debug.LogWarning("이미 로드되어있는 애셋번들입니다.");
			if( func!=null ) func( wParam, lParam );
			Release( null );
			return null;
		}

		download = new tagDownload();
		{
			download.Name			= Name;
			download.assetBundle	= assetBundle;
			download.SetCallback( func, wParam, lParam );
		}
		Register( download );

		return download;
	}

    //다운로드 정보를 등록하기 위한 함수
	public tagDownload Register( tagDownload download )
	{
		if( download==null ) return null;

		if( data.task!=null )
		{
			download.task = data.task;
			data.task.Downloads.Add( download );
		}

		data.Reserves.Add( download );
		Enable( true );

		return download;
	}
	
    //다운로드를 설정하기 위한 함수
	IEnumerator Download( tagDownload download )
	{
		if( download==null ) yield break;

		while( true )
		{
			if( Library.Is(download.url) ) break;
			if( download.assetBundle!=null && Library.Is(download.assetBundle.url) ) break;
			if( download.texture!=null && Library.Is(download.texture.url) ) break;

#if UNITY_EDITOR
			Debug.Log("비어있는 다운로드 URL이 요청됨");
#endif
			yield break;
		}

		//스트리밍 또는 캐쉬에서 로드할 수 있는지 확인함
		if( download.allowStreaming )
		{
			if( Library.IsTexture(download.GetUrl()) )
			{
				if( IsCache(download.texture.url) )
				{
					LoadTexture( CEngine.GetCachePath(download.texture.url), (download) );
					yield break;
				}
				/*
				else
				if( IsStreamingAssets(download.texture.Url) )
				{
					LoadTexture( CEngine.GetStreamingPath( (download.texture.Url), (false) ), (download) );
					yield break;
				}
				*/
			}
			else
			if( Library.IsAssetBundle(download.GetUrl()) )
			{
				if( IsCache(download.assetBundle.url) )
				{
					StartCoroutine( LoadAssetBundle( CEngine.GetCachePath(download.assetBundle.url), (download) ) );
					yield break;
				}
				else
				if( IsStreamingAssets(download.assetBundle.url) )
				{
					if( Library.IsAssetBundle(download.assetBundle) )
					{
						StartCoroutine( LoadAssetBundle( CEngine.GetStreamingPath( (download.assetBundle.url), (false) ), (download) ) );
						yield break;
					}
					else
					{
						//애셋번들 외 프로토콜을 통해 받아야만 인식되는 파일을 다운로드하기 위해, 에셋번들이 아닐 경우 Break없이 통과시킴
					}
				}
			}
			else
			if( Library.IsXml(download.GetUrl()) )
			{
			}
		}

		//프로토콜을 생성함
		DownloadProtocol http = null;
		if( true )
		{
			string url = download.GetUrl();
			if( !download.endStreaming && Library.IsUrl(url) )
			{
#if UNITY_EDITOR
				if( ( download.allowStreaming && download.texture==null ) || ( download.texture!=null && download.texture.isStreamingSave ) )
#else
				if( download.allowStreaming && download.texture==null )
#endif
				{
					url = CEngine.GetStreamingPath(url);
				}
			}

			http = new DownloadProtocol( url );
			if( http==null ) yield break;

			if( download.texture!=null )
			{
				http.texture = download.texture;
			}

			http.download = download;
		}

#if DOWNLOAD_LOG
		Debug.Log( "Download => "+http.Url );
#endif

		download.http = http;

		long preTicks = DateTime.Now.Ticks;

		while( http.IsProcess() )
		{
			if( (DateTime.Now.Ticks-preTicks)/10000000f>=1f/30 )
			{
				preTicks = DateTime.Now.Ticks;
				yield return new WaitForEndOfFrame();
			}
		}

		yield return http.Request();

		if( http.IsSuccess() )
        {
			foreach( tagCallback func in download.Callbasks )
			{
				if( func.lParam==null ) func.lParam = http;
			}

			if( download.texture!=null && http.texture!=null && http.texture.IsLoad() )
			{
				download.texture.texture	= http.texture.texture;
				download.texture.width		= http.texture.width;
				download.texture.height		= http.texture.height;

				if( download.texture.texture!=null )
				{
					download.texture.texture.wrapMode = TextureWrapMode.Clamp;
				}

				download.texture.Update = false;
			}
			else
			if( download.assetBundle!=null )
			{
#if UNITY_EDITOR
				if( Library.IsAssetBundle(download.assetBundle) )
				{
					string oldPath = CEngine.GetStreamingPath( download.assetBundle.oldUrl, false );
					if( Library.IsFile(oldPath) )
					{
						Debug.Log( "(StreamingAsset-Delete) "+oldPath );
						Library.Delete( oldPath );
					}

					if( http.bytes!=null && http.bytes.Length>0 )
					{
						Debug.Log( "(StreamingAsset) "+Library.GetFileName(http.url)+".asset" );
						if( !Directory.Exists( CEngine.GetStreamingPath( http.url, false, false ) ) )
						{
							//폴더를 생성함
							Library.CreateDirectory( CEngine.GetStreamingPath( http.url, false, false ) );
						}

						Func.WriteAllBytes( CEngine.GetStreamingPath( http.url, false ), http.bytes );
					}
					else
					{
						Debug.Log( "애셋번들에서 Byte 정보를 찾을 수 없습니다. 암호화가 되어 있지 않을 수 있습니다. : "+download );
					}
				}
				else
				{
					string oldPath = CEngine.GetStreamingPath( (download.assetBundle.oldUrl), (false) );
					if( File.Exists(oldPath) )
					{
						Debug.Log( "(StreamingAsset-Delete) "+(oldPath) );
						Library.Delete( oldPath );
					}

					if( http.bytes!=null && http.bytes.Length>0 )
					{
						Debug.LogWarning( "(StreamingAsset) "+Library.GetFileNameExt(http.url) );
						if( !Directory.Exists( CEngine.GetStreamingPath( (http.url), (false), (false) ) ) )
						{
							Library.CreateDirectory( CEngine.GetStreamingPath( (http.url), (false), (false) ) );
						}

						Func.WriteAllBytes( (Application.persistentDataPath)+"/"+Library.GetFileNameExt(http.url), (http.bytes) );
					}
					else
					{
						Debug.Log( "애셋번들에서 Byte 정보를 찾을 수 없습니다. 암호화가 되어 있지 않을 수 있습니다. : "+(download) );
					}
				}
#endif

				if( Library.IsAssetBundle(download.assetBundle) && download.http.Progresses.Count<=0 )
				{
					if( LoadAssetBundle( download, http.bytes ) )
					{
						if( download.assetBundle.bundle==null )
						{
							download.assetBundle.bundle = http.assetBundle;
						}

						download.assetBundle.Update = false;
					}
				}
			}

			if( download.http.Progresses.Count<=0 )
			{
				Release( download );
			}
        }
        else
        {
			if( !Library.IsUrl(download.http.url) && !download.endStreaming )
			{
//				Debug.Log( "(Jar파일을 찾을 수 없음, 온라인 서버에서 검색함) "+download.http.Url );

				download.http			= null;
				download.endStreaming	= true;

				Register(download);
				data.Downloads.Remove(download);

				yield return null;
			}
			else
			{
				Debug.LogError( "Download Error : "+http.error+" => "+http.url );
				Release( download );
			}
        }

		http.Release();
	}

	//다운로드 정보를 초기화 하기 위한 함수
    public void Release( tagDownload download )
    {
//		if( download==null ) return;	//(NULL)값을 허용함

		if( download!=null )
		{
			download.Call();

			if( download.http!=null && download.http.Progresses.Count>0 )
			{
				foreach( tagProgress progress in download.http.Progresses )
				{
					app.Progress.Release( progress );
				}
			}

			if( !data.Completes.Contains(download) )
			{
				if( app.ViewLoading.Is() && download.task!=null )
				{
					app.ViewLoading.TaskProgress( download.task.component, GetProgressValue(download.task) );
				}

				download.Complete = true;
				data.Completes.Add(download);
			}

			data.Downloads.Remove(download);
		}

		if( !Is() )
		{
			Complete();
		}

		/*
		if( download!=null && download.http!=null )
		{
			download.http.Release();
			download.http = null;
		}
		*/
    }

    //-------------------------------------------------------------------------------------------------------------------------------
	// 로딩을 완료하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Complete()
	{
        //---------------------------------------------------------------------------------------------------------------------------
	    // 로딩 화면이 활성화되있을 경우, 완료 리스트를 초기화 하지 않음
	    //---------------------------------------------------------------------------------------------------------------------------
		if( !app.ViewLoading.Is() )
		{
			Reset();
		}

        //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
		if( true )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			List<tagCallback>	Functions	= new List<tagCallback>();
			foreach( tagCallback function in data.Callbacks )
			{
				//리스트가 변조되었을때 정상 처리하기 위해, 리스트를 복제해서 사용함
				Functions.Add( function );
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			data.Callbacks.Clear();

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			foreach( tagCallback function in Functions )
			{
				Func.Call( function );
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}

        //---------------------------------------------------------------------------------------------------------------------------
	    // 아직 대기중인 다운로드가 있을 경우, 작업을 중단하고 객체를 다시 활성화 함
	    //---------------------------------------------------------------------------------------------------------------------------
		if( (Reserves().Count)>(0) )
		{
			Enable( true );
		}
		else
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( app.ViewIndigator.Is(app.Download) ) app.ViewIndigator.OFF( app.Download );
			if( app.ViewLoading.Is(app.Download) )	 app.ViewLoading.OFF( app.Download );

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( Apps.IsConnect() && !Apps.IsLogin() && Apps.isUpdateEnd )
			{
				app.Engine.OnLoadEnd();
			}
			else
			if( !(play.LoadEnd) )
			{
//				Debug.Log("No OnLoadEnd");
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			if( !Is() )
			{
				Enable( false );
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}

        //---------------------------------------------------------------------------------------------------------------------------
	    // -
	    //---------------------------------------------------------------------------------------------------------------------------
	}

	//다운로드 완료를 처리하기 위한 함수
	public void Complete( DownloadProtocol http )
	{
		if( http==null ) return;

		foreach( tagDownload download in data.Downloads )
		{
			if( download.http==http )
			{
				Release( download );
				break;
			}
		}
	}

	//다운로드 정보를 초기화 하기 위한 함수
	public void Reset()
	{
		data.task = null;

		Reserves().Clear();
		data.Downloads.Clear();
		data.Completes.Clear();
	}

	public void SetCallback( Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( func==null ) return;
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( !Is() )
		{
			if( app.ViewIndigator.Is(this) )
			{
				app.ViewIndigator.OFF( this );
			}

			//함수 실행 순서가 엉킬 수 있기 때문에, 다음 프레임으로 넘겨서 처리함
			app.MessageQueue.ON( func, wParam, lParam );
			return;
		}

		data.Callbacks.Add( new tagCallback( func, wParam, lParam ) );

  	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 인디게이터를 활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Loading()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( !app.ViewLoading.Is(this) )
		{
			app.ViewLoading.ON( this );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//인디게이터를 활성화 하기 위한 함수
	public void Indigator()
	{
		if( !app.ViewLoading.Is() )
		{
			app.ViewIndigator.ON( this );
			Enable();
		}
	}

    //다운로드 중인 파일 크기를 얻기 위한 함수
	public ulong GetBytes()
	{
		ulong transBytes = 0;

		foreach( tagDownload download in Reserves() )
		{
			transBytes += GetBytes(download);
		}

		foreach( tagDownload download in data.Downloads )
		{
			transBytes += GetBytes(download);
		}

		foreach( tagDownload download in data.Completes )
		{
			transBytes += GetBytes(download);
		}

		return transBytes;
	}

	//다운로드 완료한 파일 크기를 얻기 위한 함수
	public ulong GetCompleteBytes()
	{
		ulong transBytes = 0;

		foreach( tagDownload download in data.Completes )
		{
			transBytes += GetBytes(download);
		}

		return transBytes;
	}

	public ulong GetBytes( tagDownload download )
	{
		if( download==null ) return 0;

		if( download.assetBundle!=null )
		{
			return download.assetBundle.Size;
		}
		else
		if( download.texture!=null )
		{
			return download.texture.Size;
		}
		else
		if( download.http!=null && download.http.Request()!=null )
		{
			return download.http.Request().downloadedBytes;
		}

		return 0;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 다운로드중인지 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool Is( bool includeComplete=(false) )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (Reserves().Count)>(0) )
		{
			return true;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (data.Downloads.Count)>(0) )
		{
			return true;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (includeComplete) && (data.Completes.Count)>(0) )
		{
			return true;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 다운로드가 활성화 되어 있는지 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsActive()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( isActiveAndEnabled )
		{
			return true;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 다운로드 리스트를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public List<tagDownload> GetDownloadList()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.Downloads);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 완료 리스트를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public List<tagDownload> GetCompleteList()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.Completes);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 다운로드를 취소하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Clear()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( tagDownload download in data.Downloads )
		{
			Debug.Log( "(Download) "+Library.GetFileNameExt(download.GetUrl()) );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Reset();

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( IsActive() )
		{
			Complete();
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 캐시를 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsCache( string filename )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (filename)==(null) ) return false;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( File.Exists( CEngine.GetCachePath(filename) ) )
		{
			return true;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 스트리밍 에셋을 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsStreamingAssets( string filename )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (filename)==(null) ) return false;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( File.Exists( CEngine.GetStreamingPath( (filename), (false) ) ) )
		{
			return true;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return false;
	}

    //애셋 번들을 불러오기 위한 함수
	IEnumerator LoadAssetBundle( string filepath, tagDownload download )
	{
		if( !Library.IsFile(filepath) ) yield break;
		if( download==null ) yield break;

		yield return null;

		FileStream	stream		= File.OpenRead(filepath);
		byte[]		buffer		= new byte[stream.Length];
		byte[]		bytes		= new byte[SYSTEM.FILE_LOAD_BUFFER];
		int			transbytes	= 0;
		int			nBuf		= 0;
		long		preTicks	= 0;

		while( stream!=null )
		{
			transbytes = stream.Read( bytes, 0, Math.Min( (int)stream.Length, bytes.Length ) );
			if( transbytes<=0 )
			{
				break;
			}

			Buffer.BlockCopy( bytes, 0, buffer, nBuf, transbytes );
			nBuf += transbytes;

			if( (DateTime.Now.Ticks-preTicks)/10000000f>=1f/30f )
			{
				preTicks = DateTime.Now.Ticks;
				yield return new WaitForEndOfFrame();
			}
		}

		stream.Close();
		yield return new WaitForEndOfFrame();

		LoadAssetBundle( download, buffer );
		yield return new WaitForEndOfFrame();

		download.Call();
		Release( download );
	}

    //애셋번들 정보를 불러오기 위한 함수
	bool LoadAssetBundle( tagDownload download, byte[] bytes )
	{
		if( download==null ) return false;
		if( bytes==null || bytes.Length<=0 ) return false;

		if( download.assetBundle==null ) download.assetBundle = new tagAssetBundle();

		DownloadProtocol.AssetBundleDecode(bytes);

		download.assetBundle.loadCache = download.assetBundle.cache;
		if( download.assetBundle.bundle!=null )
		{
#if UNITY_EDITOR
			Debug.LogWarning( download.Name+" : "+download.assetBundle.url );
#endif
		}
		else
		if( download.assetBundle.Is() )
		{
			download.assetBundle.bundle = AssetBundle.LoadFromMemory(bytes);
		}

		return true;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 애셋 번들을 불러오기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void LoadTexture( string filepath, tagDownload download )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( !File.Exists(filepath) ) return;
		if( (download)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		byte[] bytes = File.ReadAllBytes(filepath);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		LoadTexture( (download), CXml.Encode( (bytes), (SYSTEM.TEXTURE_ENCODE_DENSITY) ) );

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		download.Call();

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		Release( download );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 애셋번들 정보를 불러오기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void LoadTexture( tagDownload download, byte[] bytes )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (download)==(null) ) return;
		if( (bytes)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (download.texture)==(null) ) (download.texture)	= new tagTexture();

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(download.texture.texture)	= new Texture2D(0,0);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		download.texture.texture.LoadImage(bytes);
		(download.texture.width)	= (download.texture.texture.width);
		(download.texture.height)	= (download.texture.texture.height);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

    //객체 활성화를 설정하기 위한 함수
	public void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			if( !enable && Is() )
			{
#if UNITY_EDITOR
				Debug.Log("아직 예약된 다운로드 요청이 있는데 프로세스 비활성화가 요청됨, 요청을 거부함");
#endif
			}
			else
			{
				enabled = enable;
			}
		}
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 예약된 다운로드가 있는지 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public tagDownload Find( tagTexture texture )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (texture)==(null) ) return (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( tagDownload download in Reserves() )
		{
			if( (download.texture)==(texture) )
			{
				return (download);
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( tagDownload download in data.Downloads )
		{
			if( (download.texture)==(texture) )
			{
				return (download);
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( tagDownload download in data.Completes )
		{
			if( (download.texture)==(texture) )
			{
				return (download);
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (null);
	}

    //예약된 다운로드가 있는지 확인하기 위한 함수
	public tagDownload Find( tagAssetBundle assetBundle )
	{
		if( assetBundle==null ) return null;

		foreach( tagDownload download in Reserves() )
		{
			if( download.assetBundle==assetBundle )
			{
				return download;
			}
		}

		foreach( tagDownload download in data.Downloads )
		{
			if( download.assetBundle==assetBundle )
			{
				return download;
			}
		}

		foreach( tagDownload download in data.Completes )
		{
			if( download.assetBundle==assetBundle )
			{
				return download;
			}
		}

		return null;
	}

	//완료 수를 얻기 위한 함수
	public int CompleteCount()
	{
		return GetCompleteList().Count;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // -
	//-------------------------------------------------------------------------------------------------------------------------------
	public int Count()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (Reserves().Count) + (GetDownloadList().Count) + (GetCompleteList().Count);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 다운로드 정보를 등록하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public List<tagDownload> Reserves()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.Reserves);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // TASK를 설정하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Task( Component component )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (component)==(null) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(data.task)	= app.ViewLoading.FindTask(component);
		if( (data.task)==(null) )
		{
			(data.task)	= app.ViewLoading.Task(component);
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // TASK를 설정하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Task()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Task( this );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	public float GetProgressValue( tagLoadingTask task )
	{
		if( task==null ) return 0;

		float value = 0;

		foreach( tagDownload download in task.Downloads )
		{
			if( !download.Complete )
			{
				if( download.http!=null && download.http.Request()!=null )
				{
					value += download.http.Request().downloadProgress;
				}
				else
				if( download.IsLoad() )
				{
					value += 1f;
				}
			}
			else
			{
				value += 1f;
			}
		}

		return Func.Divide( value, task.Downloads.Count );
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // TASK를 확인하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public bool IsTask( Component component )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (component)==(null) ) return false;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (data.task)!=(null) )
		{
			if( (data.task.component)==(component) )
			{
				return true;
			}
		}

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