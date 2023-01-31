using UnityEngine;

public class DefaultScaleBehaviour : MonoBehaviour
{
	private Vector3	vDefault = new Vector3();

    void Awake()
    {
        vDefault = transform.localScale;
    }

	//원래 크기를 얻기 위한 함수
	public Vector3 GetDefaultScale()
	{
		return vDefault;
	}
}