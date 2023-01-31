using UnityEngine;

public class SoundDesc
{
	public tagSoundStatic soundstatic = null;
	public tagResource resource = null;
	public SoundBehaviour soundbehaviour = null;

	public string		tag			= null;
	public float		volume		= 1f;
	public bool			loop		= false;
	public float		delay		= 0f;
	public float		time		= 0f; //다운로드에 걸린 시간을 딜레이 시간에서 보정하기 위한 파라메터

	public SoundDesc()
	{
	}

	public SoundDesc( float volume, bool loop )
	{
		this.volume = volume;
		this.loop = loop;
	}

	public SoundDesc( tagSoundStatic soundstatic, tagResource resource )
	{
		this.soundstatic = soundstatic;
		this.resource = resource;
	}
}