using UnityEngine;

public class MainUI
{
	//인터페이스를 생성하기 위한 함수
	public static RectTransform Create( Transform parent, UnityEngine.Object sample )
	{
//		if( parent==null ) return null; //(NULL)값을 허용함
		if( sample==null ) return null;

		GameObject sampleObject = sample as GameObject;
		RectTransform sampleTransform = sampleObject.transform as RectTransform;

		RectTransform transform = Library.Create( parent, sampleObject ) as RectTransform;
		InterfaceBehaviour interfacebehaviour = transform.GetComponent(typeof(InterfaceBehaviour)) as InterfaceBehaviour;
		if( interfacebehaviour!=null )
		{
			CanvasBehaviour canvas = interfacebehaviour.GetComponentInParent( typeof(CanvasBehaviour), true ) as CanvasBehaviour;
			if( canvas!=null )
			{
				canvas.Register( interfacebehaviour );
			}
		}

		transform.anchoredPosition	= sampleTransform.anchoredPosition;
		transform.localRotation		= sampleTransform.localRotation;
		transform.localScale		= sampleTransform.localScale;

		return transform;
	}

	//인터페이스를 생성하기 위한 함수
	public static RectTransform Create( Transform parent, string source )
	{
//		if( parent==null ) return null; //(NULL)값을 허용함
		if( !Library.Is(source) ) return null;

		return Create( parent, Resources.Load(source) );
	}

	//인터페이스를 생성하기 위한 함수
	public static RectTransform Create( string source )
	{
		if( !Library.Is(source) ) return null;
		return Create( source, Canvas() );
	}

	//인터페이스를 생성하기 위한 함수
	public static RectTransform Create( string source, CanvasBehaviour canvas )
	{
		if( canvas==null ) return null;
		if( !Library.Is(source) ) return null;

		return Create( canvas.Transform(), source );
	}

	//캔버스 객체를 얻기 위한 함수
	public static CanvasBehaviour Canvas()
	{
		return PlayBehaviour.This.uiCanvas;
	}

	//전면 객체를 얻기 위한 함수
	public static CanvasBehaviour Fore()
	{
		return PlayBehaviour.This.foreCanvas;
	}

	//전면 객체를 얻기 위한 함수
	public static CanvasBehaviour Forward()
	{
		return PlayBehaviour.This.forwardCanvas;
	}

	//배경 객체를 얻기 위한 함수
	public static CanvasBehaviour Back()
	{
		return PlayBehaviour.This.backCanvas;
	}

	//Scene 객체를 얻기 위한 함수
	public static CanvasBehaviour Scene()
	{
		return PlayBehaviour.This.sceneCanvas;
	}
}