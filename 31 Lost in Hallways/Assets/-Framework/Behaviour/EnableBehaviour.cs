using UnityEngine;

public class EnableBehaviour : MonoBehaviour
{
    public ENABLE nAction = ENABLE.START;
    public bool enable = false;

	void Awake()
    {
	    if( nAction==ENABLE.AWAKE )
        {
	        Enable();
        }
	}

	void Start()
    {
	    if( nAction==ENABLE.START || nAction==ENABLE.NOTHING )
        {
            Enable();
        }
	}

	void Update()
    {
	    if( nAction==ENABLE.UPDATE )
        {
	        Enable();
        }
	}

	void Enable()
    {
	    gameObject.SetActive( enable );
        Component.Destroy( this );
	}
}