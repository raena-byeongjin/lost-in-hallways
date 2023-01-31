using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//VALUE 객체를 처리하기 위한 클래스
public class ValueBehaviour : RectTransformBehaviour
{
	public RectTransform	m_value			= null;
	private Image			m_image			= null;
	public Text				m_text			= null;

	private Vector2			Pivot			= new Vector2();
	private Vector2			Size			= new Vector2();
	public List<Color>		Colores			= new List<Color>();

	private float			fValue			= 0f;

	public float			fSpeed			= 1f;
	private float			fSmooth			= 0f;
	public float			delay			= 0f;

	public AXIS nAxis = AXIS.NOTHING;

	protected override void Awake()
	{
		if( m_transform==null )
		{
			base.Awake();
			if( m_value==null ) m_value	= m_transform;
			m_image			= m_value.GetComponentInChildren(typeof(Image)) as Image;
			if( m_text==null ) m_text	= GetComponentInChildren(typeof(Text)) as Text;

			if( Size==new Vector2() )
			{
				/*
				if( Transform().anchorMin==new Vector2() && Transform().anchorMax==new Vector2(1, 1) )
				{
					//앵커를 변환함
					Transform().anchorMin	= new Vector2( 0.5f, 0.5f );
					Transform().anchorMax	= new Vector2( 0.5f, 0.5f );

					//앵커를 변환하면 불완전한 값이 설정되므로
					//부모 객체로부터 사이즈를 얻어와 보정함
					RectTransform parent = Transform().parent as RectTransform;
					if( parent!=null )
					{
						Transform().sizeDelta	= parent.sizeDelta + Transform().sizeDelta;
					}
				}
				*/
				if( Axis()==AXIS.VERTICAL )
				{
					if( Value().sizeDelta.y==0 || Value().anchorMin.y!=0.5f || Value().anchorMax.y!=0.5f )
					{
						Vector3 position	= Value().position;
						Value().anchorMin	= new Vector2( Value().anchorMin.x, 0.5f );
						Value().anchorMax	= new Vector2( Value().anchorMax.x ,0.5f );
						Value().position	= position;
						Value().sizeDelta	= new Vector2( Value().sizeDelta.x, GetParentHeight() );
					}
				}
				else
				{
					if( Value().sizeDelta.x==0 || Value().anchorMin.x!=0.5f || Value().anchorMax.x!=0.5f )
					{
						Vector3 position	= Value().position;
						Value().anchorMin	= new Vector2( 0.5f, Value().anchorMin.y );
						Value().anchorMax	= new Vector2( 0.5f, Value().anchorMax.y );
						Value().position	= position;
						Value().sizeDelta	= new Vector2( GetParentWidth(), Value().sizeDelta.y );
					}
				}

				Pivot = new Vector2( Value().localPosition.x-Value().sizeDelta.x/2, Value().localPosition.y-Value().sizeDelta.y/2 );
				Size = Value().sizeDelta;
			}

			Enable( false );
		}
	}

	void Update()
	{
		if( delay>0 )
		{
			delay -= Time.deltaTime;
			return;
		}

		fSmooth = Mathf.Lerp( fSmooth, fValue, Time.deltaTime*fSpeed );
		SetSize( fSmooth );

		if( Mathf.Abs(fSmooth-fValue)<0.01f )
		{
			SetSize( fValue );
			Enable(false);
		}
	}

	//트랜스 폼 객체를 얻기 위한 함수
	public RectTransform Value()
	{
		return m_value;
	}

	//이미지 객체를 얻기 위한 함수
	public Image Image()
	{
		return m_image;
	}

	//수치 값을 입력하기 위한 함수
	public void ON( float value, float transit=-1f )
	{
		if( m_transform==null ) Awake();

		if( value>1f )
		{
			value = 1f;
		}

		fValue = value;

		if( transit==0f )
		{
			SetSize( value );
		}
		else
		if( transit>0f )
		{
			fSpeed = Library.Divide( 1f, transit );
		}

		Enable();
	}

	public void SetSize( float value )
	{
		if( Axis()==AXIS.VERTICAL )
		{
			Value().sizeDelta		= new Vector2( Size.x, Size.y*value );
			Value().localPosition	= new Vector3( Pivot.x+Value().sizeDelta.x/2, Pivot.y+Value().sizeDelta.y/2, Value().localPosition.z );
		}
		else
		{
			Value().sizeDelta		= new Vector2( Size.x*value, Size.y );
			Value().localPosition	= new Vector3( Pivot.x+Value().sizeDelta.x/2, Pivot.y+Value().sizeDelta.y/2, Value().localPosition.z );
		}

		if( fSpeed>0 )
		{
			fSmooth	= value;
		}

		if( Colores!=null && Colores.Count>0 )
		{
			SetColor( value );
		}
	}

