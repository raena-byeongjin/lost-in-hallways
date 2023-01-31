using UnityEngine;

public class Bgsound
{
	//배경음악을 재생하기 위한 함수
	public static SoundBehaviour ON( tagBgsoundStatic bgsound )
	{
		if( bgsound==null ) return null;

		if( bgsound.clip!=null )
		{
			return ApplicationBehaviour.This.Bgsound.Play( bgsound.clip, bgsound.volume );
		}
		else
		if( bgsound.AssetBundle()!=null && bgsound.AssetBundle().IsLoad() )
		{
			ApplicationBehaviour.This.BgsoundStatic.AsyncLoad(bgsound);
		}

		return null;
	}

	//배경음악을 검색하기 위한 함수
	public static tagBgsoundStatic Find( string value )
	{
		if( !Library.Is(value) ) return null;
		return ApplicationBehaviour.This.BgsoundStatic.FindFromId(value) as tagBgsoundStatic;
	}
}