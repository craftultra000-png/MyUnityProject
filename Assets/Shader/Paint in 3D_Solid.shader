Shader "Paint in 3D/Solid" {
	Properties {
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
		[NoScaleOffset] _MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
		[NoScaleOffset] [Normal] _BumpMap ("Normal (RGBA)", 2D) = "bump" {}
		[NoScaleOffset] _MetallicGlossMap ("Metallic (R) Occlusion (G) Smoothness (B)", 2D) = "white" {}
		[NoScaleOffset] _EmissionMap ("Emission (RGB)", 2D) = "black" {}
		_Color ("Color", Vector) = (1,1,1,1)
		_BumpScale ("Normal Map Strength", Range(0, 5)) = 1
		_Metallic ("Metallic", Range(0, 1)) = 0
		_GlossMapScale ("Smoothness", Range(0, 1)) = 1
		_EmissionScale ("Emission Scale", Float) = 1
		_Tiling ("Tiling (XY)", Vector) = (1,1,0,0)
		[Toggle(_USE_UV2)] _UseUV2 ("Use Second UV", Float) = 0
		[Enum(Both Sides,0,Back Face,1,Front Face,2)] _P3D_Cull ("Show", Float) = 2
		[Header(OVERRIDE SETTINGS)] [Toggle(_USE_UV2_ALT)] _UseUV2Alt ("	Use Second UV", Float) = 1
		[Toggle(_OVERRIDE_OPACITY)] _EnableOpacity ("	Enable Opacity", Float) = 0
		[Toggle(_OVERRIDE_NORMAL)] _EnableNormal ("	Enable Normal", Float) = 0
		[Toggle(_OVERRIDE_MOS)] _EnableMos ("	Enable MOS", Float) = 0
		[Toggle(_OVERRIDE_EMISSION)] _EnableEmission ("	Enable Emission", Float) = 0
		[Header(OVERRIDES)] [NoScaleOffset] _AlbedoTex ("	Premultiplied Albedo (RGB) Weight (A)", 2D) = "black" {}
		[NoScaleOffset] _OpacityTex ("	Premultiplied Opacity (R) Weight (A)", 2D) = "black" {}
		[NoScaleOffset] _NormalTex ("	Premultiplied Normal (RG) Weight (A)", 2D) = "black" {}
		[NoScaleOffset] _MosTex ("	Premultiplied Metallic (R) Occlusion (G) Smoothness (B) Weight (A)", 2D) = "black" {}
		[NoScaleOffset] _EmissionTex ("	Premultiplied Emission (RGB) Weight (A)", 2D) = "black" {}
		[Header(UNITY FOG)] [Toggle(DISABLEFOG)] _CW_DisableFog ("	Disable", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _MainTex_ST;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Vertex_Stage_Output
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.uv = (input.uv.xy * _MainTex_ST.xy) + _MainTex_ST.zw;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			Texture2D<float4> _MainTex;
			SamplerState sampler_MainTex;
			float4 _Color;

			struct Fragment_Stage_Input
			{
				float2 uv : TEXCOORD0;
			};

			float4 frag(Fragment_Stage_Input input) : SV_TARGET
			{
				return _MainTex.Sample(sampler_MainTex, input.uv.xy) * _Color;
			}

			ENDHLSL
		}
	}
}