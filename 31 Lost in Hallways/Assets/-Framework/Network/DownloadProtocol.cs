using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;

public class DownloadProtocol
{
	private UnityWebRequest		request				= null;
	public List<tagProgress>	Progresses			= new List<tagProgress>();

	public string				url					= null;
	public int					Version				= 0;
	public bool					Done				= false;
	public bool					Success				= false;
	public AudioClip			audioClip			= null;
	public AssetBundle			assetBundle			= null;
	public tagTexture			texture				= null;
	public string				text				= null;
	public byte[]				bytes				= null;
	public string				error				= null;
	public tagDownload			download			= null;

	private AsyncOperation		asyncOperation		= null;

	public DownloadProtocol( string url )
	{
		this.url		= url;
		this.Version	= -1;

		if( Library.Is(url) )
		{
			Apps.Download(url);
			request = Internet.Protocol( url );
			if( request!=null )
			{
				asyncOperation = request.SendWebRequest();
			}
		}
	}

	public DownloadProtocol( UnityWebRequest request )
	{
		this.request = request;

		if( request!=null )
		{
			this.url = request.url;
		}
	}

	~DownloadProtocol()
	{
		if( Request()!=null )
		{
			Request().Dispose();
		}
	}

	//진행중인지 확인하기 위한 함수
	public bool IsProcess()
	{
		foreach( tagProgress progress in this.Progresses  )
		{
			if( progress.IsProgress() )
			{
				//비동기 복호화 중, true를 반환함
				return true;
			}
		}

		if( Request()!=null && Request().result!=UnityWebRequest.Result.InProgress )
		{
			if( !Dispose() )
			{
				return true;
			}
		}

		return !Done;
	}

	//성공했는지 확인하기 위한 함수
	public bool IsSuccess()
	{
		if( Request()!=null )
		{
			if( Request().isDone )
			{
				Dispose();
			}
		}

		return Success;
	}

	//HTTP 프로토콜을 해제하기 위한 함수
	public bool Dispose()
	{
		if( Done ) return true;
		if( Request()==null ) return true;

		if( Request().isDone && Request().result==UnityWebRequest.Result.Success )
		{
			string Ext = Library.Ext(GetUrl());
			if( Library.IsTexture( Ext, true ) )
			{
				if( Library.IsUrl(url) )
				{
#if UNITY_EDITOR
					if( texture==null || texture.isStreamingSave )
					{
						Debug.Log( "(StreamingAssets) "+Library.GetFileName(request.url)+".texture" );

						if( !Library.IsFile( CEngine.GetStreamingPath( GetUrl(), false, false ) ) ) Library.CreateDirectory( CEngine.GetStreamingPath( GetUrl(), false, false ) );
						Func.WriteAllBytes( CEngine.GetStreamingPath( GetUrl(), false ), CXml.CopyEncode( request.downloadHandler.data, SYSTEM.TEXTURE_ENCODE_DENSITY ) );
					}
					else
#endif
					if( Platform.IsRuntime() && Library.IsUrl(GetUrl()) && texture.isCacheSave )
					{
						if( !Library.IsDir(CEngine.GetCachePath()) ) Directory.CreateDirectory(CEngine.GetCachePath());

#if UNITY_EDITOR
						Debug.Log( "(Cache) "+Library.GetFileName(GetUrl())+".texture" );
#endif
						Func.WriteAllBytes( CEngine.GetCachePath()+"/"+Library.GetFileName(GetUrl())+".texture", CXml.CopyEncode( request.downloadHandler.data, SYSTEM.TEXTURE_ENCODE_DENSITY ) );
					}

					if( texture!=null )
					{
						texture.Load( Request().downloadHandler.data );
					}
					else
					{
						texture = new tagTexture( Request().downloadHandler.data );
					}
				}
				else
				if( Request().downloadHandler.data!=null && Request().downloadedBytes>0 )
				{
					Texture2D texture = new Texture2D(0,0);
					texture.LoadImage( CXml.Decode( request.downloadHandler.data, SYSTEM.TEXTURE_ENCODE_DENSITY ) );

					if( this.texture!=null )
					{
						this.texture.Set( texture );
					}
					else
					{
						this.texture = new tagTexture(Request().downloadHandler.data);
					}
				}

				if( texture!=null && texture.isBinaryLoad )
				{
					texture.bytes = request.downloadHandler.data;
				}
			}
			else
			if( Library.IsAssetBundle( Ext, true ) )
			{
				if( Library.GetString( request.downloadHandler.data, 0, "UnityFS".Length )=="UnityFS" )
				{
#if UNITY_EDITOR
					Debug.Log("애셋번들을 로드할 수 없습니다. : "+request.url);
#endif

//					this.assetBundle = www.assetBundle;
				}
				else
				if( Progresses.Count<=0 )
				{
					bytes = Request().downloadHandler.data;
					if( download!=null && download.assetBundle!=null )
					{
						download.assetBundle.bytes = bytes;
					}

					//에디터에서는 StreamingAssets에 저장할것이기 때문에, 로컬 Cache에는 저장하지 않음
					if( Platform.IsRuntime() && Library.IsUrl(url) )
					{
						Debug.Log( "(Cache) "+Library.GetFileNameExt(url) );
						SaveAssetBundleLowData( Library.GetFileNameExt(url), bytes );

						//비동기 복호화
						tagProgress progress = CApp.This.Engine.EncodeAsync(this);
						if( progress!=null )
						{
							this.Progresses.Add( progress );
						}

						//비동기 복호화를 위해, 이후 처리를 중단함
						return false;
					}
				}
			}
			else
			if( Library.IsSoundFile( Ext, true ) )
			{
#if UNITY_EDITOR
				Debug.Log("사운드 파일을 로드할 수 없습니다. : "+request.url);
#endif

//				this.audioClip	= request.GetAudioClip(false);
//				this.bytes		= request.bytes;
			}
			else
			{
				this.text	= request.downloadHandler.text;
				this.bytes	= request.downloadHandler.data;
			}

			this.Success = true;
		}
		else
		{
			if( Library.Is(Request().error) )
			{
				error = Request().error;
			}
			else
			{
				error = Request().result.ToString();
			}
		}

		Done = true;

		if( request!=null )
		{
			Reset();
		}

		return true;
	}

