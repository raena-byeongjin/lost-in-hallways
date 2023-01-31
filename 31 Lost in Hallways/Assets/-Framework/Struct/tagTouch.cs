using UnityEngine;

public class tagTouch : tagFocus
{
	public bool		is_push		= false;
	public Vector2	position	= new Vector2();

	public override string ToString()
	{
		return base.ToString()+" : "+position.x+", "+position.y;
	}
}