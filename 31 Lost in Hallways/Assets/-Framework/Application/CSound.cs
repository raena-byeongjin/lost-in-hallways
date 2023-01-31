using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//사운드 정보를 처리하기 위한 클래스
public class CSound : FrameworkBehaviour
{
	private List<SoundBehaviour> PlayedSounds = new List<SoundBehaviour>();
	private AudioSource m_audioSource = null;

	protected override void Awake()
	{
		base.Awake();

		m_audioSource = GetComponentInChildren(typeof(AudioSource)) as AudioSource;
		if( m_audioSource==null )
		{
			m_audioSource = _GameObject().AddComponent(typeof(AudioSource)) as AudioSource;
		}
	}

	//사운드를 재생하기 위한 함수
	public void ON( tagSoundStatic soundstatic )
	{
		if( soundstatic==null ) return;

		if( !soundstatic.unavailable )
		{
			app.SoundStatic.Play( soundstatic );
		}
	}

	//사운드를 재생하기 위한 함수
	public SoundBehaviour Play( AudioClip clip, float volume=1f, bool loop=false, float delay=0f, SoundBehaviour soundbehaviour=null, System.Type type=null )
	{
		if( clip==null ) return null;
//		if( soundbehaviour==null ) return null; //(NULL)값을 허용함

		GameObject gameObject = null;
		if( soundbehaviour!=null )
		{
			gameObject = soundbehaviour._GameObject();
		}
		else
		{
			gameObject = new GameObject();
			gameObject.transform.SetParent( play._System );
		}

		AudioSource audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		if( audioSource!=null )
		{
			audioSource.clip	= clip;
			audioSource.loop	= loop;
			audioSource.volume	= volume*GetMasterVolume();

			if( delay>=0f )
			{
				audioSource.PlayDelayed(delay);
			}
			else
			{
				audioSource.Play();
			}
		}

		if( soundbehaviour==null )
		{
			if( type==null )
			{
				type = typeof(SoundBehaviour);
			}

			return gameObject.AddComponent(type) as SoundBehaviour;
		}

		if( soundbehaviour!=null )
		{
			soundbehaviour.Initialize();
		}

		return soundbehaviour;
	}

	//사운드를 재생하기 위한 함수
	public SoundBehaviour Play( AudioClip clip, System.Type type  )
	{
		if( clip==null ) return null;
		return Play( clip, 1f, false, 0f, null, type );
	}

	//효과음을 재생하기 위한 함수
	public SoundBehaviour Fx( AudioClip clip, float volume=1f, bool loop=false, float delay=0f, SoundBehaviour soundbehaviour=null )
	{
		if( clip==null ) return null;
//		if( soundbehaviour==null ) return null; //(NULL)값을 허용함

		if( delay>0f )
		{
			MessageQueue.ON( funcFx, clip, new SoundDesc( volume, loop ), delay );
			return null;
		}

		if( volume>0f && GetMasterVolume()>0f )
		{
			if( loop )
			{
				return Play( clip, volume, loop, delay, soundbehaviour );
			}
			else
			{
				AudioSource().PlayOneShot( clip, volume*GetMasterVolume() );
			}
		}

		return null;
	}

	//효과음을 재생하기 위한 함수
	void funcFx( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(AudioClip) ) return;
		if( lParam==null || lParam.GetType()!=typeof(SoundDesc) ) return;

		SoundDesc desc = (SoundDesc)lParam;
		if( desc!=null )
		{
			Fx( wParam as AudioClip, desc.volume, desc.loop );
		}
	}

	//재생중인지 확인하기 위한 함수
	public bool IsPlay( AudioClip audioClip )
	{
		if( audioClip==null ) return false;

		foreach( SoundBehaviour sound in GetList() )
		{
			if( sound.Clip()==audioClip )
			{
				return true;
			}
		}

		return false;
	}

	//사운드를 정보를 초기화 하기 위한 함수
	public void Release( SoundBehaviour sound )
	{
		if( sound==null ) return;
		GetList().Remove( sound );
	}

	//사운드를 재생하기 위한 함수
	public void funcPlay( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(tagSoundStatic) ) return;
		ON( wParam as tagSoundStatic );
	}

	//오디오 객체를 얻기 위한 함수
	AudioSource AudioSource()
	{
		return m_audioSource;
	}

	//마스터 음량을 얻기 위한 함수
	float GetMasterVolume()
	{
		return 1f;
	}

	//재생 리스트를 얻기 위한 함수
	public List<SoundBehaviour> GetList()
	{
		return PlayedSounds;
	}
}