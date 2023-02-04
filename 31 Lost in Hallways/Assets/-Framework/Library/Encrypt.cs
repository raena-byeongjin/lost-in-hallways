using System;
using UnityEngine;

public class Encrypt
{
	private static int[] defaultBit = new int[4]{ 243, 126, 135, 39 };

	// 데이타를 암호화 하기 위한 함수
    public static byte[] Encode( byte[] bytes, int[] bit=null )
    {
        if( bytes==null || bytes.Length<=0 ) return null;
//		if( bit==null ) return null; //(NULL)값을 허용함

        for( int i=0; i<bytes.Length; i++ )
        {
            bytes[i] = Encode( bytes[i], bit );
        }

        return bytes;
    }

	//데이타를 암호화 하기 위한 함수
    public static byte Encode( byte value, int[] bit=null )
    {
//		if( bit==null ) return 0; //(NULL)값을 허용함

		if( bit!=null && bit.Length>=4 )
		{
			return (byte)( value ^ bit[0] ^ bit[1] ^ bit[2] ^ bit[3] );
		}

		return (byte)( value ^ defaultBit[0] ^ defaultBit[1] ^ defaultBit[2] ^ defaultBit[3] );		
    }

	//암호화 하기 위한 함수
    public static string Encode( string value, int density=1, int header=0, int[] bitBody=null, int[] bitHeader=null, bool legacy=false )
    {
        if( !Library.Is(value) ) return null;
        if( density<=0 ) return null;
//		if( bitBody==null ) return null;	//(NULL)값을 허용함
//		if( bitHeader==null ) return null;	//(NULL)값을 허용함

		byte[] bytes = System.Text.Encoding.ASCII.GetBytes( value.ToCharArray() );
        string result = null;

		if( header>0 )
		{
			if( bitHeader==null || bitHeader.Length<4 ) bitHeader = bitBody;
			int End = Mathf.Min( header, bytes.Length );
			for( int i=0; i<End; i++ )
			{
//				result += Library.DecToHex( (sbyte)bytes[i] ^ defaultBit[0] ^ defaultBit[1] ^ defaultBit[2] ^ defaultBit[3] );
				result += Library.DecToHex( Encode( bytes[i], bitHeader ) );
			}
		}

		if( legacy )
		{
			for( int i=0; i<bytes.Length; i+=density )
			{
				result += Library.DecToHex( Encode( bytes[i], bitBody ) );
			}
		}
		else
		{
			for( int i=density; i<bytes.Length; i+=density )
			{
//				result += Library.DecToHex( (sbyte)bytes[i] ^ defaultBit[0] ^ defaultBit[1] ^ defaultBit[2] ^ defaultBit[3] );
				result += Library.DecToHex( Encode( bytes[i], bitBody ) );
			}
		}

        return result;
    }

	//복호화 하기 위한 함수
    public static string Decode( string value, int density=1, int header=0, int[] bitBody=null, int[] bitHeader=null, bool legacy=false )
    {
        if( !Library.Is(value) ) return null;
        if( density<=0 ) return null;
//		if( bitBody==null ) return null;	//(NULL)값을 허용함
//		if( bitHeader==null ) return null;	//(NULL)값을 허용함

		return Encode( value, density, header, bitBody, bitHeader, legacy );
    }

	//암호화 하기 위한 함수
    public static byte[] Encode( byte[] bytes, int density=1, int header=0, int[] bitBody=null, int[] bitHeader=null, bool legacy=false )
    {
        if( bytes==null || bytes.Length<=0 ) return null;
        if( density<=0 ) return null;
//		if( bitBody==null ) return null;	//(NULL)값을 허용함
//		if( bitHeader==null ) return null;	//(NULL)값을 허용함

		if( header>0 )
		{
			if( bitHeader==null || bitHeader.Length<4 ) bitHeader = bitBody;
			int End = Mathf.Min( header, bytes.Length );
			for( int i=0; i<End; i++ )
			{
				bytes[i] = Encode( bytes[i], bitHeader );
			}
		}

		if( legacy )
		{
			for( int i=0; i<bytes.Length; i+=density )
			{
				bytes[i] = Encode( bytes[i], bitBody );
			}
		}
		else
		{
			for( int i=density; i<bytes.Length; i+=density )
			{
				bytes[i] = Encode( bytes[i], bitBody );
			}
		}

        return bytes;
    }

	//복호화 하기 위한 함수
    public static byte[] Decode( byte[] bytes, int density=1, int header=0, int[] bitBody=null, int[] bitHeader=null, bool legacy=false )
    {
        if( bytes==null || bytes.Length<=0 ) return null;
        if( density<=0 ) return null;
//		if( bitBody==null ) return null;	//(NULL)값을 허용함
//		if( bitHeader==null ) return null;	//(NULL)값을 허용함

		return Encode( bytes, density, header, bitBody, bitHeader, legacy );
    }
}