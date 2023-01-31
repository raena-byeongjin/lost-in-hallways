using UnityEngine;
using System.Collections;
using System;

//어플리케이션 정보를 처리하기 위한 클래스
public class CPlay : MonoBehaviour
{
	public static PlayBehaviour	This = null;

	void Awake()
	{
		This = this as PlayBehaviour;
	}

	public SCENE				Scene;
	public VIEW					View;

	public float				ScreenSize			= (1f);

	public tagFocus				Focus				= new tagFocus();
	public tagFocus				Rollover			= new tagFocus();

	public Vector3				mousePosition		= new Vector3();

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public int					drag_x, drag_y;
	public float				drag_length;
	public float				DragStartLength;
	public float				ZoomStartValue;
	public int					pickingLayerMask;

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public int					Day					= (0);
	public float				Time				= (0);
//	public int					preDayUpdate		= (Func.Now().Day);

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public float				deltaTime			= (0);
	public long					serverTime			= (0);
	public float				fServerTime			= (0);
	public DateTime				utcTime				= new DateTime();
	public DateTime				preDateTime			= new DateTime();
	public float				Hour				= (0);

	//-------------------------------------------------------------------------------------------------------------------------------
	// -
	//-------------------------------------------------------------------------------------------------------------------------------
	public string				country				= null;
	public tagCurrencyStatic	dallor				= null;

	public tagSourceCode		Code				= new tagSourceCode();

	public Transform			_UIRoot				= null;

	public CameraBehaviour		mainCamera			= null;

	public CanvasBehaviour		foreCanvas			= null;
	public CanvasBehaviour		forwardCanvas		= null;
	public CanvasBehaviour		uiCanvas			= null;
	public CanvasBehaviour		backCanvas			= null;
	public CanvasBehaviour		sceneCanvas			= null;

	public Transform			_System				= null;
	public Transform			_Plugins			= null;
	public Transform			_Scene				= null;
	public Transform			layerMaps			= null;
	public Transform			layerPlayers		= null;
	public Transform			layerCharacters		= null;
	public Transform			layerStructures		= null;
	public Transform			layerObjects		= null;
	public Transform			layerTrees			= null;
	public Transform			layerRocks			= null;
	public Transform			layerCoins			= null;
	public Transform			layerEffects		= null;
	public Transform			layerWayPoints		= null;
	public Transform			layerRoads			= null;
	public Transform			layerParticles		= null;

	public bool					LoadEnd				= false;
	public bool					allowUserInput		= true;
}