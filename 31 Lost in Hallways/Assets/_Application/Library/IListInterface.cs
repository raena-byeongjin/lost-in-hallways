using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------------------------------------------------------------------------------
// -
//-----------------------------------------------------------------------------------------------------------------------------------
public interface IListInterface
{
	public bool IList_AllowList( object param );
	public void IList_Select( ViewPanel panel, object wParam=(null), object lParam=(null) );
	public void IList_OnList( ViewPanel panel, object param );
	public void IList_LoadList( int page, object wParam=(null), object lParam=(null) );
}

//-----------------------------------------------------------------------------------------------------------------------------------
// -
//-----------------------------------------------------------------------------------------------------------------------------------