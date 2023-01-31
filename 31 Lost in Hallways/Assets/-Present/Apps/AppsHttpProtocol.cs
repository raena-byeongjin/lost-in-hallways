using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

//웹 프로토콜을 처리하기 위한 클래스
public class AppsHttpProtocol : MonoBehaviour
{
	private static string SessionID = null;
	private static bool bHttpLog = true;

	public void Get( string url, bool log=true )
	{
		if( !Library.Is(url) ) return;

		WWWForm form = new WWWForm();
		if( SessionID.Length>0 )
		{
			form.AddField( "PHPSESSID", (SessionID) );
		}

		StartCoroutine( WaitForRequest( url, form ) );
	}

	public void Post( string url, AppsHTTPPost post=null )
	{
		if( !Library.Is(url) ) return;

		WWWForm form = new WWWForm();

		if( post!=null )
		{
			foreach( KeyValuePair<string, string> arg in post.dictionary ) 
			{ 
				form.AddField( arg.Key, arg.Value ); 
			}

			tagFile file = null;
			foreach( KeyValuePair<string, tagFile> arg in post.files ) 
			{
				file = arg.Value;
				if( file!=null )
				{
					form.AddBinaryData( arg.Key, file.bytes, file.filename, file.minetype ); 
				}
			}
		}

		if( Library.Is(SessionID) )
		{
			form.AddField( "PHPSESSID", SessionID );
		}

		StartCoroutine( WaitForRequest( url, form ) );
	}

	private IEnumerator WaitForRequest( string url, WWWForm form=null )
	{
		if( !Library.Is(url) ) yield break;
//		if( form==null ) yield break;	//(NULL)값을 허용함

		HttpProtocol request = null;

		for( int loop=0; loop<3; loop++ )
		{
			if( form!=null && form.data.Length>0 )
			{
				request = new HttpProtocol(Internet.Protocol( url, form ));
			}
			else
			{
				request = new HttpProtocol(Internet.Protocol( url ));
			}

			yield return request.SendWebRequest();

			//check for errors 
			if( request.Result()==UnityWebRequest.Result.Success ) 
			{
				while( request.GetResponseHeaders()!=null && request.GetResponseHeaders().ContainsKey("SET-COOKIE") )
				{
					char[] splitter = { ';' };
					string[] headers = request.GetResponseHeaders()["SET-COOKIE"].Split(splitter); 

					foreach( string header in headers ) 
					{
						if( string.IsNullOrEmpty(header) ) continue;

						string[] column = header.Split( '=' );

						if( column[0]=="PHPSESSID" )
						{
							SessionID = column[1];
						}
					} 

					break;
				}

				if( Receive(request) )
				{
					if( bHttpLog )
					{
						Debug.Log( "(http ok : "+request.GetUrl()+") "+request.GetDownloadHandler().text );
					}
				}
				else
				{
					if( request.GetDownloadHandler().text!=null )
					{
#if UNITY_EDITOR
						Debug.Log("(http error : "+request.GetUrl()+") "+request.GetDownloadHandler().text);
#else
						Debug.LogError("(http error : "+request.GetUrl()+") "+request.GetDownloadHandler().text);
#endif
					}
					else
					{
						Debug.LogError("(http error : "+request.GetUrl()+")");
					}

					Apps.ProtocolError( request.Request() );
				}
			}
			else
			{
				request.Dispose();
				request = null;

				//프로토콜 재시도
				yield return new WaitForSeconds(1f);
				continue;
			}

			break;
		}

		if( request==null || request.Result()!=UnityWebRequest.Result.Success )
		{
			if( !Apps.HttpError(request.Request()) )
			{
#if UNITY_EDITOR
				Debug.Log( "(Http Error) "+url );
#endif

				objConfirm confirm = CApp.This.Confirm.ON( CApp.This.Language.Get(TEXT.서버와_접속이_끊겼습니다), CONFIRM.YESNO, CApp.This.Engine.funcRestart );
				if( confirm!=null )
				{
					confirm.SetRejectCallback( CApp.This.Engine.funcQuit );
					confirm.data.allowEmptySpaceConfirm = false;
				}
			}
		}

		if( request!=null )
		{
			request.Dispose();
			request = null;
		}
	}

	//패킷 수신을 처리하기 위한 함수
	bool Receive( HttpProtocol request )
	{
		if( request==null ) return false;

		AppsParameter col = AppsParameter.JsonParse( request.GetDownloadHandler().text );
		if( col==null ) return false;
		if( col.array==null ) return false;

		AppsParameter child = null;
		foreach( Hashtable hash in col.array )
		{
			child = AppsParameter.Initialize( hash );
			if( child!=null )
			{
				child.request = request;
			}

			Apps.Receive( child );
		}

		return true;
	}

	//로그를 설정하기 위한 함수
	public static void SetLog( bool log )
	{
		bHttpLog = log;
	}

	//패킷 수신을 처리하기 위한 함수
	public bool Receive( string text, DownloadProtocol http=null )
	{
		if( !Library.Is(text) ) return false;
//		if( http==null ) return false;	//(NULL)값을 허용함

		AppsParameter col = AppsParameter.JsonParse( text );
		if( col==null ) return false;
		if( col.array==null ) return false;

		foreach( Hashtable hash in col.array )
		{
			AppsParameter child = AppsParameter.Initialize( hash );
			if( child!=null && http!=null && http.Request()!=null )
			{
				child.request = new HttpProtocol(http.Request());
			}

			Apps.Receive( child );
		}

		return true;
	}
}