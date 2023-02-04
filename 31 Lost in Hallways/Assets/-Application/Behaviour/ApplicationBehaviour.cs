using System;

public class ApplicationBehaviour : CApp
{
	protected override void Start()
	{
		base.Start();

		ViewLoading.ON( this );
//		ViewLoading.ON( (ManagementAppsEventListener.This), Language.Get(TEXT.서버에_연결하는_중입니다) );
//		ViewLoading.ON( Account );
//		ViewLoading.ON( SceneCharacterSelect );
//		ViewLoading.ON( (ViewCharacterSelect), Language.Get(TEXT.이세계_용사를_소환_중입니다) );
//		ViewLoading.ON( (ViewApplicationContract), Language.Get(TEXT.전문_경영인의_출근을_확인중입니다) );
//		ViewLoading.ON( Download );
//		ViewLoading.SetCallback( SceneCharacterSelect.funcGameStart );
//		Download.Task( ViewCharacterSelect );
//		ViveAppsEventListener.This.Startup();
	}

	[NonSerialized] public CProc Proc = null;
	[NonSerialized] public CCharacter Character = null;

	[NonSerialized] public CSafeClass SafeClass = null;

	[NonSerialized] public CCharacterStatic CharacterStatic = null;

	protected override void Awake()
	{
		base.Awake();

		Proc = GetComponentInChildren(typeof(CProc)) as CProc;
		Character = GetComponentInChildren(typeof(CCharacter)) as CCharacter;

		SafeClass = GetComponentInChildren(typeof(CSafeClass)) as CSafeClass;

		CharacterStatic = GetComponentInChildren(typeof(CCharacterStatic)) as CCharacterStatic;
	}
}