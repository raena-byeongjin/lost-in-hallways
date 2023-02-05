using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

//사운드 정보를 처리하기 위한 클래스
public class CSoundStatic : AppsItemListener
{
	public List<tagSoundStatic> SoundStatics = new List<tagSoundStatic>();

	protected override void Awake()
	{
		base.Awake();
		if( !Apps.GetAppsItemListeners().Contains(this) )
		{
			Initialize( "Sounds", UPDATE_CLASS.Sound.ToString() );
		}
	}

	public override void OnInitialize()
	{
		SOUND.Initialize();
	}

    //사운드 객체를 생성하기 위한 함수 
    protected override tagAppsItem New()
    {
        return new tagSoundStatic();
    }

	//사운드 객체를 삭제하기 위한 함수 
	protected override void OnDelete( tagAppsItem appsitem )
	{
		if( appsitem==null ) return;
		GetList().Remove( appsitem as tagSoundStatic );
    }

	//사운드 리스트를 얻기 위한 함수
	private List<tagSoundStatic> GetList()
	{
		return SoundStatics;
	}

	//사운드 정보를 검색하기 위한 함수
    private tagSoundStatic Find( string id )
    {
        if( !Library.Is(id) ) return null;

        foreach( tagSoundStatic soundstatic in GetList() )
        {
            if( soundstatic.id==id )
            {
				return soundstatic;
            }
        }

		return null;
    }

	//사운드를 재생하기 위한 함수
	public SoundBehaviour Play( tagSoundStatic soundstatic, bool loop=false, float delay=0f, Transform parent=null )
	{
		if( soundstatic==null ) return null;
//		if( parent==null ) return null; //(NULL)값을 허용함

		if( soundstatic.Latest>0 && Time.time-soundstatic.Latest<soundstatic.interval )
		{
			//재생 간격이 너무 좁게 호출되었을 경우, 중단함
			return null;
		}

		//마지막 재생 시간을 업데이트 함 (inverval을 확인하기 위한 용도)
		soundstatic.Latest = Time.time;

		if( soundstatic.clip!=null )
		{
			return app.Sound.Fx( soundstatic.clip, soundstatic.volume, loop, delay );
		}
		else
		if( soundstatic.AssetBundle()!=null && soundstatic.AssetBundle().Is() )
		{
			//오디오 클립이 직접 업로드되어 있는 경우
		}
		else
		if( Library.Is(soundstatic.resource) )
		{
			tagResource resource = app.Resource.Find(soundstatic.resource);
			if( resource!=null )
			{
//				SoundBehaviour soundbehaviour = null;

				SoundDesc desc = new SoundDesc();

				desc.soundstatic	= soundstatic;
				desc.tag			= Library.CH( soundstatic.name, soundstatic.tag, false );
				desc.volume			= soundstatic.volume;
				desc.loop			= loop;
				desc.delay			= delay;
				desc.time			= Time.time;

				if( loop )
				{
					GameObject gameObject = new GameObject();
					Transform transform = gameObject.transform;

					transform.SetParent(parent);
					desc.soundbehaviour	= gameObject.AddComponent(typeof(SoundBehaviour)) as SoundBehaviour;
				}

				app.Download.ON( resource.AssetBundle(), funcPlay, desc, resource.AssetBundle() );
				return desc.soundbehaviour;
			}
		}

		return null;
	}

	//사운드를 재생하기 위한 함수
	public SoundBehaviour Play( tagSoundStatic soundstatic, float delay, Transform parent=null )
	{
		if( soundstatic==null ) return null;
//		if( parent==null ) return null;	//(NULL)값을 허용함

		return Play( soundstatic, false, delay, parent );
	}

	//사운드를 재생하기 위한 함수
	public void funcPlay( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(SoundDesc) ) return;
		if( lParam==null || lParam as tagAssetBundle==null ) return;

		SoundDesc		desc			= (SoundDesc)wParam;
		tagAssetBundle	assetBundle		= lParam as tagAssetBundle;

