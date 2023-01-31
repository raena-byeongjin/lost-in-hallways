using System;
using UnityEngine;

public class Encrypt
{
	private const int bit1 = 243;
	private const int bit2 = 126;
	private const int bit3 = 135;
	private const int bit4 = 39;

	// 데이타를 암호화 하기 위한 함수
    public static byte[] Encode( byte[] bytes )
    {
        if( bytes==null || bytes.Length<=0 ) return null;

        for( int i=0; i<bytes.Length; i++ )
        {
            bytes[i] = Encode( bytes[i] );
        }

        return bytes;
    }

	//데이타를 암호화 하기 위한 함수
    public static byte Encode( byte value )
    {
		return (byte)( value ^ bit1 ^ bit2 ^ bit3 ^ bit4 );
    }

	//암호화 하기 위한 함수
    public static string Encode( string value, int density=1, int header=0 )
    {
        if( !Library.Is(value) ) return null;
        if( density<=0 ) return null;

		byte[] bytes = System.Text.Encoding.ASCII.GetBytes( value.ToCharArray() );
        string result = null;

		if( header>0 )
		{
			int End = Mathf.Min( header, bytes.Length );
			for( int i=0; i<End; i++ )
			{
				result += Library.DecToHex( (sbyte)bytes[i] ^ bit1 ^ bit2 ^ bit3 ^ bit4 );
			}
		}

        for( int i=0; i<bytes.Length; i+=density )
        {
            result += Library.DecToHex( (sbyte)bytes[i] ^ bit1 ^ bit2 ^ bit3 ^ bit4 );
        }

        return result;
    }

	//복호화 하기 위한 함수
    public static string Decode( string value, int density=1, int header=0 )
    {
        if( !Library.Is(value) ) return null;
        if( density<=0 ) return null;

		return Encode( value, density, header );
    }

	//암호화 하기 위한 함수
    public static byte[] Encode( byte[] bytes, int density=1, int header=0 )
    {
        if( bytes==null || bytes.Length<=0 ) return null;
        if( density<=0 ) return null;

		if( header>0 )
		{
			int End = Mathf.Min( header, bytes.Length );
			for( int i=0; i<End; i++ )
			{
				bytes[i] = (byte)( bytes[i] ^ bit1 ^ bit2 ^ bit3 ^ bit4 );
			}
		}

        for( int i=0; i<bytes.Length; i+=density )
        {
            bytes[i] = (byte)( bytes[i] ^ bit1 ^ bit2 ^ bit3 ^ bit4 );
        }

        return bytes;
    }

	//복호화 하기 위한 함수
    public static byte[] Decode( byte[] bytes, int density=1, int header=0 )
    {
        if( bytes==null || bytes.Length<=0 ) return null;
        if( density<=0 ) return null;

		return Encode( bytes, density, header );
    }
}