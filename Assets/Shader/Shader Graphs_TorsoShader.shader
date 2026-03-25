Shader "Shader Graphs/TorsoShader" {
	Properties {
		_MainColor ("MainColor", Vector) = (0.4622642,0.4622642,0.4622642,1)
		_RemoveColor ("RemoveColor", Vector) = (0.9150943,0.4963955,0.4963955,1)
		_EquipColor ("EquipColor", Vector) = (0,0,0,0)
		_Speed ("Speed", Float) = 4
		[ToggleUI] _isBlink ("isBlink", Float) = 0
		[ToggleUI] _isActive ("isActive", Float) = 0
		_BlinkPosition ("BlinkPosition", Vector) = (0,0.1,0,0)
		[ToggleUI] _Base ("Base", Float) = 0
		[ToggleUI] _Level1 ("Level1", Float) = 0
		[ToggleUI] _Level2 ("Level2", Float) = 0
		_LevelColor0 ("LevelColor0", Vector) = (0.8867924,0.7350584,0.2718939,1)
		_LevelColor1 ("LevelColor1", Vector) = (0.4802435,0.9137255,0.427451,1)
		_LevelColor2 ("LevelColor2", Vector) = (0.9150943,0.4273318,0.6136684,1)
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