using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//트랜스폼 정보를 처리하기 위한 구조체
public class tagCameraTransform : tagTransform
{
	public float viewSize = 0f;

	//트랜스폼 정보를 설정하기 위한 함수
	public void Set( Camera camera, Transform transform )
	{
		if( camera==null ) return;
		if( transform==null ) return;

		viewSize = GetViewSize(camera);
		Set( transform );
	}

	//트랜스폼을 비교하기 위한 함수
	public bool Contains( Camera camera, Transform transform )
	{
//		if( camera==null ) return false;		//(NULL)값을 허용함
		if( transform==null ) return false;

		if( camera!=null && viewSize!=GetViewSize(camera) )
		{
			return false;
		}

		if( !Is(transform) )
		{
			return false;
		}

		return true;
	}

	float GetViewSize( Camera camera )
	{
		if( camera==null ) return 0f;

		if( camera.orthographic )
		{
			return camera.orthographicSize;
		}

		return camera.fieldOfView;
	}
}