Shader "Custom/ShowDistMat"
{
    Properties
    {
        _Distance("Distance", float) = 0 //보여줄 거리...수치..
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 200

        CGPROGRAM        
        #pragma surface surf Standard fullforwardshadows alpha:fade        
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        float _Distance;
                
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) ;
            
            float d = distance(IN.worldPos, _WorldSpaceCameraPos) / _Distance;
            d = 1 - saturate(d);
            
            o.Albedo = c.rgb;          
            //o.Emission = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = d;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
