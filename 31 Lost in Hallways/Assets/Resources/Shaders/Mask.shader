
// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Byeongjin/Mask"
{
	Properties
	{
//		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D)	= "white" {}
		_MaskTex ("Alpha (A)", 2D)	= "white" {}
	}

	SubShader
	{
		LOD 200
		Tags
		{
			"Queue"				= "AlphaTest"
			"IgnoreProjector"	= "True"
			"RenderType"		= "TransparentCutout"
		}

		CGPROGRAM
		#pragma surface surf Lambert alpha:fade // alphatest:_Cutoff
        #include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _MaskTex;
//		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf( Input IN, inout SurfaceOutput o )
		{
			fixed4 c	= tex2D( _MainTex, IN.uv_MainTex );// * _Color;
			fixed4 mask	= tex2D( _MaskTex, IN.uv_MainTex );// * i.color;

			o.Albedo	= c.rgb;
			o.Alpha		= mask.a;
		}
		ENDCG
	}

	Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}