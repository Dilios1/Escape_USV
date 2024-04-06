Shader "CTI/LOD Leaves 301" {
	Properties {
		[Space(5)] [Enum(UnityEngine.Rendering.CullMode)] _Culling ("Culling", Float) = 0
		[Space(5)] _HueVariation ("Color Variation*", Vector) = (0.9,0.5,0,0.1)
		[Space(5)] _MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
		_Cutoff ("Alpha Cutoff", Range(0, 1.5)) = 0.3
		[Space(5)] [NoScaleOffset] _BumpSpecMap ("Normal Map (GA) Specular (B)", 2D) = "bump" {}
		[NoScaleOffset] _TranslucencyMap ("AO (G) Translucency (B) Smoothness (A)", 2D) = "white" {}
		_OcclusionStrength ("Occlusion Strength", Range(0, 1)) = 1
		[Header(Shadows)] [Toggle(GEOM_TYPE_MESH)] _EnableShadowMap ("Enable extra Shadow Map", Float) = 0
		[NoScaleOffset] _ShadowMap ("Shadow Map Alpha (R)", 2D) = "white" {}
		[Enum(UnityEngine.Rendering.CullMode)] _ShadowCulling ("Shadow Caster Culling", Float) = 0
		[Header(Lighting)] [Space(3)] _NormalLerp ("Backface Normal Accuracy", Range(0, 1)) = 1
		_BackFaceSmoothness ("    Backface Smoothness", Range(0, 1)) = 0.5
		[Space(3)] _TranslucencyStrength ("Translucency Strength", Range(0, 1)) = 0.5
		_ViewDependency ("View Dependency", Range(0, 1)) = 0.8
		_AmbientTranslucency ("Ambient Scattering", Range(0, 8)) = 1
		[Toggle(_PARALLAXMAP)] _EnableTransFade ("Fade out Translucency", Float) = 0
		[Header(Wind)] [Space(3)] _BaseWindMultipliers ("Wind Multipliers (XYZ)*", Vector) = (1,1,1,0)
		[Space(3)] _TumbleStrength ("Tumble Strength", Range(-1, 1)) = 0
		_TumbleFrequency ("    Tumble Frequency", Range(0, 4)) = 1
		_TimeOffset ("    Time Offset", Range(0, 2)) = 0.25
		[Space(3)] [Toggle(_EMISSION)] _EnableLeafTurbulence ("Enable Leaf Turbulence", Float) = 0
		_LeafTurbulence ("    Leaf Turbulence", Range(0, 4)) = 0.2
		_EdgeFlutterInfluence ("    Edge Flutter Influence", Range(0, 1)) = 0.25
		[Space(3)] [Toggle(GEOM_TYPE_LEAF)] _EnableAdvancedEdgeBending ("Enable advanced Edge Flutter", Float) = 0
		[CTI_AdvancedEdgeFluttering] _AdvancedEdgeBending ("    Strenght (X) Frequency (Y) ", Vector) = (0.1,5,0,0)
		[Space(5)] [Toggle(_METALLICGLOSSMAP)] _LODTerrain ("Use Wind from Script*", Float) = 0
		[Space(5)] [Toggle(_NORMALMAP)] _AnimateNormal ("Enable Normal Rotation", Float) = 0
		[Space(10)] [Header(Options for lowest LOD)] [Space(3)] [Toggle] _FadeOutWind ("Fade out Wind", Float) = 0
		[HideInInspector] _IsBark ("Is Bark", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	//CustomEditor "CTI_ShaderGUI"
}