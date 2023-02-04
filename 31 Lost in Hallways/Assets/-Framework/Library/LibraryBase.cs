using System.Text.RegularExpressions;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LibraryBase
{
	//문자열을 확인하기 위한 함수
    public static bool Is( string value )
    {
		if( value==null ) return false;

		if( string.IsNullOrEmpty(value) )
		{
			return false;
		}

		return true;
    }

	//객체를 생성하기 위한 함수
	public static Transform Create( Transform parent, Vector3 position, Quaternion rotation, UnityEngine.Object sample )
	{
//		if( parent==null ) return null; //(NULL)값을 허용함
		if( sample==null ) return null;

		GameObject gameObject = GameObject.Instantiate( sample as GameObject, position, rotation, parent );
		return gameObject.transform;
	}

	//객체를 생성하기 위한 함수
	public static Transform Create( Vector3 position, UnityEngine.Object sample )
	{
//		if( parent==null ) return null; //(NULL)값을 허용함
		if( sample==null ) return null;

		return Create( null, position, Quaternion.identity, sample );
	}

	//객체를 생성하기 위한 함수
	public static Transform Create( Vector3 position, Quaternion rotation, UnityEngine.Object sample )
	{
//		if( parent==null ) return null; //(NULL)값을 허용함
		if( sample==null ) return null;

		return Create( null, position, rotation, sample );
	}

	//객체를 생성하기 위한 함수
	public static Transform Create( Transform parent, UnityEngine.Object sample )
	{
//		if( parent==null ) return null; //(NULL)값을 허용함
		if( sample==null ) return null;

		GameObject gameObject = GameObject.Instantiate( sample as GameObject, parent );
		return gameObject.transform;
	}

	//객체를 생성하기 위한 함수
	public static Transform Create( UnityEngine.Object sample )
	{
		if( sample==null ) return null;
		return Create( null, sample );
	}

	//객체를 생성하기 위한 함수
	public static Transform Create( Transform parent, string source )
	{
//		if( parent==null ) return null;	//(NULL)값을 허용함
		if( !Is(source) ) return null;

		return Create( parent, Resources.Load(source) );
	}

	//객체를 생성하기 위한 함수
	public static Transform Create( Vector3 position, string source )
	{
//		if( parent==null ) return null;	//(NULL)값을 허용함
		if( !Is(source) ) return null;

		return Create( null, position, Quaternion.identity, Resources.Load(source) );
	}

	//객체를 생성하기 위한 함수
	public static Transform Create( string source )
	{
		if( !Is(source) ) return null;
		return Create( Resources.Load(source) );
	}

	//파일을 확인하기 위한 함수
	public static bool IsDir( string path )
	{
		if( !Is(path) ) return false;

		if( Directory.Exists(path) ) 
		{
			return true;
		}

		return false;
	}

	//파일을 확인하기 위한 함수
	public static bool IsFile( string filepath )
	{
		if( !Is(filepath) ) return false;

		if( File.Exists(filepath) ) 
		{
			return true;
		}

		return false;
	}

	//파일의 확장자를 얻기 위한 함수
	public static string Ext( string filepath )
	{
		if( !Is(filepath) ) return null;

		string[] urlarray	= filepath.Split('?');
		string[] strarray	= urlarray[0].Split('.');
		if( strarray.Length<1 ) return null;

		return strarray[strarray.Length-1].ToLower();
	}

	//파일 이름을 얻기 위한 함수
	public static string GetFileName( string filepath )
	{
		if( !Is(filepath) ) return null;

		string filenameext = GetFileNameExt(filepath);

		string[] strarray = filenameext.Split('.');
		if( strarray.Length<1 ) return null;

		return filenameext.Substring( 0, filenameext.Length-(Ext(filenameext).Length+1) );
	}

	//파일 이름을 얻기 위한 함수
	public static string GetFileNameExt( string filepath )
	{
		if( !Is(filepath) ) return null;

		string[] strarray = filepath.Replace( "\\", "/" ).Split('/');
		if( strarray.Length<1 ) return null;

		return strarray[strarray.Length-1];
	}

	//파일 이름을 얻기 위한 함수
	public static string GetPath( string filepath )
	{
		if( !Is(filepath) ) return null;
		return filepath.Substring( 0, filepath.Length-GetFileNameExt(filepath).Length );
	}

	//숫자인지 확인하기 위한 함수
	public static bool IsNumber( string value )
	{
		if( !Is(value) ) return false;

		int numChk = 0;
		if( int.TryParse( value, out numChk ) )
		{
			return true;
		}

		return false;
	}

	//실수인지 확인하기 위한 함수 
	public static bool IsSingle( string value )
	{
		if( !Is(value) ) return false;

		if( IsNumber(value) )
		{
			return true;
		}

		Regex regex = new Regex(@"[^0-9\.\-]");

		if( regex.IsMatch(value) )
		{
			//숫자, 음수, 콤마 외에 다른 문자가 포함되었을 경우, FALSE
			return false;
		}

		if (value.Split('.').Length > 2)
		{
			//실수 콤마가 여러개 사용되었을 경우, FALSE
			return false;
		}

		if( value.IndexOf('-')>0 )
		{
			//음수 표기가 맨 앞에 있지 않으면, FALSE
			return false;
		}

		return true;
	}

	//나누셈을 처리하기 위한 함수
    public static float Divide( int value1, int value2 )
    {
		if( value2!=0 )
        {
            return value1 / (float)value2;
        }

        return 0;
    }

	//나누셈을 처리하기 위한 함수
    public static Vector3 Divide( Vector3 vector, int value )
    {
		vector.x = Divide( vector.x, value );
		vector.y = Divide( vector.y, value );
		vector.z = Divide( vector.z, value );

        return vector;
    }

	//나누셈을 처리하기 위한 함수
    public static float Divide( float value1, float value2 )
    {
        if( value2!=0 )
        {
            return value1 / value2;
        }

        return 0;
    }

	//프로토콜을 확인하기 위한 함수
	public static bool IsUrl( string url )
	{
		if( !Is(url) ) return false;

		if( url.StartsWith("http://") || url.StartsWith("https://") )
		{
			return true;
		}

		return false;
	}

	public static bool IsJar( string url )
	{
		if( !Is(url) ) return false;

		if( url.StartsWith("jar:file://") )
		{
			return true;
		}

		return false;
	}

	//프로토콜을 확인하기 위한 함수
	public static bool IsUrlOrJar( string url )
	{
		if( !Is(url) ) return false;

		if( IsUrl(url) || IsJar(url) )
		{
			return true;
		}

		return false;
	}

	//경로를 생성하기 위한 함수
	public static void CreateDirectory( string in_path )
	{
  		if( !Is(in_path) ) return;
		if( IsDir(in_path) ) return;

		string[] pathArray = in_path.Replace( "file://", "" ).Replace( "\\", "/" ).Split('/');

		string path = null;
		foreach( string dir in pathArray )
		{
			if( Is(path) ) path += "/";
			path += dir;

			if( Is(path) && !IsDir(path) )
			{
				Directory.CreateDirectory(path);
			}
		}
	}

	//오브젝트를 활성화 하기 위한 함수
	public static void Active( GameObject obj )
	{
		if( obj==null ) return;

		EnableBehaviour enable = obj.GetComponent(typeof(EnableBehaviour)) as EnableBehaviour;
		if( enable!=null )
		{
			enable.enabled = false;
			Component.Destroy( enable );
		}

		obj.SetActive( true );
	}

	//객체를 비활성화 하기 위한 함수
	public static void Inactive( GameObject obj )
	{
		if( obj==null ) return;
		obj.SetActive( false );
	}

	public static void funcActive( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(GameObject) ) return;
		Active( wParam as GameObject );
	}

	public static void funcInactive( object wParam=null, object lParam=null )
	{
		if( wParam==null || wParam.GetType()!=typeof(GameObject) ) return;
		Inactive( wParam as GameObject );
	}

	//컴포넌트를 삭제하기 위한 함수
	public static void Destroy( GameObject gameObjet )
	{
		if( gameObjet==null ) return;
		GameObject.Destroy( gameObjet );
	}

	//컴포넌트를 삭제하기 위한 함수
	public static void Destroy( Component component )
	{
		if( component==null ) return;
		Component.Destroy( component );
	}

	//컴포넌트를 삭제하기 위한 함수
	public static void funcDestroy( object wParam=null, object lParam=null )
	{
		if( wParam!=null && wParam.GetType()==typeof(GameObject) )
		{
			GameObject.Destroy( wParam as GameObject );
		}
	}

	public static Sprite Sprite( Texture2D texture )
	{
		if( texture==null ) return null;
		return UnityEngine.Sprite.Create( texture, new Rect( 0f, 0f, texture.width, texture.height ), new Vector2() );
	}

	public static Texture2D Texture( RenderTexture renderTexture )
	{
		if( renderTexture==null ) return null;

		Texture2D texture = new Texture2D( renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false );
		if( texture!=null )
		{
			RenderTexture.active = renderTexture;
			texture.ReadPixels( new Rect( 0, 0, renderTexture.width, renderTexture.height), 0, 0 );
			texture.Apply();
		}

		return texture;
	}

	public static Sprite Sprite( RenderTexture renderTexture )
	{
		if( renderTexture==null ) return null;
		return Sprite(Texture(renderTexture));
	}

	//문자열을 확인하기 위한 함수
	public static string CH( string value1, string value2, bool allowNothing=true )
	{
//		if( !Is(value1) ) return null; //(NULL)값을 허용함 
//		if( !Is(value2) ) return null; //(NULL)값을 허용함 

		if( Is(value2) )
		{
			return value2;
		}

		if( !Is(value1) && allowNothing )
		{
			return "(NOTHING)";
		}

		return value1;
	}

	//색상 값을 얻기 위한 함수
    public static Color Color( float r, float g, float b, float a=1f )
    {
        return new Color( r/255f, g/255f, b/255f, a );
    }

	//색상 값을 얻기 위한 함수
    public static Color Color( Color color, float a )
    {
		if( a<0f ) a = 0f;
        return new Color( color.r, color.g, color.b, a );
    }

	//월드 좌표를 캔버스 좌표로 변환하기 위한 함수
	public static Vector3 CanvasToWorldPoint( CanvasBehaviour canvas, CameraBehaviour camera, Vector3 point )
	{
		if( canvas==null ) return new Vector3();
		if( camera==null ) return new Vector3();
		if( camera.Get()==null ) return new Vector3();

		return CanvasToWorldPoint( canvas, camera.Get(), point );
	}

	//월드 좌표를 캔버스 좌표로 변환하기 위한 함수
	public static Vector3 CanvasToWorldPoint( CanvasBehaviour canvas, Camera camera, Vector3 point )
	{
		if( canvas==null ) return new Vector3();
		if( camera==null ) return new Vector3();

		return ScreenToWorldPoint( camera, CanvasToScreenPoint( canvas, point ) );
	}

	//캔버스 좌표를 스크린 좌표로 변환하기 위한 함수
	public static Vector3 CanvasToScreenPoint( CanvasBehaviour canvas, Vector3 point )
	{
		if( canvas==null ) return new Vector3();

		if( canvas.Camera()!=null && canvas.Camera().Get()!=null )
		{
			return RectTransformUtility.WorldToScreenPoint( canvas.Camera().Get(), point );
		}

		return new Vector3();
	}

	//월드 좌표를 스크린 좌표로 변환하기 위한 함수
	public static Vector2 WorldToScreenPoint( CameraBehaviour camera, Vector3 point )
	{
		if( camera==null ) return new Vector2();

		return WorldToScreenPoint( camera.Get(), point );
	}

	//월드 좌표를 스크린 좌표로 변환하기 위한 함수
	public static Vector2 WorldToScreenPoint( Camera camera, Vector3 point )
	{
		if( camera==null ) return new Vector2();

		return RectTransformUtility.WorldToScreenPoint( camera, point );
	}

	//월드 좌표를 스크린 좌표로 변환하기 위한 함수
	public static Vector3 ScreenToWorldPoint( Camera camera, Vector3 point )
	{
		if( camera==null ) return new Vector3();

		return camera.ScreenToWorldPoint( point );
	}

	//월드 좌표를 캔버스 좌표로 변환하기 위한 함수
	public static Vector3 WorldToCanvasPoint( CameraBehaviour camera, CanvasBehaviour canvas, Vector3 point )
	{
		if( camera==null ) return new Vector3();
		if( canvas==null ) return new Vector3();
		if( camera.Get()==null ) return new Vector3();

		return WorldToCanvasPoint( camera.Get(), canvas, point );
	}

	//월드 좌표를 캔버스 좌표로 변환하기 위한 함수
	public static Vector3 WorldToCanvasPoint( Camera camera, CanvasBehaviour canvas, Vector3 point )
	{
		if( camera==null ) return new Vector3();
		if( canvas==null ) return new Vector3();

		return ScreenToCanvasPoint( canvas, WorldToScreenPoint(camera, point) );
	}

	//월드 좌표를 캔버스 좌표로 변환하기 위한 함수
	public static Vector3 WorldToCanvasPoint( Vector3 point )
	{
		return WorldToCanvasPoint( Framework.Camera(), MainUI.Canvas(), point );
	}

	//스크린 좌표를 캔버스 좌표로 변환하기 위한 함수
	public static Vector3 ScreenToCanvasPoint( CanvasBehaviour canvas, Vector2 point )
	{
		if( canvas==null ) return new Vector3();
		if( canvas.Camera()==null ) return new Vector3();
		if( canvas.Camera().Get()==null ) return new Vector3();

		Vector3 vector = new Vector3();
		if( RectTransformUtility.ScreenPointToWorldPointInRectangle( canvas.Transform(), point, canvas.Camera().Get(), out vector ) )
		{
			return vector;
		}

		return new Vector3();
	}

	//스크린 좌표를 캔버스 좌표로 변환하기 위한 함수
	public static Vector3 ScreenToCanvasPoint( Vector2 point )
	{
		return ScreenToCanvasPoint( MainUI.Canvas(), point );
	}
	
	//레이어를 설정하기 위한 함수
	public static void SetLayer( Transform transform, int Layer, bool inflow=true )
	{
		if( transform==null ) return;

		transform.gameObject.layer = Layer;

		if( inflow )
		{
			Transform child = null;
			int End = transform.childCount;

			for( int i=0; i<End; i++ )
			{
				child = transform.GetChild(i);
				if( child!=null )
				{
					SetLayer( child, Layer, inflow );
				}
			}
		}
	}

	public static void MaterialSetup( GameObject gameObject )
	{
		if( gameObject==null ) return;

		bool isStandard = false;
		int nMode = 0;

		Component[] comArray = gameObject.GetComponentsInChildren( typeof(Renderer), true );
		foreach( Renderer renderer in comArray )
		{
			foreach( Material material in renderer.sharedMaterials )
			{
				if( material==null ) continue;
				//material.GetTag("RenderType", true, "Nothing");

				isStandard = material.shader.name=="Standard" || material.shader.name=="Standard (Specular setup)";
				if( isStandard )
				{
					nMode = material.GetInt("_Mode");
				}
				else
				{
					nMode = 0;
				}

				if( isStandard && ( nMode==2 || nMode==3 ) )
				{
#if UNITY_EDITOR
					material.shader = SHADER.Transparent;
#else
					material.shader = Shader.Find(material.shader.name);
#endif
				}
				else
				{
					material.shader = Shader.Find(material.shader.name);
				}
			}
		}
	}

	//파일을 삭제하기 위한 함수
	public static bool Delete( string path )
	{
		if( !Is(path) ) return false;

		if( IsFile(path) )
		{
			File.Delete( path );
			return true;
		}
		else
		if( IsDir(path) )
		{
			Directory.Delete( path, true );
			return true;
		}

		return false;
	}

    //10진수를 16진수로 변환하기 위한 함수
	public static string DecToHex( int value )
    {
        string result = null;

        result += DecToHexByte(value/16);
        result += DecToHexByte(value%16);

        return result;
    }

    //16진수를 10진수로 변환하기 위한 함수
    public static int HexToDec( string code )
    {
        if( !Is(code) ) return 0;

        int result = 0;

		if( code.Length>2 )
		{
			result += HexToDecByte( code.Substring( 0, 1 ) ) * 16;
			result += HexToDecByte( code.Substring( 1, 1 ) );
		}
		else
		{
			result = HexToDecByte(code);
		}

        return result;
    }

    //10진수를 16진수로 변환하기 위한 함수
	static string DecToHexByte( int value )
    {
		switch( value )
        {
            case 1:
                return "1";
            case 2:
                return "2";
            case 3:
                return "3";
            case 4:
                return "4";
            case 5:
                return "5";
            case 6:
                return "6";
            case 7:
                return "7";
            case 8:
                return "8";
            case 9:
                return "9";
            case 10:
                return "A";
            case 11:
                return "B";
            case 12:
                return "C";
            case 13:
                return "D";
            case 14:
                return "E";
            case 15:
                return "F";
        }

        return "0";
    }

    //16진수를 10진수로 변환하기 위한 함수
    static int HexToDecByte( string value )
    {
        switch( value )
        {
            case "1":
                return 1;
            case "2":
                return 2;
            case "3":
                return 3;
            case "4":
                return 4;
            case "5":
                return 5;
            case "6":
                return 6;
            case "7":
                return 7;
            case "8":
                return 8;
            case "9":
                return 9;
            case "A":
                return 10;
            case "B":
                return 11;
            case "C":
                return 12;
            case "D":
                return 13;
            case "E":
                return 14;
            case "F":
                return 15;
        }

        return 0;
    }

	//Euc-Kr 포맷의 캐릭터 셋으로 변환하기 위한 함수
    public static byte[] EucKr( string value )
    {
        if( !Is(value) ) return null;

        System.Text.Encoding charset = System.Text.Encoding.GetEncoding("euc-kr");
        byte[] encoding = System.Text.Encoding.Convert( System.Text.Encoding.UTF8, charset, System.Text.Encoding.UTF8.GetBytes(value) );

        return encoding;
    }

	//UTF-8 포맷의 캐릭터셋으로 변환하기 위한 함수
    public static string UTF8( byte[] bytes )
    {
        if( bytes==null || bytes.Length<=0 ) return null;

        System.Text.Encoding charset = System.Text.Encoding.GetEncoding("euc-kr");
        byte[] encoding = System.Text.Encoding.Convert( charset, System.Text.Encoding.UTF8, bytes );

		return System.Text.Encoding.UTF8.GetString(encoding);
    }

	//바이트 배열로 변환하기 위한 함수
    public static byte[] GetBytes( string value )
    {
        if( !Is(value) ) return null;
		return System.Text.Encoding.UTF8.GetBytes( value );
	}

	//문자열을 얻기 위한 함수
    public static string GetString( char value )
    {
		char[] chars = { value };
		return new string(chars);
	}

	//문자열을 얻기 위한 함수
    public static string GetString( byte[] bytes, System.Text.Encoding encoding=null )
    {
        if( bytes==null || bytes.Length<=0 ) return null;

		if( encoding==null )
		{
			encoding = System.Text.Encoding.Default;
		}

        return encoding.GetString(bytes);
    }

	//문자열을 얻기 위한 함수
	public static string GetString( byte[] bytes, int start, int length )
	{
		if( bytes==null ) return null;
		if( bytes==null || bytes.Length<start ) return null;
		if( start<=0 ) return null;
		if( length<=0 ) return null;

		return System.Text.Encoding.UTF8.GetString( bytes, start, Mathf.Min( bytes.Length-start, length ) );
	}

	public static float GetDeltaTime( float value, bool harp=false )
	{
		if( harp )
		{
			return Mathf.Min( SYSTEM.UNSCALE_TIME_GRAPHIC_LIMIT_UNDER_FRAME_HARP, value );
		}

		return Mathf.Min( SYSTEM.UNSCALE_TIME_GRAPHIC_LIMIT_UNDER_FRAME, value );
	}

	public static float GetUnscaleFixedDeltaTime( bool harp=false )
	{
		return GetDeltaTime( Time.fixedUnscaledDeltaTime, harp );
	}

	public static float GetUnscaleDeltaTime( bool harp=false )
	{
		return GetDeltaTime( Time.unscaledDeltaTime, harp );
	}

	public static float GetFixedDeltaTime( bool harp=false )
	{
		return GetDeltaTime( Time.fixedDeltaTime, harp );
	}

	public static float GetDeltaTime( bool harp=false )
	{
		return GetDeltaTime( Time.deltaTime, harp );
	}

	public static int Random( int min, int max )
	{
		return UnityEngine.Random.Range( min, max+1 );
	}

	//난수를 얻기 위한 함수
	public static float Random( float min, float max )
	{
		return UnityEngine.Random.Range( Mathf.Min(min, max), Mathf.Max(min, max) );
	}

	//난수를 얻기 위한 함수
	public static int RandomInt( Vector2 range )
	{
		return Random( (int)range.x, (int)range.y );
	}

	//난수를 얻기 위한 함수
	public static float Random( Vector2 range )
	{
		return Random( range.x, range.y );
	}

	//확률을 처리하기 위한 함수
	public static bool Random( int value )
	{
		if( Random(0,99)<value )
		{
			return true;
		}

		return false;
	}

	public static float Randomf( int min, int max )
	{
		return UnityEngine.Random.Range( (float)min, (float)max+1 );
	}

	//난수를 얻기 위한 함수
	public static float Randomf( Vector2 value )
	{
		return UnityEngine.Random.Range(value.x, value.y);
	}

	public static float BothRandom( float min, float max )
	{
		float value = Random( Mathf.Min(min, max), Mathf.Max(min, max) );

		if( Random(50) )
		{
			value *= -1f;
		}

		return value;
	}

	public static float BothRandom( float value )
	{
		return BothRandom( 0f, value );
	}

	//정수 범위를 제한하기 위한 함수
    public static int Limit( int in_min, int in_max, int value )
    {
		int min = Mathf.Min( in_min, in_max );
		int max = Mathf.Max( in_min, in_max );

		value = Mathf.Min( max, value );
		value = Mathf.Max( min, value );

        return value;
    }

	//실수 범위를 제한하기 위한 함수
    public static float Limit( float in_min, float in_max, float value )
    {
		float min = Mathf.Min( in_min, in_max );
		float max = Mathf.Max( in_min, in_max );

		value = Mathf.Min( max, value );
		value = Mathf.Max( min, value );

        return value;
    }

	//정수 값을 얻기 위한 함수
	public static int GetInt( string value )
	{
		if( !Is(value) ) return 0;

		if( IsNumber(value) )
		{
			return int.Parse(value);
		}

		return 0;
	}

	//텍스쳐인지 확인하기 위한 함수
	public static bool IsTexture( string filepath, bool isExt=false )
	{
		if( !Is(filepath) ) return false;

		string ext = null;
		if( !isExt )
		{
			ext = Ext(filepath);
		}
		else
		{
			ext = filepath.ToLower();
		}

		if( ext=="png" || ext=="jpg" || ext=="jpeg" || ext=="tga" || ext=="texture" )
		{
			return true;
		}

		return false;
	}

	//애셋번들인지 확인하기 위한 함수
	public static bool IsAssetBundle( string filepath, bool isExt=false )
	{
		if( !Is(filepath) ) return false;

		string ext = null;
		if( !isExt )
		{
			ext = Ext(filepath);
		}
		else
		{
			ext = filepath.ToLower();
		}

		if( ext=="unity3d" || ext=="asset" )
		{
			return true;
		}

		return false;
	}

	//애셋번들인지 확인하기 위한 함수
	public static bool IsAssetBundle( tagAssetBundle assetBundle, bool isExt=false )
	{
		if( assetBundle==null ) return false;
		return IsAssetBundle( assetBundle.url, isExt );
	}

	//애셋번들인지 확인하기 위한 함수
	public static bool IsXml( string filepath, bool isExt=false )
	{
		if( !Is(filepath) ) return false;

		string ext = null;
		if( !isExt )
		{
			ext = Ext(filepath);
		}
		else
		{
			ext = filepath.ToLower();
		}

		if( ext=="xml" || ext=="data" )
		{
			return true;
		}

		return false;
	}

	//애셋번들인지 확인하기 위한 함수
	public static bool IsSoundFile( string filepath, bool isExt=false )
	{
		if( !Is(filepath) ) return false;

		string ext = null;
		if( !isExt )
		{
			ext = Ext(filepath);
		}
		else
		{
			ext = filepath.ToLower();
		}

		if( ext=="wav" || ext=="mp3" || ext=="ogg" || ext=="sound" )
		{
			return true;
		}

		return false;
	}

	//장면을 얻기 위한 함수
	public static Scene GetScene( int buildIndex )
	{
		for( int i=0; i<SceneManager.sceneCount; i++ )
		{
			if( SceneManager.GetSceneAt(i).buildIndex==buildIndex )
			{
				return SceneManager.GetSceneAt(i);
			}
		}

		return new Scene();
	}

	// 장면을 얻기 위한 함수
	public static Scene	GetScene( string sceneName )
	{
		if( !Is(sceneName) ) return new Scene();

		for( int i=0; i<SceneManager.sceneCount; i++ )
		{
			if( SceneManager.GetSceneAt(i).name==sceneName )
			{
				return SceneManager.GetSceneAt(i);
			}
		}

		return new Scene();
	}

	//장면을 얻기 위한 함수
	public static Scene GetScene( SceneLevel buildIndex )
	{
		return GetScene( (int)buildIndex );
	}

	//장면이 로드되어 있는지 확인하기 위한 함수
	public static bool IsScene( SceneLevel buildIndex )
	{
		Scene scene = GetScene(buildIndex);
		if( scene!=null )
		{
			return scene.isLoaded;
		}

		return false;
	}

	//장면이 로드되어 있는지 확인하기 위한 함수
	public static bool IsScene( string sceneName )
	{
		if( !Is(sceneName) ) return false;

		Scene scene = GetScene(sceneName);
		if( scene!=null )
		{
			return scene.isLoaded;
		}

		return false;
	}

	//장면을 불러오기 위한 함수
	public static bool LoadScene( SceneLevel buildIndex, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( !IsScene(buildIndex) )
		{
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync( (int)buildIndex, LoadSceneMode.Additive );
			if( func!=null )
			{
				if( wParam==null ) wParam = Library.GetScene(buildIndex);
				ApplicationBehaviour.This.Coroutine( asyncOperation, func, wParam, lParam );
			}

//			ApplicationBehaviour.This.Process.Register(asyncOperation);
			return true;
		}
		else
		if( func!=null )
		{
			if( wParam==null ) wParam = Library.GetScene(buildIndex);
			func( wParam, lParam );
			return true;
		}

		return false;
	}

	//장면을 불러오기 위한 함수
	public static AsyncOperation LoadScene( string sceneName, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( !Is(sceneName) ) return null;
//		if( func==null ) return null;	//(NULL)값을 허용함
//		if( wParam==null ) return null;	//(NULL)값을 허용함
//		if( lParam==null ) return null;	//(NULL)값을 허용함

		if( !IsScene(sceneName) )
		{
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync( sceneName, LoadSceneMode.Additive );
			if (func != null)
			{
				if( wParam==null ) wParam = GetScene(sceneName);
				ApplicationBehaviour.This.Coroutine( asyncOperation, func, wParam, lParam );
			}

//			ApplicationBehaviour.This.Process.Register(asyncOperation);
			return asyncOperation;
		}
		else
		if( func!=null )
		{
			if( wParam==null ) wParam = GetScene(sceneName);
			func( wParam, lParam );
		}

		return null;
	}

	//장면을 불러오기 위한 함수
	public static void LoadSceneAndActiveScene( SceneLevel buildIndex )
	{
		LoadScene( buildIndex, funcActiveScene );
	}

	//장면을 언로드하기 위한 함수
	public static void UnloadScene( Scene scene, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( scene==null ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( scene.isLoaded )
		{
			if( ApplicationBehaviour.IsStartup() )
			{
				ApplicationBehaviour.This.Coroutine( SceneManager.UnloadSceneAsync(scene), func, wParam, lParam );
			}
			else
			{
				SceneManager.UnloadSceneAsync(scene);
			}
		}
		else
		if( func!=null )
		{
			func( wParam, lParam );
		}
	}

	//장면을 언로드하기 위한 함수
	public static void UnloadScene( SceneLevel buildIndex, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
//		if( func==null ) return;		//(NULL)값을 허용함
//		if( wParam==null ) return;		//(NULL)값을 허용함
//		if( lParam==null ) return;		//(NULL)값을 허용함

		Scene scene = GetScene( (int)buildIndex );
		if( scene!=null && scene.isLoaded )
		{
			UnloadScene( scene, func, wParam, lParam );
		}
		else
		if( scene==null )
		{
			if( func!=null )
			{
				func( wParam, lParam );
			}
		}
	}

	//장면 설정을 활성화 하기 위한 함수
	public static void ActiveScene( Scene scene )
	{
		if( scene==null ) return;
		if( !scene.isLoaded ) return;

		SceneManager.SetActiveScene( scene );
	}

	//장면 설정을 활성화 하기 위한 함수
	public static void funcActiveScene( object wParam=null, object lParam=null )
	{
		if( wParam!=null && wParam.GetType()==typeof(Scene) )
		{
			ActiveScene( (Scene)wParam );
		}
		else
		if( wParam!=null && wParam.GetType()==typeof(SceneLevel) )
		{
			ActiveScene( GetScene((int)wParam) );
		}
		else
		if( wParam!=null && wParam.GetType()==typeof(string) )
		{
			ActiveScene( GetScene(wParam as string) );
		}
		else
		if( lParam!=null && Is((string)lParam) ) 
		{
			ActiveScene( GetScene((string)lParam) );
		}
	}

    public static string NumberFormat( int in_value )
    {
		string output = NumberFormat( Mathf.Abs(in_value).ToString() );

		if( in_value<(0) )
		{
			output = "-"+output;
		}

		return output;
    }

    static string NumberFormat( string value, int unit=3 )
    {
        if( value==null ) return null;

        string output = null;

        while( value.Length>unit )
        {
            if( output!=null )
            {
                output = ","+output;
            }

			output	= value.Substring( value.Length-unit, unit )+output;
			value	= value.Substring( 0, value.Length-unit );
        }

        if( output!=null )
        {
            output = ","+output;
        }

        output = value.Substring( 0, value.Length )+output;

        return output;
    }

    public static string NumberFormat( object value )
    {
		if( value==null ) return null;
        return NumberFormat(value.ToString());
    }

    public static string NumberFormat( float in_value )
    {
		float value = Mathf.Abs(in_value);
		string text = NumberFormat( (int)in_value );

		if( value<100 && value>0 )
		{
			int nDecimal = (int)Mathf.Round(value*100) - (int)value*100;
			if( nDecimal>0 )
			{
				text += "."+nDecimal;
			}
		}

		return text;
    }

    public static string NumberUnit( long value )
    {
		return NumberUnit_Korean(value);
//		return NumberUnit_Global(value);
    }

    public static string NumberUnit_Korean( long in_value )
    {
		long value = in_value;
		if( value<0 )
		{
			value *= -1;
		}

		string output = null;
		if( value>=1000000000000 )
		{
			output = NumberFormat( value/1000000000000 )+" 조";
			long remine = (int)( value%100000000 );
			if( remine>0 )
			{
				output += " "+NumberFormat( Mathf.RoundToInt( (remine)/(100000000f) ) )+" 억";
			}
		}
		else
		if( value>=100000000 )
		{
			output = NumberFormat( value/100000000 )+" 억";
			int remine = (int)( value%100000000 );
			if( remine>0 )
			{
				output += " "+NumberFormat( Mathf.RoundToInt( remine/10000f ) )+" 만";
			}
		}
		else
		if( value>=10000 )
		{
			output = NumberFormat( value/10000 )+" 만";
			int remine = (int)( value%10000 );
			if( remine>0 )
			{
				output += " "+NumberFormat(remine);
			}
		}
		else
		{
			output = NumberFormat(value);
		}

		if( in_value<0 )
		{
			output = "-"+output;
		}

        return output;
    }

    public static string NumberUnit_Global( long value )
    {
		if( value>=100000000000000 )
		{
			return NumberFormat( Mathf.RoundToInt(value/1000000000000f) )+" t";
		}
		else
		if( value>=100000000000 )
		{
			return NumberFormat( Mathf.RoundToInt(value/1000000000f) )+" b";
		}
		else
		if( value>=100000000 )
		{
			return NumberFormat( Mathf.RoundToInt(value/1000000f) )+" m";
		}
		else
		if( value>=100000 )
		{
			return NumberFormat( Mathf.RoundToInt(value/1000f) )+" k";
		}

        return NumberFormat(value);
    }

	public static string Script( string value, object var0=null, object var1=null, object var2=null, object var3=null )
	{
		if( !Is(value) ) return value;
//		if( var0==null ) return value;	//(NULL)값을 허용함
//		if( var1==null ) return value;	//(NULL)값을 허용함
//		if( var2==null ) return value;	//(NULL)값을 허용함
//		if( var3==null ) return value;	//(NULL)값을 허용함

		int End = value.Length;
		int nStart = 0;
		string result = null;
		bool inBracket = false;
		string temp = null;

		for( int i=0; i<End; i++ )
		{
			temp = value.Substring( i, 1 );

			if( inBracket && temp=="}" )
			{
				result += Script_( value.Substring( nStart+1, i-nStart-1 ), var0, var1, var2, var3 );
				inBracket = false;
				nStart = i+1;
			}
			else
			if( temp=="{" )
			{
				result += value.Substring( nStart, i-nStart );
				inBracket = true;
				nStart = i;
			}
		}

		if( nStart<value.Length )
		{
			result += value.Substring( nStart, End-nStart );
		}

		return result;
	}

	public static string ScriptUnhighlight( string value, object var0=null, object var1=null, object var2=null, object var3=null )
	{
		if( !Is(value) ) return value;
//		if( var0==null ) return value;	//(NULL)값을 허용함
//		if( var1==null ) return value;	//(NULL)값을 허용함
//		if( var2==null ) return value;	//(NULL)값을 허용함
//		if( var3==null ) return value;	//(NULL)값을 허용함

		int End = value.Length;
		int nStart = 0;
		string result = null;
		bool inBracket = false;
		string temp = null;

		for( int i=0; i<End; i++ )
		{
			temp = value.Substring( i, 1 );
			if( inBracket && temp=="}" )
			{
				result += ScriptUnhighlight_( value.Substring( nStart+1, i-nStart-1 ), var0, var1, var2, var3 );
				inBracket = false;
				nStart = i+1;
			}
			else
			if( temp=="{" )
			{
				result += value.Substring( nStart, i-nStart );
				inBracket = true;
				nStart = i;
			}
		}

		if( nStart<value.Length )
		{
			result += value.Substring( nStart, End-nStart );
		}

		return result;
	}

	public static string Script_( string value, object var0=null, object var1=null, object var2=null, object var3=null )
	{
		if( !Is(value) ) return value;
//		if( var0==null ) return value;	//(NULL)값을 허용함
//		if( var1==null ) return value;	//(NULL)값을 허용함
//		if( var2==null ) return value;	//(NULL)값을 허용함
//		if( var3==null ) return value;	//(NULL)값을 허용함

		string[] textArray = value.Split(':');
		string subject = null;
		string postposition = null;
		bool highlight = false;
		bool alert = false;
		string result = null;

		for( int i=0; i<textArray.Length; i++ )
		{
			if( i==0 )
			{
				if( textArray[i]=="0" && var0!=null )
				{
					subject = var0.ToString();
				}
				else
				if( textArray[i]=="1" && var1!=null )
				{
					subject = var1.ToString();
				}
				else
				if( textArray[i]=="2" && var2!=null )
				{
					subject = var2.ToString();
				}
				else
				if( textArray[i]=="3" && var3!=null )
				{
					subject = var3.ToString();
				}
				else
				{
					subject = Language.Get( textArray[i] );
				}
			}
			else
			if( i==1 )
			{
				postposition = textArray[i];
			}
			else
			{
				if( textArray[i].ToLower()=="highlight" )
				{
					highlight = true;
				}
				else
				if( textArray[i].ToLower()=="alert" )
				{
					alert = true;
				}
			}
		}

		if( highlight )
		{
			result = Highlight(subject);
		}
		else
		if( alert )
		{
			result = HighlightAlert(subject);
		}
		else
		{
			result = subject;
		}

		result += PostPosition_( subject, postposition );

		return result;
	}

	public static string ScriptUnhighlight_( string value, object var0=null, object var1=null, object var2=null, object var3=null )
	{
		if( !Is(value) ) return value;
//		if( var0==null ) return value;	//(NULL)값을 허용함
//		if( var1==null ) return value;	//(NULL)값을 허용함
//		if( var2==null ) return value;	//(NULL)값을 허용함
//		if( var3==null ) return value;	//(NULL)값을 허용함

		string[] textArray = value.Split(':');
		string subject = null;
		string postposition = null;
//		bool highlight = false;
//		bool alert = false;		
		string result = null;

		for( int i=0; i<textArray.Length; i++ )
		{
			if( i==0 )
			{
				if( textArray[i]=="0" )
				{
					subject = var0.ToString();
				}
				else
				if( textArray[i]=="1" )
				{
					subject = var1.ToString();
				}
				else
				if( textArray[i]=="2" )
				{
					subject = var2.ToString();
				}
				else
				if( textArray[i]=="3" )
				{
					subject = var3.ToString();
				}
				else
				{
					subject = Language.Get(textArray[i]);
				}
			}
			else
			if( i==1 )
			{
				postposition = textArray[i];
			}
			/*
			else
			{
				if( textArray[i].ToLower()=="highlight" )
				{
					highlight = true;
				}
				else
				if( textArray[i].ToLower()=="alert" )
				{
					alert = true;
				}
			}
			*/
		}

		/*
		if( highlight )
		{
			result = Highlight(subject);
		}
		else
		if( alert )
		{
			result = HighlightAlert(subject);
		}
		else
		*/
		{
			result = subject;
		}

		result += PostPosition_( subject, postposition );

		return result;
	}

	//조사어를 얻기 위한 함수
	public static string PostPosition( string value, string postposition )
	{
		return value+PostPosition_( value, postposition );
	}

	// 조사어를 얻기 위한 함수
	static string PostPosition_( string value, string postposition )
	{
		if( !Is(value)) return null;
		if( value==postposition ) return null;

		int length = value.Length;
		int code, temp, T, V, L;

		string char1 = null, char2 = null, char3 = null, return1 = null, return2 = null, return3 = null;

		string[] LCetable = { "ㄱ", "ㄲ", "ㄴ", "ㄷ", "ㄸ", "ㄹ", "ㅁ", "ㅂ", "ㅃ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅉ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };
		string[] MVetable = { "ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ", "ㅚ", "ㅛ", "ㅜ", "ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ" };
		string[] TCetable = { "", "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ", "ㄺ", "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ", "ㅂ", "ㅄ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };

		byte[] buffer = System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.UTF8.GetBytes(value));
		value = System.Text.Encoding.Unicode.GetString(buffer);

		string ch = null;
		string number = null;

		for( int i=0; i<length; i++ )
		{
			ch = value.Substring( i, 1 );
			if( IsNumber(ch) )
			{
				char3 = "number";
				number = ch;
			}
			else
			{
				byte[] bytes = System.Text.Encoding.Unicode.GetBytes(ch);
				int val = bytes[1] * 256 + bytes[0];

				if (val >= 44032 && val <= 55203)
				{
					code = 0;
					code = val;
					temp = code - 44032;
					T = (int)(temp % 28);
					temp /= 28;
					V = (int)(temp % 21);
					temp /= 21;
					L = (int)temp;

					char1 = LCetable[L];
					char2 = MVetable[V];
					char3 = TCetable[T];
				}
				else
				{
					char3 = "alphabet";
				}
			}
		}

		bool is_char3 = Is(char3);

		switch( postposition )
		{
			case "로":
			case "으로":
				return1 = "(으)로";
				return2 = "으로";
				return3 = "로";
				break;

			case "을":
			case "를":
				return1 = "(을)를";
				return2 = "을";
				return3 = "를";
				break;

			case "이":
			case "가":
				return1 = "(이)가";
				return2 = "이";
				return3 = "가";
				break;

			case "은":
			case "는":
				return1 = "(은)는";
				return2 = "은";
				return3 = "는";
				break;

			case "과":
			case "와":
				return1 = "(와)과";
				return2 = "과";
				return3 = "와";
				break;

			case "야":
				return1 = "(이)야";
				return2 = "이야";
				return3 = "야";
				break;
		}

		if( is_char3 && char3=="number" )
		{
			if( !Is(return1) )
			{
				return postposition;
			}

			switch( number )
			{
				case "1":
				case "2":
//				case "3":
				case "4":
				case "5":
//				case "6":
				case "7":
				case "8":
				case "9":
//				case "0":
					return return3;
			}

			return return2;
		}
		else
		if( is_char3 && char3=="alphabet" )
		{
			if( !Is(return1) )
			{
				return postposition;
			}

			return return1;
		}
		else
		if( is_char3 )
		{
			if( !Is(return2) )
			{
				return postposition;
			}

			return return2;
		}
		else
		if( !Is(return3) )
		{
			return postposition;
		}

		return return3;
	}

	public static string Highlight( string value )
	{
		if( !Is(value) ) return null;
		return "<color="+SYSTEM.HIGHLIGHT_COLOR+">"+value+"</color>";
	}

	public static string Highlight( int value )
	{
		return Highlight(value.ToString());
	}

	public static string HighlightAlert( string value )
	{
		if( !Is(value) ) return null;
		return "<color=yellow>"+value+"</color>";
	}

	public static string HighlightConfirm( string value )
	{
		if( !Is(value) ) return null;
		return "<color=green>"+value+"</color>";
	}

	//파일 수정 시간을 얻기 위한 함수
	public static ulong GetFileModifyTime( string filepath )
	{
		if( !IsFile(filepath) ) return 0;

		DateTime utcTime = new DateTime( 1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc );
		TimeSpan timeSpan = File.GetLastWriteTimeUtc(filepath) - utcTime;
		return (ulong)timeSpan.TotalSeconds;
	}

	public static string BeforeTime( int value )
	{
		if( value<5 )
		{
			return Language.Get(TEXT.방금);
		}
		else
		if( value<60 )
		{
			return Script( Language.Get(TEXT.__초_전), value.ToString() );
		}
		else
		if( value<60*60 )
		{
			return Script( Language.Get(TEXT.__분_전), (value/60).ToString() );
		}
		else
		if( value<60*60*24 )
		{
			return Script( Language.Get(TEXT.__시간_전), (value/(60*60)).ToString() );
		}
		else
		if( value<60*60*24*30 )
		{
			return Script( Language.Get(TEXT.__일_전), (value/(60*60*24)).ToString() );
		}
		else
		if( value<60*60*24*365 )
		{
			return Script( Language.Get(TEXT.__개월_전), (value/(60*60*24*30)).ToString() );
		}

		return Script( Language.Get(TEXT.__개월_전), (value/(60*60*24*365)).ToString() );
	}

	public static string BeforeTime( ulong value )
	{
		return BeforeTime((int)value);
	}
}