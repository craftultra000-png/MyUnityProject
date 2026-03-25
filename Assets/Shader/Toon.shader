Shader "Toon" {
	Properties {
		[HideInInspector] _simpleUI ("SimpleUI", Float) = 0
		[Enum(OFF, 0, ON, 1)] [HideInInspector] _isUnityToonshader ("Material is touched by Unity Toon Shader", Float) = 1
		[HideInInspector] _utsVersionX ("VersionX", Float) = 0
		[HideInInspector] _utsVersionY ("VersionY", Float) = 10
		[HideInInspector] _utsVersionZ ("VersionZ", Float) = 2
		[HideInInspector] _utsTechnique ("Technique", Float) = 0
		_AutoRenderQueue ("Automatic Render Queue ", Float) = 1
		[Enum(OFF, 0, StencilOut, 1, StencilMask, 2)] _StencilMode ("StencilMode", Float) = 0
		_StencilComp ("Stencil Comparison", Float) = 8
		_StencilNo ("Stencil No", Float) = 1
		_StencilOpPass ("Stencil Operation", Float) = 0
		_StencilOpFail ("Stencil Operation", Float) = 0
		[Enum(OFF, 0, ON, 1,] _TransparentEnabled ("Transparent Mode", Float) = 0
		[Enum(OFF, 0, ON, 1, TRANSMODE, 2)] _ClippingMode ("CliippingMode", Float) = 0
		[Enum(OFF, 0, FRONT, 1, BACK, 2)] _CullMode ("Cull Mode", Float) = 2
		[Enum(OFF, 0, ONT, 1)] _ZWriteMode ("ZWrite Mode", Float) = 1
		[Enum(OFF, 0, ONT, 1)] _ZOverDrawMode ("ZOver Draw Mode", Float) = 0
		_SPRDefaultUnlitColorMask ("SPRDefaultUnlit Path Color Mask", Float) = 15
		[Enum(OFF, 0, FRONT, 1, BACK, 2)] _SRPDefaultUnlitColMode ("SPRDefaultUnlit  Cull Mode", Float) = 1
		_ClippingMask ("ClippingMask", 2D) = "white" {}
		_IsBaseMapAlphaAsClippingMask ("IsBaseMapAlphaAsClippingMask", Float) = 0
		[Toggle(_)] _Inverse_Clipping ("Inverse_Clipping", Float) = 0
		_Clipping_Level ("Clipping_Level", Range(0, 1)) = 0
		_Tweak_transparency ("Tweak_transparency", Range(-1, 1)) = 0
		[Enum(OFF,0,FRONT,1,BACK,2)] _CullMode ("Cull Mode", Float) = 2
		_MainTex ("BaseMap", 2D) = "white" {}
		_BaseMap ("BaseMap", 2D) = "white" {}
		_BaseColor ("BaseColor", Vector) = (1,1,1,1)
		_Color ("Color", Vector) = (1,1,1,1)
		[Toggle(_)] _Is_LightColor_Base ("Is_LightColor_Base", Float) = 1
		_1st_ShadeMap ("1st_ShadeMap", 2D) = "white" {}
		[Toggle(_)] _Use_BaseAs1st ("Use BaseMap as 1st_ShadeMap", Float) = 0
		_1st_ShadeColor ("1st_ShadeColor", Vector) = (1,1,1,1)
		[Toggle(_)] _Is_LightColor_1st_Shade ("Is_LightColor_1st_Shade", Float) = 1
		_2nd_ShadeMap ("2nd_ShadeMap", 2D) = "white" {}
		[Toggle(_)] _Use_1stAs2nd ("Use 1st_ShadeMap as 2nd_ShadeMap", Float) = 0
		_2nd_ShadeColor ("2nd_ShadeColor", Vector) = (1,1,1,1)
		[Toggle(_)] _Is_LightColor_2nd_Shade ("Is_LightColor_2nd_Shade", Float) = 1
		_NormalMap ("NormalMap", 2D) = "bump" {}
		_BumpScale ("Normal Scale", Range(0, 1)) = 1
		[Toggle(_)] _Is_NormalMapToBase ("Is_NormalMapToBase", Float) = 0
		[Toggle(_)] _Set_SystemShadowsToBase ("Set_SystemShadowsToBase", Float) = 1
		_Tweak_SystemShadowsLevel ("Tweak_SystemShadowsLevel", Range(-0.5, 0.5)) = 0
		_BaseColor_Step ("BaseColor_Step", Range(0, 1)) = 0.5
		_BaseShade_Feather ("Base/Shade_Feather", Range(0.0001, 1)) = 0.0001
		_ShadeColor_Step ("ShadeColor_Step", Range(0, 1)) = 0
		_1st2nd_Shades_Feather ("1st/2nd_Shades_Feather", Range(0.0001, 1)) = 0.0001
		_1st_ShadeColor_Step ("1st_ShadeColor_Step", Range(0, 1)) = 0.5
		_1st_ShadeColor_Feather ("1st_ShadeColor_Feather", Range(0.0001, 1)) = 0.0001
		_2nd_ShadeColor_Step ("2nd_ShadeColor_Step", Range(0, 1)) = 0
		_2nd_ShadeColor_Feather ("2nd_ShadeColor_Feather", Range(0.0001, 1)) = 0.0001
		_StepOffset ("Step_Offset (ForwardAdd Only)", Range(-0.5, 0.5)) = 0
		[Toggle(_)] _Is_Filter_HiCutPointLightColor ("PointLights HiCut_Filter (ForwardAdd Only)", Float) = 1
		_Set_1st_ShadePosition ("Set_1st_ShadePosition", 2D) = "white" {}
		_Set_2nd_ShadePosition ("Set_2nd_ShadePosition", 2D) = "white" {}
		_ShadingGradeMap ("ShadingGradeMap", 2D) = "white" {}
		_Tweak_ShadingGradeMapLevel ("Tweak_ShadingGradeMapLevel", Range(-0.5, 0.5)) = 0
		_BlurLevelSGM ("Blur Level of ShadingGradeMap", Range(0, 10)) = 0
		_HighColor ("HighColor", Vector) = (0,0,0,1)
		_HighColor_Tex ("HighColor_Tex", 2D) = "white" {}
		[Toggle(_)] _Is_LightColor_HighColor ("Is_LightColor_HighColor", Float) = 1
		[Toggle(_)] _Is_NormalMapToHighColor ("Is_NormalMapToHighColor", Float) = 0
		_HighColor_Power ("HighColor_Power", Range(0, 1)) = 0
		[Toggle(_)] _Is_SpecularToHighColor ("Is_SpecularToHighColor", Float) = 0
		[Toggle(_)] _Is_BlendAddToHiColor ("Is_BlendAddToHiColor", Float) = 0
		[Toggle(_)] _Is_UseTweakHighColorOnShadow ("Is_UseTweakHighColorOnShadow", Float) = 0
		_TweakHighColorOnShadow ("TweakHighColorOnShadow", Range(0, 1)) = 0
		_Set_HighColorMask ("Set_HighColorMask", 2D) = "white" {}
		_Tweak_HighColorMaskLevel ("Tweak_HighColorMaskLevel", Range(-1, 1)) = 0
		[Toggle(_)] _RimLight ("RimLight", Float) = 0
		_RimLightColor ("RimLightColor", Vector) = (1,1,1,1)
		[Toggle(_)] _Is_LightColor_RimLight ("Is_LightColor_RimLight", Float) = 1
		[Toggle(_)] _Is_NormalMapToRimLight ("Is_NormalMapToRimLight", Float) = 0
		_RimLight_Power ("RimLight_Power", Range(0, 1)) = 0.1
		_RimLight_InsideMask ("RimLight_InsideMask", Range(0.0001, 1)) = 0.0001
		[Toggle(_)] _RimLight_FeatherOff ("RimLight_FeatherOff", Float) = 0
		[Toggle(_)] _LightDirection_MaskOn ("LightDirection_MaskOn", Float) = 0
		_Tweak_LightDirection_MaskLevel ("Tweak_LightDirection_MaskLevel", Range(0, 0.5)) = 0
		[Toggle(_)] _Add_Antipodean_RimLight ("Add_Antipodean_RimLight", Float) = 0
		_Ap_RimLightColor ("Ap_RimLightColor", Vector) = (1,1,1,1)
		[Toggle(_)] _Is_LightColor_Ap_RimLight ("Is_LightColor_Ap_RimLight", Float) = 1
		_Ap_RimLight_Power ("Ap_RimLight_Power", Range(0, 1)) = 0.1
		[Toggle(_)] _Ap_RimLight_FeatherOff ("Ap_RimLight_FeatherOff", Float) = 0
		_Set_RimLightMask ("Set_RimLightMask", 2D) = "white" {}
		_Tweak_RimLightMaskLevel ("Tweak_RimLightMaskLevel", Range(-1, 1)) = 0
		[Toggle(_)] _MatCap ("MatCap", Float) = 0
		_MatCap_Sampler ("MatCap_Sampler", 2D) = "black" {}
		_BlurLevelMatcap ("Blur Level of MatCap_Sampler", Range(0, 10)) = 0
		_MatCapColor ("MatCapColor", Vector) = (1,1,1,1)
		[Toggle(_)] _Is_LightColor_MatCap ("Is_LightColor_MatCap", Float) = 1
		[Toggle(_)] _Is_BlendAddToMatCap ("Is_BlendAddToMatCap", Float) = 1
		_Tweak_MatCapUV ("Tweak_MatCapUV", Range(-0.5, 0.5)) = 0
		_Rotate_MatCapUV ("Rotate_MatCapUV", Range(-1, 1)) = 0
		[Toggle(_)] _CameraRolling_Stabilizer ("Activate CameraRolling_Stabilizer", Float) = 0
		[Toggle(_)] _Is_NormalMapForMatCap ("Is_NormalMapForMatCap", Float) = 0
		_NormalMapForMatCap ("NormalMapForMatCap", 2D) = "bump" {}
		_BumpScaleMatcap ("Scale for NormalMapforMatCap", Range(0, 1)) = 1
		_Rotate_NormalMapForMatCapUV ("Rotate_NormalMapForMatCapUV", Range(-1, 1)) = 0
		[Toggle(_)] _Is_UseTweakMatCapOnShadow ("Is_UseTweakMatCapOnShadow", Float) = 0
		_TweakMatCapOnShadow ("TweakMatCapOnShadow", Range(0, 1)) = 0
		_Set_MatcapMask ("Set_MatcapMask", 2D) = "white" {}
		_Tweak_MatcapMaskLevel ("Tweak_MatcapMaskLevel", Range(-1, 1)) = 0
		[Toggle(_)] _Inverse_MatcapMask ("Inverse_MatcapMask", Float) = 0
		[Toggle(_)] _Is_Ortho ("Orthographic Projection for MatCap", Float) = 0
		[Toggle(_)] _AngelRing ("AngelRing", Float) = 0
		_AngelRing_Sampler ("AngelRing_Sampler", 2D) = "black" {}
		_AngelRing_Color ("AngelRing_Color", Vector) = (1,1,1,1)
		[Toggle(_)] _Is_LightColor_AR ("Is_LightColor_AR", Float) = 1
		_AR_OffsetU ("AR_OffsetU", Range(0, 0.5)) = 0
		_AR_OffsetV ("AR_OffsetV", Range(0, 1)) = 0.3
		[Toggle(_)] _ARSampler_AlphaOn ("ARSampler_AlphaOn", Float) = 0
		[KeywordEnum(SIMPLE,ANIMATION)] _EMISSIVE ("EMISSIVE MODE", Float) = 0
		_Emissive_Tex ("Emissive_Tex", 2D) = "white" {}
		[HDR] _Emissive_Color ("Emissive_Color", Vector) = (0,0,0,1)
		_Base_Speed ("Base_Speed", Float) = 0
		_Scroll_EmissiveU ("Scroll_EmissiveU", Range(-1, 1)) = 0
		_Scroll_EmissiveV ("Scroll_EmissiveV", Range(-1, 1)) = 0
		_Rotate_EmissiveUV ("Rotate_EmissiveUV", Float) = 0
		[Toggle(_)] _Is_PingPong_Base ("Is_PingPong_Base", Float) = 0
		[Toggle(_)] _Is_ColorShift ("Activate ColorShift", Float) = 0
		[HDR] _ColorShift ("ColorSift", Vector) = (0,0,0,1)
		_ColorShift_Speed ("ColorShift_Speed", Float) = 0
		[Toggle(_)] _Is_ViewShift ("Activate ViewShift", Float) = 0
		[HDR] _ViewShift ("ViewSift", Vector) = (0,0,0,1)
		[Toggle(_)] _Is_ViewCoord_Scroll ("Is_ViewCoord_Scroll", Float) = 0
		[KeywordEnum(NML,POS)] _OUTLINE ("OUTLINE MODE", Float) = 0
		_Outline_Width ("Outline_Width", Float) = 0
		_Farthest_Distance ("Farthest_Distance", Float) = 100
		_Nearest_Distance ("Nearest_Distance", Float) = 0.5
		_Outline_Sampler ("Outline_Sampler", 2D) = "white" {}
		_Outline_Color ("Outline_Color", Vector) = (0.5,0.5,0.5,1)
		[Toggle(_)] _Is_BlendBaseColor ("Is_BlendBaseColor", Float) = 0
		[Toggle(_)] _Is_LightColor_Outline ("Is_LightColor_Outline", Float) = 1
		[Toggle(_)] _Is_OutlineTex ("Is_OutlineTex", Float) = 0
		_OutlineTex ("OutlineTex", 2D) = "white" {}
		_Offset_Z ("Offset_Camera_Z", Float) = 0
		[Toggle(_)] _Is_BakedNormal ("Is_BakedNormal", Float) = 0
		_BakedNormal ("Baked Normal for Outline", 2D) = "white" {}
		_GI_Intensity ("GI_Intensity", Range(0, 1)) = 0
		_Unlit_Intensity ("Unlit_Intensity", Range(0, 4)) = 0
		[Toggle(_)] _Is_Filter_LightColor ("VRChat : SceneLights HiCut_Filter", Float) = 1
		[Toggle(_)] _Is_BLD ("Advanced : Activate Built-in Light Direction", Float) = 0
		_Offset_X_Axis_BLD (" Offset X-Axis (Built-in Light Direction)", Range(-1, 1)) = -0.05
		_Offset_Y_Axis_BLD (" Offset Y-Axis (Built-in Light Direction)", Range(-1, 1)) = 0.09
		[Toggle(_)] _Inverse_Z_Axis_BLD (" Inverse Z-Axis (Built-in Light Direction)", Float) = 1
		[Toggle(_)] _BaseColorVisible ("Channel mask", Float) = 1
		[Toggle(_)] _BaseColorOverridden ("Channel mask", Float) = 0
		_BaseColorMaskColor ("chennel mask color", Vector) = (1,1,1,1)
		[Toggle(_)] _FirstShadeVisible ("Channel mask", Float) = 1
		[Toggle(_)] _FirstShadeOverridden ("Channel mask", Float) = 0
		_FirstShadeMaskColor ("chennel mask color", Vector) = (0,1,1,1)
		[Toggle(_)] _SecondShadeVisible ("Channel mask", Float) = 1
		[Toggle(_)] _SecondShadeOverridden ("Channel mask", Float) = 0
		_SecondShadeMaskColor ("chennel mask color", Vector) = (0,0,1,1)
		[Toggle(_)] _HighlightVisible ("Channel mask", Float) = 1
		[Toggle(_)] _HighlightOverridden ("Channel mask", Float) = 0
		_HighlightMaskColor ("Channel mask color", Vector) = (1,1,0,1)
		[Toggle(_)] _AngelRingVisible ("Channel mask", Float) = 1
		[Toggle(_)] _AngelRingOverridden ("Channel mask", Float) = 0
		_AngelRingMaskColor ("Channel mask color", Vector) = (0,1,0,1)
		[Toggle(_)] _RimLightVisible ("Channel mask", Float) = 1
		[Toggle(_)] _RimLightOverridden ("Channel mask", Float) = 0
		_RimLightMaskColor ("Channel mask color", Vector) = (1,0,1,1)
		[Toggle(_)] _OutlineVisible ("Channel mask", Float) = 1
		[Toggle(_)] _OutlineOverridden ("Channel mask", Float) = 0
		_OutlineMaskColor ("Channel mask color", Vector) = (0,0,0,1)
		[Toggle(_)] _ComposerMaskMode ("", Float) = 0
		[Enum(None, 0, BaseColor, 1, FirstShade, 2, SecondShade,3, Highlight, 4, AngelRing, 5, RimLight, 6)] _ClippingMatteMode ("Clipping Matte Mode", Float) = 0
		[HideInInspector] emissive ("to avoid srp batcher error", Vector) = (0,0,0,1)
		_BaseColorMap ("BaseColorMap", 2D) = "white" {}
		[HideInInspector] _BaseColorMap_MipInfo ("_BaseColorMap_MipInfo", Vector) = (0,0,0,0)
		_Metallic ("_Metallic", Range(0, 1)) = 0
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
		_MaskMap ("MaskMap", 2D) = "white" {}
		_SmoothnessRemapMin ("SmoothnessRemapMin", Float) = 0
		_SmoothnessRemapMax ("SmoothnessRemapMax", Float) = 1
		_AlphaRemapMin ("AlphaRemapMin", Float) = 0
		_AlphaRemapMax ("AlphaRemapMax", Float) = 1
		_AORemapMin ("AORemapMin", Float) = 0
		_AORemapMax ("AORemapMax", Float) = 1
		_NormalMapOS ("NormalMapOS", 2D) = "white" {}
		_NormalScale ("_NormalScale", Range(0, 8)) = 1
		_BentNormalMap ("_BentNormalMap", 2D) = "bump" {}
		_BentNormalMapOS ("_BentNormalMapOS", 2D) = "white" {}
		_HeightMap ("HeightMap", 2D) = "black" {}
		[HideInInspector] _HeightAmplitude ("Height Amplitude", Float) = 0.02
		[HideInInspector] _HeightCenter ("Height Center", Range(0, 1)) = 0.5
		[Enum(MinMax, 0, Amplitude, 1)] _HeightMapParametrization ("Heightmap Parametrization", Float) = 0
		_HeightOffset ("Height Offset", Float) = 0
		_HeightMin ("Heightmap Min", Float) = -1
		_HeightMax ("Heightmap Max", Float) = 1
		_HeightTessAmplitude ("Amplitude", Float) = 2
		_HeightTessCenter ("Height Center", Range(0, 1)) = 0.5
		_HeightPoMAmplitude ("Height Amplitude", Float) = 2
		_DetailMap ("DetailMap", 2D) = "linearGrey" {}
		_DetailAlbedoScale ("_DetailAlbedoScale", Range(0, 2)) = 1
		_DetailNormalScale ("_DetailNormalScale", Range(0, 2)) = 1
		_DetailSmoothnessScale ("_DetailSmoothnessScale", Range(0, 2)) = 1
		_TangentMap ("TangentMap", 2D) = "bump" {}
		_TangentMapOS ("TangentMapOS", 2D) = "white" {}
		_Anisotropy ("Anisotropy", Range(-1, 1)) = 0
		_AnisotropyMap ("AnisotropyMap", 2D) = "white" {}
		[HideInInspector] _DiffusionProfile ("Obsolete, kept for migration purpose", Float) = 0
		[HideInInspector] _DiffusionProfileAsset ("Diffusion Profile Asset", Vector) = (0,0,0,0)
		[HideInInspector] _DiffusionProfileHash ("Diffusion Profile Hash", Float) = 0
		_SubsurfaceMask ("Subsurface Radius", Range(0, 1)) = 1
		_SubsurfaceMaskMap ("Subsurface Radius Map", 2D) = "white" {}
		_TransmissionMask ("Transmission Mask", Range(0, 1)) = 1
		_Thickness ("Thickness", Range(0, 1)) = 1
		_ThicknessMap ("Thickness Map", 2D) = "white" {}
		_ThicknessRemap ("Thickness Remap", Vector) = (0,1,0,0)
		_IridescenceThickness ("Iridescence Thickness", Range(0, 1)) = 1
		_IridescenceThicknessMap ("Iridescence Thickness Map", 2D) = "white" {}
		_IridescenceThicknessRemap ("Iridescence Thickness Remap", Vector) = (0,1,0,0)
		_IridescenceMask ("Iridescence Mask", Range(0, 1)) = 1
		_IridescenceMaskMap ("Iridescence Mask Map", 2D) = "white" {}
		_CoatMask ("Coat Mask", Range(0, 1)) = 0
		_CoatMaskMap ("CoatMaskMap", 2D) = "white" {}
		[ToggleUI] _EnergyConservingSpecularColor ("_EnergyConservingSpecularColor", Float) = 1
		_SpecularColor ("SpecularColor", Vector) = (1,1,1,1)
		_SpecularColorMap ("SpecularColorMap", 2D) = "white" {}
		[Enum(Off, 0, From Ambient Occlusion, 1, From Bent Normals, 2)] _SpecularOcclusionMode ("Specular Occlusion Mode", Float) = 1
		[HDR] _EmissiveColor ("EmissiveColor", Vector) = (0,0,0,1)
		[HideInInspector] _EmissiveColorLDR ("EmissiveColor LDR", Vector) = (0,0,0,1)
		_EmissiveColorMap ("EmissiveColorMap", 2D) = "white" {}
		[ToggleUI] _AlbedoAffectEmissive ("Albedo Affect Emissive", Float) = 0
		[HideInInspector] _EmissiveIntensityUnit ("Emissive Mode", Float) = 0
		[ToggleUI] _UseEmissiveIntensity ("Use Emissive Intensity", Float) = 0
		_EmissiveIntensity ("Emissive Intensity", Float) = 1
		_EmissiveExposureWeight ("Emissive Pre Exposure", Range(0, 1)) = 1
		_DistortionVectorMap ("DistortionVectorMap", 2D) = "black" {}
		[ToggleUI] _DistortionEnable ("Enable Distortion", Float) = 0
		[ToggleUI] _DistortionDepthTest ("Distortion Depth Test Enable", Float) = 1
		[Enum(Add, 0, Multiply, 1, Replace, 2)] _DistortionBlendMode ("Distortion Blend Mode", Float) = 0
		[HideInInspector] _DistortionSrcBlend ("Distortion Blend Src", Float) = 0
		[HideInInspector] _DistortionDstBlend ("Distortion Blend Dst", Float) = 0
		[HideInInspector] _DistortionBlurSrcBlend ("Distortion Blur Blend Src", Float) = 0
		[HideInInspector] _DistortionBlurDstBlend ("Distortion Blur Blend Dst", Float) = 0
		[HideInInspector] _DistortionBlurBlendMode ("Distortion Blur Blend Mode", Float) = 0
		_DistortionScale ("Distortion Scale", Float) = 1
		_DistortionVectorScale ("Distortion Vector Scale", Float) = 2
		_DistortionVectorBias ("Distortion Vector Bias", Float) = -1
		_DistortionBlurScale ("Distortion Blur Scale", Float) = 1
		_DistortionBlurRemapMin ("DistortionBlurRemapMin", Float) = 0
		_DistortionBlurRemapMax ("DistortionBlurRemapMax", Float) = 1
		[ToggleUI] _UseShadowThreshold ("_UseShadowThreshold", Float) = 0
		[ToggleUI] _AlphaCutoffEnable ("Alpha Cutoff Enable", Float) = 0
		_AlphaCutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
		_AlphaCutoffShadow ("_AlphaCutoffShadow", Range(0, 1)) = 0.5
		_AlphaCutoffPrepass ("_AlphaCutoffPrepass", Range(0, 1)) = 0.5
		_AlphaCutoffPostpass ("_AlphaCutoffPostpass", Range(0, 1)) = 0.5
		[ToggleUI] _TransparentDepthPrepassEnable ("_TransparentDepthPrepassEnable", Float) = 0
		[ToggleUI] _TransparentBackfaceEnable ("_TransparentBackfaceEnable", Float) = 0
		[ToggleUI] _TransparentDepthPostpassEnable ("_TransparentDepthPostpassEnable", Float) = 0
		_TransparentSortPriority ("_TransparentSortPriority", Float) = 0
		[Enum(None, 0, Box, 1, Sphere, 2, Thin, 3)] _RefractionModel ("Refraction Model", Float) = 0
		[Enum(Proxy, 1, HiZ, 2)] _SSRefractionProjectionModel ("Refraction Projection Model", Float) = 0
		_Ior ("Index Of Refraction", Range(1, 2.5)) = 1
		_ThicknessMultiplier ("Thickness Multiplier", Float) = 1
		_TransmittanceColor ("Transmittance Color", Vector) = (1,1,1,1)
		_TransmittanceColorMap ("TransmittanceColorMap", 2D) = "white" {}
		_ATDistance ("Transmittance Absorption Distance", Float) = 1
		[ToggleUI] _TransparentWritingMotionVec ("_TransparentWritingMotionVec", Float) = 0
		[HideInInspector] _StencilRef ("_StencilRef", Float) = 2
		[HideInInspector] _StencilWriteMask ("_StencilWriteMask", Float) = 3
		[HideInInspector] _StencilRefGBuffer ("_StencilRefGBuffer", Float) = 2
		[HideInInspector] _StencilWriteMaskGBuffer ("_StencilWriteMaskGBuffer", Float) = 3
		[HideInInspector] _StencilRefDepth ("_StencilRefDepth", Float) = 0
		[HideInInspector] _StencilWriteMaskDepth ("_StencilWriteMaskDepth", Float) = 32
		[HideInInspector] _StencilRefMV ("_StencilRefMV", Float) = 128
		[HideInInspector] _StencilWriteMaskMV ("_StencilWriteMaskMV", Float) = 128
		[HideInInspector] _StencilRefDistortionVec ("_StencilRefDistortionVec", Float) = 64
		[HideInInspector] _StencilWriteMaskDistortionVec ("_StencilWriteMaskDistortionVec", Float) = 64
		[HideInInspector] _SurfaceType ("__surfacetype", Float) = 0
		[HideInInspector] _BlendMode ("__blendmode", Float) = 0
		[HideInInspector] _SrcBlend ("__src", Float) = 1
		[HideInInspector] _DstBlend ("__dst", Float) = 0
		[HideInInspector] _AlphaSrcBlend ("__alphaSrc", Float) = 1
		[HideInInspector] _AlphaDstBlend ("__alphaDst", Float) = 0
		[ToggleUI] [HideInInspector] _ZWrite ("__zw", Float) = 1
		[ToggleUI] [HideInInspector] _TransparentZWrite ("_TransparentZWrite", Float) = 0
		[HideInInspector] _CullMode ("__cullmode", Float) = 2
		[HideInInspector] _CullModeForward ("__cullmodeForward", Float) = 2
		[HideInInspector] _TransparentCullMode ("_TransparentCullMode", Float) = 2
		[HideInInspector] _ZTestDepthEqualForOpaque ("_ZTestDepthEqualForOpaque", Float) = 4
		[HideInInspector] _ZTestModeDistortion ("_ZTestModeDistortion", Float) = 8
		[HideInInspector] _ZTestGBuffer ("_ZTestGBuffer", Float) = 4
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTestTransparent ("Transparent ZTest", Float) = 4
		[ToggleUI] _EnableFogOnTransparent ("Enable Fog", Float) = 1
		[ToggleUI] _EnableBlendModePreserveSpecularLighting ("Enable Blend Mode Preserve Specular Lighting", Float) = 1
		[ToggleUI] _DoubleSidedEnable ("Double sided enable", Float) = 0
		[Enum(Flip, 0, Mirror, 1, None, 2)] _DoubleSidedNormalMode ("Double sided normal mode", Float) = 1
		[HideInInspector] _DoubleSidedConstants ("_DoubleSidedConstants", Vector) = (1,1,-1,0)
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3, Planar, 4, Triplanar, 5)] _UVBase ("UV Set for base", Float) = 0
		[Enum(WorldSpace, 0, ObjectSpace, 1)] _ObjectSpaceUVMapping ("Mapping space", Float) = 0
		_TexWorldScale ("Scale to apply on world coordinate", Float) = 1
		[HideInInspector] _InvTilingScale ("Inverse tiling scale = 2 / (abs(_BaseColorMap_ST.x) + abs(_BaseColorMap_ST.y))", Float) = 1
		[HideInInspector] _UVMappingMask ("_UVMappingMask", Vector) = (1,0,0,0)
		[Enum(TangentSpace, 0, ObjectSpace, 1)] _NormalMapSpace ("NormalMap space", Float) = 0
		[Enum(Subsurface Scattering, 0, Standard, 1, Anisotropy, 2, Iridescence, 3, Specular Color, 4, Translucent, 5)] _MaterialID ("MaterialId", Float) = 1
		[ToggleUI] _TransmissionEnable ("_TransmissionEnable", Float) = 1
		[Enum(None, 0, Vertex displacement, 1, Pixel displacement, 2)] _DisplacementMode ("DisplacementMode", Float) = 0
		[ToggleUI] _DisplacementLockObjectScale ("displacement lock object scale", Float) = 1
		[ToggleUI] _DisplacementLockTilingScale ("displacement lock tiling scale", Float) = 1
		[ToggleUI] _DepthOffsetEnable ("Depth Offset View space", Float) = 0
		[ToggleUI] _EnableGeometricSpecularAA ("EnableGeometricSpecularAA", Float) = 0
		_SpecularAAScreenSpaceVariance ("SpecularAAScreenSpaceVariance", Range(0, 1)) = 0.1
		_SpecularAAThreshold ("SpecularAAThreshold", Range(0, 1)) = 0.2
		_PPDMinSamples ("Min sample for POM", Range(1, 64)) = 5
		_PPDMaxSamples ("Max sample for POM", Range(1, 64)) = 15
		_PPDLodThreshold ("Start lod to fade out the POM effect", Range(0, 16)) = 5
		_PPDPrimitiveLength ("Primitive length for POM", Float) = 1
		_PPDPrimitiveWidth ("Primitive width for POM", Float) = 1
		[HideInInspector] _InvPrimScale ("Inverse primitive scale for non-planar POM", Vector) = (1,1,0,0)
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3)] _UVDetail ("UV Set for detail", Float) = 0
		[HideInInspector] _UVDetailsMappingMask ("_UVDetailsMappingMask", Vector) = (1,0,0,0)
		[ToggleUI] _LinkDetailsWithBase ("LinkDetailsWithBase", Float) = 1
		[Enum(Use Emissive Color, 0, Use Emissive Mask, 1)] _EmissiveColorMode ("Emissive color mode", Float) = 1
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3, Planar, 4, Triplanar, 5)] _UVEmissive ("UV Set for emissive", Float) = 0
		_TexWorldScaleEmissive ("Scale to apply on world coordinate", Float) = 1
		[HideInInspector] _UVMappingMaskEmissive ("_UVMappingMaskEmissive", Vector) = (1,0,0,0)
		_EmissionColor ("Color", Vector) = (1,1,1,1)
		_Color ("Color", Vector) = (1,1,1,1)
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
		[ToggleUI] _SupportDecals ("Support Decals", Float) = 1
		[ToggleUI] _ReceivesSSR ("Receives SSR", Float) = 1
		[ToggleUI] _AddPrecomputedVelocity ("AddPrecomputedVelocity", Float) = 0
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
	//CustomEditor "UnityEditor.Rendering.Toon.UTS3GUI"
}