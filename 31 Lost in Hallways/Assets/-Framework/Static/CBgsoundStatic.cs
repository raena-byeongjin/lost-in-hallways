using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

//배경음악 정보를 처리하기 위한 클래스
public class CBgsoundStatic : CSoundStatic
{
	public List<tagBgsoundStatic> BgsoundStatics = new List<tagBgsoundStatic>();

	protected override void Awake()
	{
		base.Awake();
		Initialize( "Bgsounds", UPDATE_CLASS.Sound.ToString(), UPDATE_TYPE.Bgsound.ToString() );
	}

	public override void OnInitialize()
	{
		BGSOUND.Initialize();
	}

    //객체를 생성하기 위한 함수 
    protected override tagAppsItem New()
    {
        return new tagBgsoundStatic();
    }

	//사운드 객체를 삭제하기 위한 함수 
	protected override void OnDelete( tagAppsItem appsitem )
	{
		if( appsitem==null ) return;
		BgsoundStatics.Remove( appsitem as tagBgsoundStatic );
    }

	public void AsyncLoad( tagBgsoundStatic bgsoundstatic )
	{
		if( bgsoundstatic==null ) return;
		StartCoroutine( AsyncLoad_(bgsoundstatic) );
	}

	IEnumerator AsyncLoad_( tagBgsoundStatic bgsoundstatic )
	{
		if( bgsoundstatic==null ) yield break;

		if( bgsoundstatic.AssetBundle()!=null && bgsoundstatic.AssetBundle().IsLoad() && bgsoundstatic.AssetBundle().bundle!=null )
		{
			AssetBundleRequest request = bgsoundstatic.AssetBundle().bundle.LoadAllAssetsAsync(typeof(AudioClip));
			yield return request;

			bgsoundstatic.clip = request.asset as AudioClip;
			if( bgsoundstatic.clip!=null )
			{
				Bgsound.ON(bgsoundstatic);
			}
		}
		else
		{
			Debug.Log("배경음악이 로드되지 못함 : "+bgsoundstatic+" => "+bgsoundstatic.AssetBundle().bundle);
		}
	}
}