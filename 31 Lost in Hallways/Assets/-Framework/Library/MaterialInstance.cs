using UnityEngine.UI;
using UnityEngine;

public class MaterialInstance : MonoBehaviour
{
	void Awake()
    {
        Graphic graphic = GetComponent(typeof(Graphic))  as Graphic;
		if( graphic!=null && !Library.IsDefaulUIMaterial(graphic.material) )
		{
			graphic.material = Library.Instance(graphic.material);
		}

		Component.Destroy(this);
    }
}
