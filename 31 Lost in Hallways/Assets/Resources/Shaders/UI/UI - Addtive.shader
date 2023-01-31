// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/Additive"
 {
     Properties
     {
         [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
         _Color ("Tint", Color) = (1,1,1,1)
         
         _StencilComp ("Stencil Comparison", Float) = 8
         _Stencil ("Stencil ID", Float) = 0
         _StencilOp ("Stencil Operation", Float) = 0
         _StencilWriteMask ("Stencil Write Mask", Float) = 255
         _StencilReadMask ("Stencil Read Mask", Float) = 255
 
         _ColorMask ("Color Mask", Float) = 15
 
         [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
 
         _FlowTex("Flow Texture", 2D) = "grey" {}
         _XMul ("UV.x mul", float) = 0.25
         _YMul ("UV.y mul", float) = 0.25
         _Speed ("Speed", float) = 0.05
     }
 
     SubShader
     {
         Tags
         { 
             "Queue"="Transparent" 
             "IgnoreProjector"="True" 
             "RenderType"="Transparent" 
             "PreviewType"="Plane"
             "CanUseSpriteAtlas"="True"
         }
         
         Stencil
         {
             Ref [_Stencil]
             Comp [_StencilComp]
             Pass [_StencilOp] 
             ReadMask [_StencilReadMask]
             WriteMask [_StencilWriteMask]
         }
 
         Cull Off
         Lighting Off
         ZWrite Off
         ZTest [unity_GUIZTestMode]
         Blend SrcAlpha OneMinusSrcAlpha
         ColorMask [_ColorMask]
 
         Pass
         {
         CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
 
             #include "UnityCG.cginc"
             #include "UnityUI.cginc"
 
             #pragma multi_compile __ UNITY_UI_ALPHACLIP
             
             struct appdata_t
             {
                 float4 vertex   : POSITION;
                 float4 color    : COLOR;
                 float2 texcoord : TEXCOORD0;
                 float2 flowcoord : TEXCOORD1;
             };
 
             struct v2f
             {
                 float4 vertex   : SV_POSITION;
                 fixed4 color    : COLOR;
                 half2 texcoord  : TEXCOORD0;
                 float2 flowcoord : TEXCOORD1;
                 float4 worldPosition : TEXCOORD2;
             };
             
             fixed4 _Color;
             fixed4 _TextureSampleAdd;
             float4 _ClipRect;
 
             v2f vert(appdata_t IN)
             {
                 v2f OUT;
                 OUT.worldPosition = IN.vertex;
                 OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
 
                 OUT.texcoord = IN.texcoord;
                 
                 #ifdef UNITY_HALF_TEXEL_OFFSET
                 OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
                 #endif
 
                 OUT.flowcoord = IN.flowcoord;
 
                 OUT.color = IN.color * _Color;
                 return OUT;
             }
 
             uniform sampler2D _MainTex;
             uniform sampler2D _FlowTex;
             uniform fixed _XMul;
                uniform fixed _YMul;
             uniform fixed _Speed;
 
             fixed4 frag(v2f IN) : SV_Target
             {
                 fixed phase = _Time[1];
                 fixed2 flowDir = fixed2(_XMul, _YMul) * _Speed;
                 fixed2 flowUV = IN.flowcoord + flowDir * phase;
                 fixed4 tex1 = tex2D(_FlowTex, flowUV);
                 fixed4 tex0 = tex2D(_MainTex, IN.texcoord);
 
                 fixed4 add = tex1 < .5 ? 2.0 * tex1 * tex0 : 1.0 - 2.0 * (1.0 - tex1) * (1.0 - tex0); //additive calc
 
                 fixed4 c = (add + _TextureSampleAdd) * IN.color;
 
                 c.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                 
                 #ifdef UNITY_UI_ALPHACLIP
                 clip (c.a - 0.001);
                 #endif
 
                 return c;
             }
         ENDCG
         }
     }
 }