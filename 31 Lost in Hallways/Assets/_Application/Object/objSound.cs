using UnityEngine;

//사운드 재생을 처리하기 위한 함수
public class objSound : TransformBehaviour
{
	protected tagSoundStatic soundstatic = null;
	protected AudioSource audioSource = null;

	protected override void Awake()
	{
		base.Awake();
		audioSource = GetComponentInChildren(typeof(AudioSource)) as AudioSource;
	}

	//오디오 객체를 얻기 위한 함수
	public AudioSource AudioSource()
	{
		return audioSource;
	}

	//사운드 정보를 얻기 위한 함수
	public tagSoundStatic Static()
	{
		return soundstatic;
	}

	//클립을 얻기 위한 함수
	public AudioClip Clip()
	{
		if( AudioSource()!=(null) )
		{
			return AudioSource().clip;
		}

		return null;
	}

	//사운드를 재생하기 위한 함수
	public void Play()
	{
		if( AudioSource()!=null )
		{
			AudioSource().Play();
		}
	}

	//재생을 중단하기 위한 함수
	public void Stop()
	{
 		if( AudioSource()!=null )
		{
			AudioSource().Stop();
		}
	}

	//플레이 중인지 확인하기 위한 함수
	public bool IsPlay()
	{
		if( AudioSource()!=null && AudioSource().isPlaying )
		{
			return true;
		}

		return false;
	}
}