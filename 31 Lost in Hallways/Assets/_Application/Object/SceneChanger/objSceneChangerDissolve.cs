﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------------------------------------------------------------------------------
// 디졸브 장면 전환을 처리하기 위한 클래스
//-----------------------------------------------------------------------------------------------------------------------------------
public class objSceneChangerDissolve : SceneChangerBehaviour
{
	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	[System.Serializable]
	public class tagData : _tagData
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public Transform	child		= (null);
		public Material		material	= (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	};
	public tagData	data	= new tagData();

	//-------------------------------------------------------------------------------------------------------------------------------
	// 움직임을 처리하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	void Awake()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Init( data );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(data.material)		= ((data.child.GetComponent(typeof(Renderer)) as Renderer).material);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 움직임을 처리하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public override void Action()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( GetAction()==(ACTION.OUT) )
		{
			float	dissolve	= _Material().GetFloat("_DissolvePower")-(0.65f*Time.deltaTime);
			data.material.SetFloat( "_DissolvePower", (dissolve) ); 
			if( (dissolve)<=(0) )
			{
				OFF();
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 장면 전환을 시작하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public override void OnStart()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(data.child.localScale)		= new Vector3( play.uiCanvas.Width(), play.uiCanvas.Height(), (1) );
		(_Material().mainTexture)	= (data.texture);
		data.child.gameObject.SetActive( true );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 매트리얼 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public Material _Material()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.material);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
}

//-----------------------------------------------------------------------------------------------------------------------------------
// -
//-----------------------------------------------------------------------------------------------------------------------------------