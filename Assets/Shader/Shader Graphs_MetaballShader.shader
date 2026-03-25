Shader "Shader Graphs/MetaballShader" {
	Properties {
		_MainColor ("MainColor", Vector) = (0.9056604,0.02135991,0.02135991,1)
		_SubColor ("SubColor", Vector) = (0.3629253,0.6886792,0.2891153,0)
		[HDR] _SpecularColor ("SpecularColor", Vector) = (0,0,0,0)
		_Smoothness ("Smoothness", Float) = 0
		_Ambient ("Ambient", Float) = 0
		_VertexPower ("VertexPower", Float) = 0.005
		[NoScaleOffset] _WaterTex ("WaterTex", 2D) = "white" {}
		[NoScaleOffset] _NormalTex ("NormalTex", 2D) = "white" {}
		_NormalTilling ("NormalTilling", Float) = 0.5
		_NormalPower ("NormalPower", Float) = 2
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