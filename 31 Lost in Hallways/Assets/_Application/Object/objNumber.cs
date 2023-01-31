﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//-----------------------------------------------------------------------------------------------------------------------------------
// -
//-----------------------------------------------------------------------------------------------------------------------------------
public class objNumber : MonoBehaviour
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
		public Transform		transform		= (null);
		public GameObject		gameObject		= (null);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public int				value			= (0);
		public Color			color			= new Color();

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		public Image			image			= (null);
		public List<Image>		Samples			= new List<Image>();
		public List<Sprite>		Sprites			= new List<Sprite>();

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	};
	public tagData	data	= new tagData();

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	void Awake()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(data.transform)	= (transform);
		(data.gameObject)	= (gameObject);
		(data.image)		= (GetComponentInChildren(typeof(Image)) as Image);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 인터페이스를 활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void ON()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		_GameObject().SetActive( true );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 인터페이스를 비활성화 하기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public void OFF()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		_GameObject().SetActive( false );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Set( int value, Color color )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (_GameObject().activeSelf) && (data.value)==(value) && (data.color)==(color) )
		{
			return;
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		(data.value)	= (value);
		(data.color)	= (color);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Clear();

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		string	strValue	= (value.ToString());
		int		End			= (strValue.Length);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		for( int i=0; i<(End); i++ )
		{
			Create( strValue[i].ToString(), (color) );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		RectTransform	sample	= (data.image.rectTransform);
		for( int i=0; i<(data.Samples.Count); i++ )
		{
			(data.Samples[i].rectTransform.localPosition)	= new Vector3( (-sample.sizeDelta.x/2)*(data.Samples.Count)+(sample.sizeDelta.x)*(i)+(sample.sizeDelta.x/2), (0), (0) );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Library.SetLayer( Transform(), (_GameObject().layer) );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		ON();

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Set( int value )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Set( (value), (Color.white) );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Create( string Char, Color color )
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( !Library.Is(Char) ) return;

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		GameObject		obj				= GameObject.Instantiate(data.image.gameObject);
		Transform		transform		= (obj.transform);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		transform.SetParent( Transform() );
		(transform.localPosition)		= new Vector3();
		(transform.localScale)			= new Vector3( (1), (1), (1) );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Image	image	= (obj.GetComponent(typeof(Image)) as Image);
		if( (image)!=(null) )
		{
			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			(image.color)	= (color);

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
			switch( Char )
			{
				case "0":
				case "1":
				case "2":
				case "3":
				case "4":
				case "5":
				case "6":
				case "7":
				case "8":
				case "9":
					(image.sprite)	= (data.Sprites[int.Parse(Char)]);
					break;
			}

			//-----------------------------------------------------------------------------------------------------------------------
			// -
			//-----------------------------------------------------------------------------------------------------------------------
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		if( (image)!=(null) )
		{
			data.Samples.Add( image );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		Func.Active( obj );

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public void Clear()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		foreach( Image image in data.Samples )
		{
			GameObject.Destroy( image.gameObject );
		}

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		data.Samples.Clear();

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 트랜스 폼 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public Transform Transform()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.transform);

		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	// 게임 오브젝트 객체를 얻기 위한 함수
	//-------------------------------------------------------------------------------------------------------------------------------
	public GameObject _GameObject()
	{
		//---------------------------------------------------------------------------------------------------------------------------
		// -
		//---------------------------------------------------------------------------------------------------------------------------
		return (data.gameObject);

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