using System.Collections;
using UnityEngine.Networking;

public class AppsParameter
{
	private Hashtable hash = null;
    public ArrayList array = null;
    public HttpProtocol request = null;

    AppsParameter( string value )
    {
        array = SemsJSONExtensions.arrayListFromJson( value );
    }

    AppsParameter( Hashtable hash )
    {
		this.hash = hash;
    }

    public static AppsParameter JsonParse( string text )
    {
        if( !Library.Is(text) ) return null;
        return new AppsParameter( text );
    }

    public static AppsParameter Initialize( Hashtable hash )
    {
        if( hash==null ) return null;
	    return new AppsParameter( hash );
    }

    //JSON 포맷을 해제하기 위한 함수
    public static AppsParameter Decode( string value )
    {
        if( !Library.Is(value) ) return null;
        return Initialize( AppsJson.jsonDecode(value) as Hashtable );
    }

	//파라메터 정보를 얻기 위한 함수
    public bool Is( string column )
    {
        if( !Library.Is(column) ) return false;
        if( hash==null ) return false;

        if( hash.Contains(column) )
        {
			return true;
        }

        return false;
    }

	//파라메터 정보를 얻기 위한 함수
    public string Get( string column )
    {
        if( !Library.Is(column) ) return null;
        if( hash==null ) return null;

        if( hash.Contains(column) )
        {
            if( hash[column]!=null )
            {
                return hash[column].ToString();
            }
        }

        return null;
    }

	//불형식의 파라메터 정보를 얻기 위한 함수
    public bool GetBoolean( string column )
    {
		if( !Library.Is(column) ) return false;

		string value = Get(column);
		if( value==null ) return false;

        if( value==true.ToString() || value=="1" )
        {
            return true;
        }

        return false;
    }

	//파라메터 정보를 얻기 위한 함수
    public int GetInt( string column )
    {
		if( !Library.Is(column) ) return 0;

		string value = Get(column);
		if( !Library.Is(value) ) return 0;

		if( Library.IsNumber(value) )
		{
			return System.Convert.ToInt32(value);
		}
		else
		if( Library.IsSingle(value) )
		{
			return (int)System.Convert.ToSingle(value);
		}

		return 0;
    }

	//파라메터 정보를 얻기 위한 함수
    public long GetLong( string column )
    {
		if( !Library.Is(column) ) return 0;

		string value = Get(column);
		if( !Library.Is(value) ) return 0;

        return System.Convert.ToInt64(value);
    }

	//파라메터 정보를 얻기 위한 함수
    public ulong GetULong( string column )
    {
		if( !Library.Is(column) ) return 0;

		string value = Get(column);
		if( !Library.Is(value) ) return 0;

        return System.Convert.ToUInt64(value);
    }

	//실수를 얻기 위한 함수
    public float GetFloat( string column )
    {
		if( !Library.Is(column) ) return 0f;

        string value = Get(column);
        if( !Library.Is(value) ) return 0f;

		if( Library.IsSingle(value) )
        {
            return System.Convert.ToSingle(value);
        }

        return 0f;
    }

	//실수를 얻기 위한 함수
    public decimal GetDecimal( string column )
    {
		if( !Library.Is(column) ) return 0;

        string value = Get(column);
        if( !Library.Is(value) ) return 0;

		if( Library.IsSingle(value) )
        {
            return System.Convert.ToDecimal(value);
        }

        return 0;
    }

	//파라메터 정보를 얻기 위한 함수
    public object GetNative( string column )
    {
        if( !Library.Is(column) ) return null;
        if( hash==null ) return null;

        if( hash.Contains(column) )
        {
            if( hash[column]!=null )
            {
                return hash[column];
            }
        }

        return null;
    }

	public override string ToString()
	{
		if( hash!=null )
		{
			string text = null;
			if( Request()!=null )
			{
				text = "("+Get("Compile")+") "+GetUrl()+"\r\n";
			}

			foreach( string key in hash.Keys )
			{
				if( hash[key]!=null && hash[key].GetType()==typeof(Hashtable) )
				{
					text += key+" => "+AppsJson.jsonEncode(hash[key])+"\r\n";
				}
				else
				{
					text += key+" => "+hash[key]+"\r\n";
				}
			}

			return text;
		}
		else
		if( Request()!=null )
		{
			return GetUrl();
		}

		return base.ToString();
	}

	//프로토콜 객체를 얻기 위한 함수
    public HttpProtocol Request()
    {
        return request;
    }

	//프로토콜 객체를 얻기 위한 함수
    public string GetUrl()
    {
		if( Request()!=null )
		{
			return Request().GetUrl();
		}

		return null;
    }
}