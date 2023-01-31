using UnityEngine;

public class DestroyBehaviour : MonoBehaviour
{
	public float delay = 0;

	void Awake()
	{
		if( delay<=0f )
		{
			ParticleSystem particle = GetComponentInChildren(typeof(ParticleSystem)) as ParticleSystem;
			if( particle!=null )
			{
				delay = particle.main.duration;
			}
		}
	}

	void Update()
	{
		this.delay -= Time.deltaTime;

		if( this.delay<=0 )
		{
			GameObject.Destroy(gameObject);
		}
	}
}