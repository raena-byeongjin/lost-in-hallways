﻿using UnityEngine.EventSystems;
using UnityEngine;

//포커스 정보를 처리하기 위한 클래스
public class tagFocus
{
    public object	p		= null;
	public float	time	= 0f;
	public Vector2	point	= new Vector2();
	public Vector2	start	= new Vector2();
	public float	length	= 0f;

	public TOUCH_INDEX index;

    //포커스를 설정하기 위한 함수
    public void Set( object obj, Vector2 point=new Vector2() )
    {
        this.p		= obj;
		this.time	= Time.time;
		this.point	= point;
    }

    //포커스를 설정하기 위한 함수
    public void Set( object obj, PointerEventData pointer )
    {
		Set( obj, pointer.position );
    }

	//좌표 값을 설정하기 위한 함수
    public void Set( Vector2 point )
    {
		this.point = point;
    }

    //포커스를 초기화 하기 위한 함수
    public void Reset()
    {
		p		= null;
		time	= Time.time;
		length	= 0f;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    // 포커스를 확인하기 위한 함수
    //-------------------------------------------------------------------------------------------------------------------------------
    public bool Is( object obj )
    {
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		if( (obj)==(null) ) return false;

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
        if( (this.p)==(obj) ) 
        {
            return true;
        }

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
        return false;
    }

	//포커스를 확인하기 위한 함수
    public bool Is()
    {
        if( p!=null ) 
        {
            return true;
        }

        return false;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    // 포커스를 확인하기 위한 함수
    //-------------------------------------------------------------------------------------------------------------------------------
    public object Get()
    {
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
        return (this.p);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    // 타입을 얻기 위한 함수
    //-------------------------------------------------------------------------------------------------------------------------------
    public System.Type Type()
    {
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
        if( (this.p)==(null) ) return (null);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
        return (p.GetType());
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    // 타입을 얻기 위한 함수
    //-------------------------------------------------------------------------------------------------------------------------------
    public bool IsType( System.Type type )
    {
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
        if( Type()==(type) )
		{
			return true;
		}

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
        return false;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    // 입력한 시간을 얻기 위한 함수
    //-------------------------------------------------------------------------------------------------------------------------------
	public float GetTime()
	{
        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
		return (Time.time)-(this.time);

        //---------------------------------------------------------------------------------------------------------------------------
        // -
        //---------------------------------------------------------------------------------------------------------------------------
	}

    //드래그 거리를 확인하기 위한 함수
	public bool IsDrag()
	{
		if( length>=Func.GetDragDistance() )
		{
			return true;
		}

		return false;
	}

	public override string ToString()
	{
 		return index+" : "+p+" ("+base.ToString()+")";
	}
}