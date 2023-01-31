﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------------------------------------------------------------------------------
// 이펙트 정보를 처리하기 위한 클래스
//-----------------------------------------------------------------------------------------------------------------------------------
public class CEffect : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	[System.Serializable]
	public class tagData
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public List<objEffect>		Effects		= new List<objEffect>();

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	};
	public tagData		data		= new tagData();

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	protected CApp		app			= (null);
	protected CPlay		play		= (null);

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	void OnEnable()
	{
		(this.app)		= (CApp.This);
		(this.play)		= (CPlay.This);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
    // 이벡트를 생성하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public objEffect ON( GameObject sample, Vector3 position=new Vector3(), Vector3 rotation=new Vector3(), Vector3 scale=new Vector3(), Transform parent=(null) )
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (sample)==(null) ) return (null);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (parent)==(null) )
		{
			(parent)	= (play.layerEffects);
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (scale)==new Vector3() )
		{
			(scale)		= new Vector3( (1), (1), (1) );
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		Transform		transform		= Func.Create( (parent), (position), new Vector3(), (scale), (sample) );
		if( (transform)!=(null) )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			(transform.localPosition)		= (position);
			(transform.localEulerAngles)	= (rotation);
			(transform.localScale)			= (scale);

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		GameObject gameObject = (transform.gameObject);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		objEffect effect = (gameObject.AddComponent(typeof(objEffect)) as objEffect);
		if( (effect)!=(null) )
		{
			GetList().Add( effect );
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		Library.MaterialSetup( gameObject );
		app.Asset.AssetComponent( (transform), (gameObject) );

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return (effect);
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// Coin 리스트를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public List<objEffect> GetList()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.Effects);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 메모리를 초기화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Release()
	{
	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		foreach( objEffect effect in GetList() )
		{
			GameObject.Destroy( effect._GameObject() );
		}

	    //---------------------------------------------------------------------------------------------------------------------------
        // -
	    //---------------------------------------------------------------------------------------------------------------------------
		GetList().Clear();

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