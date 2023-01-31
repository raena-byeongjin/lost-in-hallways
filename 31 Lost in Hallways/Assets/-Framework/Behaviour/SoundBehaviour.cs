using UnityEngine;

public class SoundBehaviour : objSound
{
	protected override void Awake()
	{
		base.Awake();
		Initialize();
	}

	public void Initialize()
	{
		audioSource = GetComponentInChildren(typeof(AudioSource)) as AudioSource;
	}

	protected virtual void OnDestroy()
	{
		ApplicationBehaviour.This.Sound.Release( this );
	}
}