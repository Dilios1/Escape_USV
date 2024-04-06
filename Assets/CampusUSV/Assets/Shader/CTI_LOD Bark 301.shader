Shader "CTI/LOD Bark 301" {
	Properties {
		_HueVariation ("Color Variation*", Vector) = (0.9,0.5,0,0.1)
		[Header(Main Maps)] [Space(5)] _MainTex ("Albedo (RGB) Smoothness (A)", 2D) = "white" {}
		[NoScaleOffset] _BumpSpecAOMap ("Normal Map (GA) Specular (R) AO (B)", 2D) = "bump" {}
		[Header(Secondary Maps)] [Space(5)] [CTI_DetailsEnum] _DetailMode ("Secondary Maps (need UV2)", Float) = 0
		[Toggle(_SWAP_UVS)] _SwapUVS ("    Swap UVS", Float) = 0
		_AverageCol ("    Average Color (RGB) Smoothness (A)", Vector) = (1,1,1,0.5)
		[Space(5)] [NoScaleOffset] _DetailAlbedoMap ("    Detail Albedo x2 (RGB) Smoothness (A)", 2D) = "gray" {}
		[NoScaleOffset] _DetailNormalMapX ("    Normal Map (GA) Specular (R) AO (B)", 2D) = "gray" {}
		_DetailNormalMapScale ("    Normal Strength", Float) = 1
		_OcclusionStrength ("Occlusion Strength", Range(0, 1)) = 1
		[Header(Wind)] [Space(3)] _BaseWindMultipliers ("Wind Multipliers (XYZ)*", Vector) = (1,1,1,0)
		[Space(5)] [Toggle(_METALLICGLOSSMAP)] _LODTerrain ("Use Wind from Script*", Float) = 0
		[Header(Options for lowest LOD)] [Space(5)] [Toggle] _FadeOutWind ("Fade out Wind", Float) = 0
		[HideInInspector] _Cutoff ("Cutoff", Range(0, 1)) = 1
		[HideInInspector] _IsBark ("Is Bark", Float) = 1
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