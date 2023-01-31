using UnityEngine;

public class HumanoidHand : HumanoidNode
{
	public Transform equipHelper = null;

	/*
	public void Equip( EquipBehaviour equipItem )
	{
		if( equipItem==null ) return;

		equipItem.Transform().SetParent( equipHelper );
		equipItem.Transform().localPosition = new Vector3();
		equipItem.Transform().localRotation = Quaternion.identity;
	}
	*/
}