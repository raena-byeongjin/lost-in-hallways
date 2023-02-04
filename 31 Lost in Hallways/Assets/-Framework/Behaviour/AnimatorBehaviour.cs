using UnityEngine;

public class AnimatorBehaviour : MonoBehaviour
{
	private Animator m_animator = null;
	private float preNormalize = 0f;

	void Awake()
	{
		m_animator = GetComponentInChildren(typeof(Animator)) as Animator;
		Enable( false );
	}

	void Update()
	{
		AnimatorStateInfo animatorStateInfo = Animator().GetCurrentAnimatorStateInfo(0);
		if( preNormalize>=1f && animatorStateInfo.normalizedTime>=1f && !animatorStateInfo.loop )
		{
#if UNITY_EDITOR
			Debug.Log(this, this);
#endif
		}

		preNormalize = animatorStateInfo.normalizedTime;
	}

	//애니메이터를 설정하기 위한 함수
	public void Set( RuntimeAnimatorController animatorcontroller )
	{
//		if( animatorcontroller==null ) return;	//(NULL)값을 허용함

		if( Animator().runtimeAnimatorController!=animatorcontroller )
		{
			Animator().runtimeAnimatorController = animatorcontroller;
			Enable();
		}
	}

	//애니메이터 객체를 얻기 위한 함수
	public Animator Animator()
	{
		return m_animator;
	}

	//애니메이터 컨트롤러를 얻기 위한 함수
	public RuntimeAnimatorController GetAnimatorController()
	{
		return Animator().runtimeAnimatorController;
	}

	//활성화를 설정하기 위한 함수
	void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}
}