	// 문자열을 입력하기 위한 함수
	public void Set( string text )
	{
		if( text==null ) return;

		if( text!=null )
		{
			Text().text = text;
		}
	}

	//입력된 수치 값을 얻기 위한 함수
	public float Get()
	{
		return fValue;
	}

	//문자열 객체를 얻기 위한 함수
	public Text Text()
	{
		return m_text;
	}

	//색상을 설정하기 위한 함수
	void SetColor( float value )
	{
		float	divide	= Library.Divide( 1f, Colores.Count );
		int		nIndex	= (int)Library.Divide( value, divide );

		if( nIndex>=Colores.Count )
		{
			Image().color = Colores[Colores.Count-1];
		}
		else
		if( nIndex>=Colores.Count-1 )
		{
			float fLerp = 1-Library.Divide( value-divide*(nIndex-1), divide );
			Image().color = Color.Lerp( Colores[nIndex], Colores[nIndex-1], fLerp );
		}
		else
		if( nIndex>=0 && nIndex<Colores.Count-1 )
		{
			float fLerp = 1-Library.Divide( value-divide*nIndex, divide );
			Image().color = Color.Lerp( Colores[nIndex+1], Colores[nIndex], fLerp );
		}
		else
		if( Colores.Count>0 )
		{
			Image().color = Colores[0];
		}
	}

	//활성화를 설정하기 위한 함수
	void Enable( bool enable=true )
	{
		if( enabled!=enable )
		{
			enabled = enable;
		}
	}

	public float GetSmoothValue()
	{
		return fSmooth;
	}

	float GetParentWidth( RectTransform transform=null )
	{
//		if( parent==null ) return new Vector2();	//(NULL)값을 허용함

		if( transform==null )
		{
			if( Transform()!=Value() )
			{
				transform = Transform();
			}
			else
			{
				transform = Value().parent as RectTransform;
			}
		}

		if( transform!=null )
		{
			if( transform.sizeDelta.x!=0 && transform.anchorMin.x==0.5f && transform.anchorMax.x==0.5f )
			{
				return transform.sizeDelta.x;
			}
			else
			{
				RectTransform parent = transform.parent as RectTransform;
				if( parent!=null )
				{
					return GetParentWidth(parent);
				}
				else
				{
					return transform.sizeDelta.x;
				}
			}
		}

		return 0f;
	}

	float GetParentHeight( RectTransform transform=null )
	{
//		if( parent==null ) return new Vector2();	//(NULL)값을 허용함

		if( transform==null )
		{
			if( Transform()!=Value() )
			{
				transform = Transform();
			}
			else
			{
				transform = Value().parent as RectTransform;
			}
		}

		if( transform!=null )
		{
			if( transform.sizeDelta.y!=0 && transform.anchorMin.y==0.5f && transform.anchorMax.y==0.5f )
			{
				return transform.sizeDelta.y;
			}
			else
			{
				RectTransform parent = transform.parent as RectTransform;
				if( parent!=null )
				{
					return GetParentWidth(parent);
				}
				else
				{
					return transform.sizeDelta.y;
				}
			}
		}

		return 0f;
	}

	//인터페이스를 활성화 하기 위한 함수
	public void Active()
	{
		if( Transform()!=Value() )
		{
			Library.Active( _GameObject() );
		}
		else
		{
			Library.Active( Value().parent.gameObject );
		}
	}

	//인터페이스를 비활성화 하기 위한 함수
	public void Inactive()
	{
		if( Transform()!=Value() )
		{
			Library.Inactive( _GameObject() );
		}
		else
		{
			Library.Inactive( Value().parent.gameObject );
		}
	}

	//활성화 되어 있는지 확인하기 위한 함수
	public bool IsActive( bool in_hierarchy=false )
	{
		if( in_hierarchy )
		{
			return _GameObject().activeInHierarchy;
		}

		return _GameObject().activeSelf;
	}

	//활성화 되어 있는지 확인하기 위한 함수
	public bool IsEnable( bool in_active=false )
	{
		if( in_active )
		{
			return isActiveAndEnabled;
		}

		return enabled;
	}

	AXIS Axis()
	{
		return nAxis;
	}
}