using UnityEngine;

public class SoundComponent : TransformBehaviour
{
	private tagSoundStatic soundstatic = null;

	public string value = "Button";
	public float delay = 0f;
	public bool PlayOnEnable = false;
	public bool loop = false;

	private SoundBehaviour m_soundbehaviour = null;

	//사운드를 재생하기 위한 함수
	public void ON()
	{
		if( SoundBehaviour()!=null )
		{
			SoundBehaviour().Play();
		}
		else
		if( ApplicationBehaviour.IsStartup() )
		{
			if( soundstatic==null && Library.Is(value) )
			{
				soundstatic = ApplicationBehaviour.This.SoundStatic.FindFromAlias(value) as tagSoundStatic;
				if( soundstatic==null )
				{
					soundstatic = ApplicationBehaviour.This.SoundStatic.FindFromTag(value) as tagSoundStatic;
					if( soundstatic==null )
					{
						soundstatic = ApplicationBehaviour.This.SoundStatic.FindFromName(value) as tagSoundStatic;
					}
				}

				value = null;
			}

			if( soundstatic!=null )
			{
				m_soundbehaviour = soundstatic.ON( loop, delay, Transform() );
			}
		}
	}

	//사운드를 재생하기 위한 함수
	public void OFF()
	{
		if( SoundBehaviour()!=null )
		{
			SoundBehaviour().Stop();
		}
	}

	void OnEnable()
	{
		if( PlayOnEnable )
		{
			ON();
		}
	}

	public void OnClick()
	{
		if( UserInterface.Is() )
		{
			ON();
		}
	}

	//사운드 정보를 얻기 위한 함수
	public tagSoundStatic Static()
	{
		return soundstatic;
	}

	//사운드 객체를 얻기 위한 함수
	public SoundBehaviour SoundBehaviour()
	{
		return m_soundbehaviour;
	}

	//사운드 객체를 설정하기 위한 함수
	public void Set( SoundBehaviour soundbehaviour )
	{
		m_soundbehaviour = soundbehaviour;
	}
}