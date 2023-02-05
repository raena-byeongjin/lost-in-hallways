using UnityEngine;

//게임 정보를 처리하기 위한 클래스
public class CGame : CGameLegacy
{
	public override void ON()
	{
		StandardLoad();
	}

	void StandardLoad()
	{
		app.CharacterStatic.AllDownload();
	}
}