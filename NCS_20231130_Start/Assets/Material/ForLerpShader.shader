Shader "Custom/ForLerpShader"
{
    Properties
    {        
        _MainTex ("MainTexture", 2D) = "white" {}     
        _MainTex2 ("MainTexture2", 2D) = "white" {}
        _BlendFloat ("BlendFloat", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }        

        CGPROGRAM        
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex; //첫번째 그림
        sampler2D _MainTex2; //두번째 그림

        struct Input
        {
            float2 uv_MainTex; //첫번째 그림의 UV
            float2 uv_MainTex2; //두번째 그림의 UV
        };

        float _BlendFloat;

        UNITY_INSTANCING_BUFFER_START(Props)

        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 c2 = tex2D(_MainTex2, IN.uv_MainTex2);
            //o.Albedo = lerp(c, c2, _BlendFloat);
             //o.Emission 
            o.Albedo = lerp(c, c2, c2.a * _BlendFloat);  //albedo에 해도되고 Emission에 해도 됨.
            //o.Alpha = ;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
