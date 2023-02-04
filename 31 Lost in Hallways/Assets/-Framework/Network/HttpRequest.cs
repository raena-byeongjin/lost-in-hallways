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

	//URL�� ��� ���� �Լ�
	public string GetUrl()
	{
		return url;
	}

	//�ٿ�ε� ��ü�� ��� ���� �Լ�
	public DownloadHandler GetDownloadHandler()
	{
		if( request!=null )
		{
			return request.downloadHandler;
		}

		return null;
	}
}