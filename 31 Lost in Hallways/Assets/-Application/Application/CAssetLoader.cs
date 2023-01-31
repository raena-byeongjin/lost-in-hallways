using System.Xml;
using UnityEngine;

public class CAssetLoader : CAssetLoaderBase
{
	public override void ON( Transform transform, GameObject gameObject )
	{
		if( transform==null ) return;
//		if( gameObject==null ) return;	//(NULL)값을 허용함

		base.ON( transform, gameObject );
	}

	protected override bool AssetComponent( AssetComponent asset, string name, XmlNode pNode, Transform transform, GameObject gameObject )
	{
		if( asset==null ) return false;
		if( !Library.Is(name) ) return false;
//		if( pNode==null ) return false;	//(NULL)값을 허용함
		if( transform==null ) return false;
		if( gameObject==null ) return false;

		if( base.AssetComponent( asset, name, pNode, transform, gameObject ) )
		{
			return true;
		}
		/*
		else
		if( IsAssetComponent( asset, name, pNode, "Humanoid-Head" ) )
		{
			return true;
		}
		else
		if( IsAssetComponent( asset, name, pNode, "Humanoid-RightHand" ) )
		{
			gameObject.AddComponent(typeof(objHandRight));
			return true;
		}
		else
		if( IsAssetComponent( asset, name, pNode, "Humanoid-LeftHand" ) )
		{
			return true;
		}
		else
		if( IsAssetComponent( asset, name, pNode, "Weapon-Handle" ) )
		{
			gameObject.AddComponent(typeof(objWeaponHandle));
			return true;
		}
		else
		if( IsAssetComponent( asset, name, pNode, "Weapon-Muzzle" ) )
		{
			gameObject.AddComponent(typeof(objWeaponMuzzle));
			return true;
		}
		else
		if( IsAssetComponent( asset, name, pNode, "Weapon" ) )
		{
			string weaponName = asset.value;
			if( !Library.Is(weaponName) )
			{
				weaponName = gameObject.name;
			}

			WeaponBehaviour weapon = null;
			if( Library.Is(weaponName) )
			{
				weapon = gameObject.AddComponent(System.Type.GetType(weaponName)) as WeaponBehaviour;
				if( weapon!=null )
				{
//					Debug.Log(weapon, weapon);
				}
			}

			if( weapon==null )
			{
				gameObject.AddComponent(typeof(WeaponBehaviour));

#if UNITY_EDITOR
				Debug.Log("올바르게 정의되지 않은 컴포넌트입니다. : "+asset.value, transform);
#endif
			}

			return true;
		}
		*/

		return false;
	}
}