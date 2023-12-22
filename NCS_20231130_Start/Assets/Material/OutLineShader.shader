Shader "Custom/OutLineShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Width ("LineWidth", float ) = 0.1
        _MainTex ("Albedo (RGB)", 2D) = "white" {}        
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        
        cull front //뒷면만 그리기
        zwrite off

        CGPROGRAM        
        #pragma surface surf NoLight vertex:vert noshadow noambient
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;    
        float _Width;

        void vert(inout appdata_full v)
        {
            v.vertex.xyz += v.normal.xyz * _Width;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {                        
        }

        float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float att)
        {
            return float4(_Color.rgb, 1);
        }

        ENDCG

            cull back //앞면만 그리기
            zwrite on

            CGPROGRAM
#pragma surface surf Standard fullforwardshadows
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
            o.Metallic = 1;
            o.Smoothness = 1;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
