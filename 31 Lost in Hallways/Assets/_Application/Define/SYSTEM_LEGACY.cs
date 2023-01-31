using UnityEngine;

public class SYSTEM_LEGACY
{
	public static string	UNITY_VERSION					= (Application.unityVersion);

	public const float		SLIM_SCREEN_RATE				= (1080f) / (1920f);	//9:16 화면 비

//	public const float		TOP_NAVIGATION_HEIGHT			= (120);

	public const string		GOOGLE_API_KEY					= "AIzaSyDLXWgCYCgaTyusdDvcKEjJz0vgI0T8SEM";

	public const float		UNITY_TO_METER					= (16.0934f);

	public const float		COMMAND_DISTANCE				= (10f);		//명령을 허용할 거리

	public const float		PROGRESS_SECOND					= (1f);			//인터페이스 조작에 대기할 시간
	public const float		DRAG_DISTANCE					= (20f);		//드래그 인식 거리
	public const float		TOUCH_TIME						= (0.1f);		//빠른 터치 인식 타임
	public const float		DEFAULT_COMMENT_TIME			= (3f);

	public const float		BUILD_ALLOW_DISTANCE			= (3f);
	public const float		STRUCTURE_RADIUS				= (3f);
	public const float		STATIC_RADIUS					= (2f);

	public const float		INVENTORY_BLOCK_WIDTH			= (100f);
	public const float		INVENTORY_BLOCK_HEIGHT			= (INVENTORY_BLOCK_WIDTH);

	public const string		MATERIAL_GRAY_PATH				= "Materials/Gray";
	public const string		MATERIAL_BUILD_EDIT				= "Materials/BuildEdit/BuildEdit";
	public const string		MATERIAL_BUILD_EDIT_ERROR		= "Materials/BuildEdit/BuildEditError";

	public const int		TEXTURE_ENCODE_DENSITY			= 1024;
	public const int		FILE_LOAD_BUFFER				= 1024;

	public const float		FAR_MESSAGE_COMEBACK_TIME		= (1.5f);		//'명령 허용 범위를 벗어남' 메세지를 출력 후 현재 위치로 되돌아오기까지의 대기 시간
	public const float		COIN_COLLECT_DELAY				= (0.225f);		//코인 획득 효과 시, 코인 움직임의 싱크를 맞추기 위해 지연할 시간
	public const float		NEW_ITEM_ALERT_TIME				= (3f);			//아이템 획득 알림을 노출할 시간
	public const float		GRAPH_ACTIVE_DISTANCE			= (100f);		//그래프 객체를 표시할 가시 거리

	public const int		ITEM_TEXTURE_BLOCK_SIZE			= (128);

	public static int		BUSINESS_BUILD_LIMIT			= (0);

#if UNITY_EDITOR
	public const string		URL_NAVER_CAFE					= "https://ressbi08.cafe24.com/naver-cafe";
	public const string		PRIVACY_POLICY					= "https://ressbi08.cafe24.com/privacy-policy";
	public const string		TERMS_OF_USE					= "https://ressbi08.cafe24.com/terms-of-use";
#else
	public const string		URL_NAVER_CAFE					= "https://latte02.cafe24.com/naver-cafe";
	public const string		PRIVACY_POLICY					= "https://latte02.cafe24.com/privacy-policy";
	public const string		TERMS_OF_USE					= "https://latte02.cafe24.com/terms-of-use";
#endif
}