using System.Collections.Generic;
using UnityEngine;

//배경음악 정보를 처리하기 위한 클래스
public class CBgsound : FrameworkBehaviour
{
	private List<BgsoundBehaviour> Bgsounds = new List<BgsoundBehaviour>();
	private BgsoundBehaviour bgsound = null;

	private const float constVolume = 0.5f;

	// 배경음악을 재생하기 위한 함수
	public SoundBehaviour Play( AudioClip clip, float volume=1f, bool loop=true )
	{
		if( clip==null ) return null;

		if( IsPlay() && Get().Clip()==clip )
		{
			return Get();
		}

		foreach( BgsoundBehaviour bgsound0 in GetList() )
		{
			if( bgsound0.IsPlay() )
			{
				bgsound0.Out();
			}
		}

		BgsoundBehaviour bgsound = app.Sound.Play( clip, typeof(BgsoundBehaviour) ) as BgsoundBehaviour;
		if( bgsound!=null )
		{
			bgsound.fVolume = volume * GetMasterVolume() * constVolume;
			bgsound.AudioSource().volume = 0f;
			bgsound.AudioSource().loop = loop;
			bgsound.In();

			GetList().Add( bgsound );
			this.bgsound = bgsound;
		}

		return bgsound;
	}

	//배경음악을 재생하기 위한 함수
	public void Play()
	{
		if( Get()!=null )
		{
			Get().Play();
		}
	}

	//재생을 중단하기 위한 함ㅅ
	public void Stop()
	{
		if( Get()!=null )
		{
			Get().Stop();
		}
	}

	//사운드 정보를 초기화 하기 위한 함수
	public void Release( BgsoundBehaviour sound )
	{
		if( sound==null ) return;

		if( Apps.Is(sound.Static()) && sound.Static().AssetBundle()!=null )
		{
			sound.Static().AssetBundle().Untake( sound );
		}

		if( Get()==sound )
		{
			this.bgsound = null;
		}

		GetList().Remove( sound );
	}

	//플레이 중인지 확인하기 위한 함수
	bool IsPlay()
	{
		if( Get()!=null && Get().IsPlay() )
		{
			return true;
		}

		return false;
	}

	//플레이 중인지 확인하기 위한 함수
	public bool IsPlay( AudioClip clip )
	{
		if( clip==null ) return false;

		foreach( BgsoundBehaviour bgsound in GetList() )
		{
			if( !bgsound.IsOut() && bgsound.IsPlay() && bgsound.Clip()==clip )
			{
				return true;
			}
		}

		return false;
	}

	//리스트를 얻기 위한 함수
	public List<BgsoundBehaviour> GetList()
	{
		return Bgsounds;
	}

	//배경음악 정보를 얻기 위한 함수
	public SoundBehaviour Get()
	{
		return bgsound;
	}

	//마스터 음량을 얻기 위한 함수
	float GetMasterVolume()
	{
		return 1f;
	}
}