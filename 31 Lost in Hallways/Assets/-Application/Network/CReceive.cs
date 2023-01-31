using UnityEngine;

public class CReceive : ReceiveBehaviour
{
    //패킷 수신에 반응하기 위한 함수
    public override bool ON( string compile, AppsParameter col )
    {
        if( !Library.Is(compile) ) return false;
        if( col==null ) return false;

		if( base.ON( compile, col ) )
		{
			return true;
		}

		if( ONCommand( compile, col ) ) return true;

		switch( compile )
		{
			case "ConnectAuthenticode":
			case "Config":
			case "Time":
			case "ConnectOK":
			case "Version":
			case "ItemState":
			case "UpdateTime":
			case "UpdateEnd":
			case "Login":
			case "Level":
			case "Exp":
			case "Cash":
			case "Life":
			case "Score":
			case "Point":
			case "MemorizeBegin":
			case "MemorizeEnd":
			case "LoginEnd":
			case "Echo":
			case "Right":
			case "StorageBegin":
			case "StorageEnd":
			case "ClientMessage":
			case "Failed":
			case "Shutdown":
				break;

#if UNITY_EDITOR
			default:
				UnityEngine.Debug.Log(col);
				break;
#endif
		}

		return false;
    }

	bool ONCommand( string compile, AppsParameter col )
    {
		if( !Library.Is(compile) ) return false;
        if( col==null ) return false;

		/*
		switch( compile )
		{
			//자산 (Property)
			case "Property":
				Property( col );
				return true;

			case "Salary":
				Salary( col );
				return true;
		}
		*/

		return false;
    }
}