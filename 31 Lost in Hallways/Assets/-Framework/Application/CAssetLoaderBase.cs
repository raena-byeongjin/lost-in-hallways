using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CAssetLoaderBase : FrameworkBehaviour
{
	public virtual void ON( Transform transform, GameObject gameObject )
	{
		if( transform==null ) return;
//		if( gameObject==null ) return;	//(NULL)값을 허용함

		if( gameObject==null )
		{
			gameObject = transform.gameObject;
		}

		Component[] comArray = transform.GetComponentsInChildren(typeof(AssetComponent));
		foreach( AssetComponent asset in comArray )
		{
			AssetComponent( asset, transform );
			Component.Destroy( asset );
		}
	}

	public void AssetComponent( AssetComponent asset, Transform root )
	{
		if( asset==null ) return;
		if( root==null ) return;

		Transform	transform	= asset.transform;
		GameObject	gameObject	= asset.gameObject;

		if( asset.xml==null )
		{
			if( !AssetComponent( asset, null, (XmlNode)null, transform, gameObject ) )
			{
#if UNITY_EDITOR
				Debug.Log( "정의되지 않은 Asset Component : "+asset.value, transform );
#endif
			}
		}
		else
		{
			XmlDocument xmlDoc = CXml.Load(asset.xml);
			if( xmlDoc==null ) return;

			XmlNodeList nodeList = xmlDoc.ChildNodes;
			XmlNode pNode = null;
			for( int i=0; i<nodeList.Count; i++ )
			{
				pNode = nodeList.Item(i);
				if( pNode.Name=="xml" ) continue;

				if( !AssetComponent( asset, pNode.Name, pNode, transform, gameObject ) )
				{
#if UNITY_EDITOR
					Debug.Log( "정의되지 않은 Asset Component : "+pNode.Name, transform );
#endif
				}
			}
		}
	}

	protected virtual bool AssetComponent( AssetComponent asset, string name, XmlNode pNode, Transform transform, GameObject gameObject )
	{
		if( asset==null ) return false;
		if( !Library.Is(name) ) return false;
//		if( pNode==null ) return false;			//(NULL)값을 허용함
		if( transform==null ) return false;
		if( gameObject==null ) return false;

		if( IsAssetComponent( asset, name, pNode, "RenderQueue" ) )
		{
			if( Library.IsNumber(asset.value) )
			{
				Renderer renderer = gameObject.GetComponent(typeof(Renderer)) as Renderer;
				if( renderer!=null )
				{
					foreach( Material mtrl in renderer.materials )
					{
						mtrl.renderQueue = int.Parse(asset.value);
					}
				}
			}

			return true;
		}
		else
		if( IsAssetComponent( asset, name, pNode, "Destroy" ) )
		{
			DestroyBehaviour destroy = gameObject.AddComponent(typeof(DestroyBehaviour)) as DestroyBehaviour;
			if( destroy!=null )
			{
				if( Library.IsSingle(asset.value) )
				{
					destroy.delay = float.Parse(asset.value);

					if( destroy.delay<=0f )
					{
						GameObject.Destroy( gameObject );
					}
				}
			}

			return true;
		}
		else
		{
			System.Type type = System.Type.GetType(name);
			if( type!=null )
			{
				AssetLoaderBehaviour assetloaderbehaviour = gameObject.AddComponent(type) as AssetLoaderBehaviour;
				if( assetloaderbehaviour!=null )
				{
					assetloaderbehaviour.Initialize( asset, pNode );
					return true;
				}
			}
		}

		return false;
	}

	protected bool IsAssetComponent( AssetComponent asset, string name, XmlNode pNode, string id )
	{
		if( asset==null ) return false;
		if( !Library.Is(name) ) return false;
//		if( pNode==null ) return false;		//(NULL)값을 허용함
		if( !Library.Is(id) ) return false;

		if( ( name==id ) || ( pNode!=null && pNode.Name==id ) || ( pNode==null && asset.value==id ) )
		{
			return true;
		}

		return false;
	}
}