		if( assetBundle.bundle!=null && Library.Is(desc.tag) )
		{
			AudioClip clip = assetBundle.bundle.LoadAsset( desc.tag, typeof(AudioClip) ) as AudioClip;
			if( clip!=null )
			{
				app.Sound.Fx( clip, desc.volume, desc.loop, desc.delay-Mathf.Max( 0f, Time.time-desc.time ), desc.soundbehaviour );
			}
#if UNITY_EDITOR
			else
			{
				Debug.Log( "사운드 정보를 찾을 수 없습니다. : "+desc.soundstatic+", "+desc.tag );
			}
#endif

			if( desc.soundstatic!=null )
			{
				desc.soundstatic.clip = clip;
			}
		}
	}

	//사운드를 재생하기 위한 함수
	public void Play( string id, float delay=0f )
	{
		if( !Library.Is(id) ) return;
		Play( Find(id), delay );
	}

	//사운드를 재생하기 위한 함수
	public void Play( tagSoundStatic soundstatic, string name, bool loop=false, float delay=0f )
	{
		if( soundstatic==null ) return;
		if( !Library.Is(name) ) return;
//		if( parent==null ) return; //(NULL)값을 허용함

		app.Sound.Play( Load( soundstatic, name ), soundstatic.volume, loop, delay );
	}

	//사운드를 재생하기 위한 함수
	public void Play( string id, string name, bool loop=false, float delay=0f )
	{
		if( !Library.Is(id) ) return;
		if( !Library.Is(name) ) return;
//		if( parent==null ) return;	//(NULL)값을 허용함

		Play( Find(id), name, loop, delay );
	}

	//리소스를 불러오기 위한 함수
	public AudioClip Load( tagSoundStatic soundstatic, string src )
	{
		if( soundstatic==null ) return null;
		if( !Library.Is(src) ) return null;

		if( soundstatic.AssetBundle().bundle!=null )
		{
			return soundstatic.AssetBundle().bundle.LoadAsset( src, typeof(AudioClip) ) as AudioClip;
		}

		return null;
	}

	//리소스를 불러오기 위한 함수
	public AudioClip Load( string resource, string src )
	{
		if( !Library.Is(resource) ) return null;
		if( !Library.Is(src) ) return null;

		return Load( Find(resource), src );
	}

	public override void Download( tagAppsItem appsitem, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( appsitem==null ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		base.Download( appsitem, func, wParam, lParam );

		if( Library.Is(appsitem.resource) && Library.Is(appsitem.tag) )
		{
			tagResource resource = app.Resource.Find(appsitem.resource);
			if( resource!=null )
			{
				resource.Download( funcDownloadEnd, new SoundDesc(appsitem as tagSoundStatic, resource), new tagCallback( func, wParam, lParam ) );
			}
		}
	}

	void funcDownloadEnd( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam as SoundDesc==null ) return;
		if( lParam==null || lParam.GetType()!=typeof(tagCallback) ) return;

		SoundDesc desc = wParam as SoundDesc;
		tagSoundStatic soundstatic = desc.soundstatic;
		tagResource resource = desc.resource;
		tagCallback callback = lParam as tagCallback;

		if( resource!=null && resource.AssetBundle().IsLoad() )
		{
			soundstatic.clip = resource.AssetBundle().bundle.LoadAsset( soundstatic.tag, typeof(AudioClip) ) as AudioClip;
		}

		if( callback.Is() )
		{
			callback.Call();
		}
	}

	//아이템 정보를 얻기 위한 함수
	public override void OnLoadCache( tagAppsItem appsitem, AppsParameter col=null )
	{
		if( appsitem==null ) return;
//		if( col==null ) return; //(NULL)값을 허용함

		tagSoundStatic soundstatic = appsitem as tagSoundStatic;
		if( soundstatic==null ) return;

		if( col!=null )
		{
#if UNITY_EDITOR
			if( !col.Is("volume") )		Debug.Log("사운드 볼륨이 설정되어 있지 않습니다.");
			if( !col.Is("interval") )	Debug.Log("사운드 Interval이 설정되어 있지 않습니다.");
#endif

			soundstatic.volume		= col.GetFloat("volume");
			soundstatic.interval	= col.GetFloat("interval");
		}

		if( !GetList().Contains(soundstatic) )
		{
			GetList().Add(soundstatic);
		}
	}

	//XML 파일을 저장하기 위한 함수
	public override void OnXmlSave( XmlTextWriter xmlWriter, tagAppsItem appsitem )
	{
		if( xmlWriter==null ) return;
		if( appsitem==null ) return;

		tagSoundStatic soundstatic = appsitem as tagSoundStatic;
		if( soundstatic==null ) return;

		CXml.WriteNodeValue( xmlWriter, "volume",	soundstatic.volume );
		CXml.WriteNodeValue( xmlWriter, "interval",	soundstatic.interval );
	}

	//XML 파일을 불러오기 위한 함수
	public override void OnXmlLoad( XmlNode pNode, tagAppsItem appsitem )
	{
 		if( pNode==null ) return;
		if( appsitem==null ) return;

		tagSoundStatic soundstatic = appsitem as tagSoundStatic;
		if( soundstatic==null ) return;

		soundstatic.volume		= CXml.GetChildFloat( pNode, "volume" );
		soundstatic.interval	= CXml.GetChildFloat( pNode, "interval" );
	}
}