using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Internet
{
	private static int nRequstCount = 0;
	public static void Analyse( string url )
	{
		if( !Library.Is(url) ) return;

		/*
		if( Library.IsHttpProtocal(Url) )
		{
			Debug.Log(Url);
		}
		*/

		nRequstCount += 1;
	}

	public static UnityWebRequest Request( string url )
	{
		if( !Library.Is(url) ) return null;

		Analyse( url );
		return UnityWebRequest.Get( url );
	}

	public static UnityWebRequest Post( string url )
	{
		if( !Library.Is(url) ) return null;

		Analyse( url );
		return UnityWebRequest.Post( url, new Dictionary<string, string>() );
	}

	public static UnityWebRequest Protocol( string url, WWWForm form=null )
	{
		if( !Library.Is(url) ) return null;

		Analyse( url );

		if( form!=null )
		{
			return UnityWebRequest.Post( url, form );
		}

		return UnityWebRequest.Get(url);
	}
}