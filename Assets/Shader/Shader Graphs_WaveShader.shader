Shader "Shader Graphs/WaveShader" {
	Properties {
		[NoScaleOffset] Texture2D_5304786eaf02426c9a9235461c5d31a7 ("Texture2D", 2D) = "white" {}
		Vector2_4f8c851b68b748459bd573c964679fcc ("Tiling", Vector) = (1,1,0,0)
		[HDR] Color_e7f99cd6c65145e08271d58042312329 ("EmissionColor", Vector) = (0,0,0,0)
		Color_ebfb2ec0cfd24984ab3d43acc5de9ac4 ("SpecularColor", Vector) = (0,0,0,0)
		Vector1_67c8f9d7bbba4750bfd9a4f172fd61a9 ("Smoothness", Float) = 0
		Vector1_4e857c5e20f24326a99e1604ef49f0e4 ("NormalStrength", Float) = 0.5
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}