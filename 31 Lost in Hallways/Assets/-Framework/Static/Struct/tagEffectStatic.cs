using System;
using UnityEngine;

//이펙트 정보를 처리하기 위한 구조체
public class tagEffectStatic : tagAppsItem
{
	private GameObject sample = null;

	//다운로드를 처리하기 위한 함수
	public override void Download( Action<object, object> func=null, object wParam=null, object lParam=null )
	{
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( Library.Is(resource) )
		{
			tagResource assetitem = ApplicationBehaviour.This.Resource.Find(resource);
			if( assetitem!=null )
			{
				assetitem.Download( funcDownloadEnd );
			}
		}
		else
		if( AssetBundle().Is() && eventListener!=null )
		{
			eventListener.Download( this, funcDownloadEnd );

			if( func!=null )
			{
				DownloadInterface.SetCallback( func, wParam, lParam );
			}
		}
	}

	void funcDownloadEnd( object wParam=null, object lParam=null )
	{
//		if( lParam==null || lParam.GetType()!=typeof(tagResource) ) return;	//(NULL)값을 허용함

		if( lParam!=null && lParam.GetType()==typeof(tagResource) )
		{
			tagResource resource = lParam as tagResource;
			if( resource!=null && resource.AssetBundle()!=null && resource.AssetBundle().IsLoad() )
			{
				sample = resource.AssetBundle().bundle.LoadAsset( tag, typeof(GameObject) ) as GameObject;
				if( sample!=null )
				{
					Library.MaterialSetup(sample);
				}
			}
		}
		else
		if( lParam!=null && lParam as tagAppsItem!=null )
		{
			tagAppsItem appsitem = lParam as tagAppsItem;
			if( appsitem!=null && appsitem.AssetBundle()!=null && appsitem.AssetBundle().IsLoad() )
			{
				sample = appsitem.AssetBundle().GetMainAsset();
				if( sample!=null )
				{
					Library.MaterialSetup(sample);
				}
			}
		}
	}

	//이펙트를 생성하기 위한 함수
	public Transform ON( Vector3 position, Transform parent=null )
	{
//		if( parent==null ) return null; //(NULL)값을 허용함

		if( sample==null && AssetBundle().Is() )
		{
			Download( funcON, position, parent );
			return null;
		}

		return Effect.ON( position, sample, parent );
	}

	void funcON( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(Vector3) ) return;
//		if( lParam==null || lParam.GetType()!=typeof(Transform) ) return; //(NULL)값을 허용함

		if( lParam!=null && lParam.GetType()==typeof(Transform) )
		{
			ON( (Vector3)wParam, lParam as Transform );
		}
		else
		{
			ON( (Vector3)wParam );
		}
	}
}