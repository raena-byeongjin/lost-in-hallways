using System.Collections.Generic;
using UnityEngine.Networking;

public class HttpProtocol
{
	private UnityWebRequest request = null;
	private string url = null;

	public HttpProtocol( UnityWebRequest request )
	{
		if( request!=null )
		{
			this.request = request;
			this.url = request.url;
		}
	}

	~HttpProtocol()
	{
		Dispose();
	}

	//요청 객체를 얻기 위한 함수
	public UnityWebRequest Request()
	{
		return request;
	}

	//URL을 얻기 위한 함수
	public string GetUrl()
	{
		return url;
	}

	//동기화 객체를 얻기 위한 함수
	public UnityWebRequestAsyncOperation SendWebRequest()
	{
		if( request!=null )
		{
			return request.SendWebRequest();
		}

		return null;
	}

	//헤더를 얻기 위한 함수
	public Dictionary<string, string> GetResponseHeaders()
	{
		if( request!=null )
		{
			return request.GetResponseHeaders();
		}

		return null;
	}

	//다운로드 객체를 얻기 위한 함수
	public DownloadHandler GetDownloadHandler()
	{
		if( request!=null )
		{
			return request.downloadHandler;
		}

		return null;
	}

	public void Dispose()
	{
		if( request!=null )
		{
			request.Dispose();
			request = null;
		}
	}

	//다운로드 객체를 얻기 위한 함수
	public UnityWebRequest.Result Result()
	{
		if( request!=null )
		{
			return request.result;
		}

		return (UnityWebRequest.Result)0;
	}
}