using UnityEngine;

//사운드 정보를 처리하기 위한 구조체
public class tagSoundStatic : tagAppsItem
{
	public AudioClip	clip		= null;

	public float		volume		= 1f;
	public float		interval	= 0f;

	public float		Latest		= 0f;	//마지막 재생 시간 (inverval 확인을 위한 파라메터)

	//사운드를 재생하기 위한 함수
	public virtual void ON( float delay )
	{
		ApplicationBehaviour.This.SoundStatic.Play( this, delay );
	}

	//사운드를 재생하기 위한 함수
	public virtual SoundBehaviour ON()
	{
		return ApplicationBehaviour.This.SoundStatic.Play( this );
	}

	//사운드를 재생하기 위한 함수
	public SoundBehaviour ON( bool loop, float delay=0f, Transform parent=null )
	{
		if( loop )
		{
			return ApplicationBehaviour.This.SoundStatic.Play( this, loop, delay, parent );
		}

		return ON();
	}
}