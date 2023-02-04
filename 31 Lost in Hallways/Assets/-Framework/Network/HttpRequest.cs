using UnityEngine.Networking;

public class HttpRequest
{
	private UnityWebRequest request = null;
	private string url = null;

	public HttpRequest( UnityWebRequest request )
	{
		if( request!=null )
		{
			this.request = request;
			this.url = request.url;
		}
	}

	//URL을 얻기 위한 함수
	public string GetUrl()
	{
		return url;
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
}