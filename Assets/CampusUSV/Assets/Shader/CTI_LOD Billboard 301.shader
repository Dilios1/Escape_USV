Shader "CTI/LOD Billboard 301" {
	Properties {
		[Space(5)] _HueVariation ("Color Variation (RGB) Strength (A)", Vector) = (0.9,0.5,0,0.1)
		[Space(5)] [NoScaleOffset] _MainTex ("Albedo (RGB) Alpha/Occlusion (A)", 2D) = "white" {}
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.3
		_AlphaLeak ("Alpha Leak Suppression", Range(0.5, 1)) = 0.6
		_OcclusionStrength ("Occlusion Strength", Range(0, 1)) = 1
		[Space(5)] [NoScaleOffset] _BumpTex ("Normal (AG) Translucency(R) Smoothness(B)", 2D) = "bump" {}
		_NormalScale ("Normal Scale", Float) = 1
		_SpecColor ("Specular", Vector) = (0.2,0.2,0.2,1)
		[Space(5)] _TranslucencyStrength ("Translucency Strength", Range(0, 1)) = 0.5
		_ViewDependency ("View Dependency", Range(0, 1)) = 0.8
		_AmbientTranslucency ("Ambient Scattering", Range(0, 8)) = 1
		[Toggle(_PARALLAXMAP)] _EnableTransFade ("Fade out Translucency", Float) = 0
		[Header(Wind)] [Space(3)] [Toggle(_EMISSION)] _UseWind ("Enable Wind", Float) = 0
		[Space(5)] _WindStrength ("Wind Strength", Float) = 1
		[HideInInspector] _TreeScale ("Tree Scale", Range(0, 50)) = 1
		[HideInInspector] _TreeWidth ("Tree Width Factor", Range(0, 1)) = 1
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
	Fallback "Transparent/Cutout/VertexLit"
}