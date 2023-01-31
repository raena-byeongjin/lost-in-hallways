using UnityEngine.UI;
using UnityEngine;

public class UIDestroy : MonoBehaviour
{
	void Awake()
	{
		Library.Destroy( GetComponent(typeof(Graphic)) );
		Library.Destroy( GetComponent(typeof(CanvasRenderer)) );
	}
}