// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FX/MirrorReflection"
{
	Properties
	{
		
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TintColor("Tint Color", Color) = (.34, .85, .92, 0.1)
		[HideInInspector] _ReflectionTex ("", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 500
 
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		    #pragma multi_compile_instancing
			#include "UnityCG.cginc"


			 struct appdata
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 refl : TEXCOORD1;
				float4 pos : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
			};
			float4x4 _ProjMatrix;
			float4 _MainTex_ST;
			float4 _TintColor;

		

			v2f vert(appdata v)
			{
				v2f o;
		
				UNITY_SETUP_INSTANCE_ID(v); //Insert
				UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert

				o.pos = UnityObjectToClipPos (v.pos);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.refl = mul (_ProjMatrix, v.pos);
				return o;
			}
			sampler2D _MainTex;
			sampler2D _ReflectionTex;

			fixed4 frag(v2f i) : SV_Target
			{

				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i)

				fixed4 tex = tex2D(_MainTex, i.uv);
				fixed4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.refl));
				return (tex * refl) * (1.0-_TintColor.a) + _TintColor * _TintColor.a;
			}
			ENDCG
	    }
	}
}