	public bool IsDone()
	{
		if( request!=null )
		{
			return request.isDone;
		}

		return false;
	}

	public float GetProgress()
	{
		if( request!=null )
		{
			return request.downloadProgress;
		}

		return 0f;
	}

	//메모리를 초기화 하기 위한 함수
	public void Release()
	{
		assetBundle	= null;
		bytes		= null;

		if( Request()!=null )
		{
			Reset();
		}
	}

	//프로토콜을 초기화하기 위한 함수
	public void Reset()
	{
		if( request!=null )
		{
			request.Dispose();
			request = null;
		}
	}

	//애셋번들 데이타를 저장하기 위한 함수
	void SaveAssetBundleLowData( string filename, byte[] bytes )
	{
		if( !Library.Is(filename) ) return;
		if( bytes==null || bytes.Length<=0 ) return;

		string path	= CEngine.GetCachePath();
		if( !Library.IsDir(path) )
		{
			Directory.CreateDirectory(path);
		}

		File.WriteAllBytes( path+"/"+Library.GetFileName(filename)+".asset", bytes );
	}

	public static void AssetBundleDecode( byte[] bytes )
	{
		if( bytes==null ) return;

		//헤더 암호화
		int End = Mathf.Min( 64, bytes.Length );
        for( int i=0; i<End; i+=2 )
        {
            bytes[i] = AppsFunc.encode( bytes[i], i );
		}

        for( int i=0; i<bytes.Length; i+=1024 )
        {
			bytes[i] = AppsFunc.encode( bytes[i], i );
        }
	}

	//프로토콜 객체를 얻기 위한 함수
	public string GetUrl()
	{
		return url;
	}

	//프로토콜 객체를 얻기 위한 함수
	public UnityWebRequest Request()
	{
		return request;
	}

	//다운로드 핸들러를 얻기 위한 함수
	public DownloadHandler GetDownloadHandler()
	{
		if( Request()!=null )
		{
			return Request().downloadHandler;
		}

		return null;
	}

	public byte[] Bytes()
	{
		return bytes;
	}
}