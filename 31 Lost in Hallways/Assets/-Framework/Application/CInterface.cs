using System.Collections.Generic;

public class CInterface : FrameworkBehaviour
{
	private List<InterfaceBehaviour> Interfaces = new List<InterfaceBehaviour>();

	//취소 입력에 반응하기 위한 함수
	public bool ONCANCEL( CANCEL Cancel )
	{
		foreach( InterfaceBehaviour interfacebehaviour in GetList() )
		{
			if( interfacebehaviour.ONCANCEL(Cancel) )
			{
				return true;
			}
		}

		return false;
	}

	//리턴 입력에 반응하기 위한 함수
	public bool ONRETURN()
	{
		foreach( InterfaceBehaviour interfacebehaviour in GetList() )
		{
			if( interfacebehaviour.ONRETURN() )
			{
				return true;
			}
		}

		return false;
	}

	//Skip 입력에 반응하기 위한 함수
	public bool ONSKIP()
	{
		foreach( InterfaceBehaviour interfacebehaviour in GetList() )
		{
			if( interfacebehaviour.ONSKIP() )
			{
				return true;
			}
		}

		return false;
	}

	//인터페이스를 등록하기 위한 함수
	public void Register( InterfaceBehaviour interfacebehaviour )
	{
		if( interfacebehaviour==null ) return;
		GetList().Add( interfacebehaviour );
	}

	//인터페이스를 해제하기 위한 함수
	public void Release( InterfaceBehaviour interfacebehaviour )
	{
		if( interfacebehaviour==null ) return;
		GetList().Remove( interfacebehaviour );
	}

	//리스트를 얻기 위한 함수
	List<InterfaceBehaviour> GetList()
	{
		return Interfaces;
	}
}