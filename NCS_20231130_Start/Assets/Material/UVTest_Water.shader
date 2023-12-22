Shader "Custom/UVTest_Water"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}  //메인 텍스쳐
        _MainTex2("Albedo 2", 2D) = "white" {} //불규칙적인 움직임을 위한 텍스쳐 하나 더 추가.        
        _FloatTest_X (/*"Multiply Texture X"*/"TimeCtrl", float) = 1
        _FloatTest_Y (/*"Multiply Texture Y"*/"UV FlowCtrl", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"} 
        
        CGPROGRAM        
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _MainTex2;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
        };

        float
            _FloatTest_X;
        float _FloatTest_Y;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {            
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex * _FloatTest);            
            //fixed4 c = tex2D(_MainTex, float2( IN.uv_MainTex.x * _FloatTest_X, IN.uv_MainTex.y * _FloatTest_Y));
            //fixed4 c = tex2D(_MainTex, float2(IN.uv_MainTex.x, IN.uv_MainTex.y + _Time.y));
            fixed4 noise = tex2D(_MainTex2, IN.uv_MainTex2 - _Time.y * _FloatTest_X); //노이즈 그림을 먼저 불러오고            
            fixed4 mainTexture = tex2D(_MainTex, IN.uv_MainTex + noise.r * _FloatTest_Y); //실제 내 메인텍스쳐에 해당 노이즈를 적용함.

            o.Emission = mainTexture.rgb; //해서 객체에 내 메인텍스쳐 색을 입힘

            //Albedo는 고유의 색상.
            //Emission은 광원소스로 활용될수있는 흰색+밝은색은 살짝 빛나는것처럼 표현될 수 있음.
        }
        ENDCG
    }
    FallBack "Diffuse"
}
