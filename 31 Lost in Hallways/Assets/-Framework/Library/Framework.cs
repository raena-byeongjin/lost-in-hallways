public class Framework
{
	//카메라 객체를 얻기 위한 함수
	public static CameraBehaviour Camera()
	{
		return PlayBehaviour.This.mainCamera;
	}

	//카메라를 설정하기 위한 함수
	public static void Set( CameraBehaviour camerabehaviour )
	{
//		if( camerabehaviour==null ) return; //(NULL)값을 허용함
		PlayBehaviour.This.mainCamera = camerabehaviour;
	}
}