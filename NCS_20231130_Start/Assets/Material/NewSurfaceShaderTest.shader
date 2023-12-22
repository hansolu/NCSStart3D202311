Shader "Custom/NewSurfaceShaderTest"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        //변수하나 추가해보기
        //_스크립트에서 부를 이름 추가 ( "인스펙터창에서 보일 이름", int) = 0
        _TestFloat("floatTest", float) = 0 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

            //#pragma === 설정. 스니핏이라고도 불립니다. 
            // 셰이더의 조명계산 설정, 기타 세부적인 분기를 정해주고 == 전처리 작업을 해줍니다.
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex; //텍스쳐 자체. 옷에대한 얘기라고하면

        struct Input
        {
            float2 uv_MainTex; //그 옷을 어디다가 입히냐 에 대한 얘기.
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _TestFloat;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o) //inout == 뒤에 매개변수로 들어온 친구의 정보를 읽고쓰기가 가능함.
        {
            //// Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //o.Albedo = c.rgb;
            //o.Albedo = float3(1,0,0) + float3(0,1,0); //더하면 밝아지고
            //o.Albedo = float3(1,0,0) * float3(0,1,0); //곱하면 어두워짐..
            //o.Albedo = float3(0, _TestFloat, 0) + float3(0, _TestFloat, 0);
            o.Albedo = float3(1, 1, 1) - float3(0, _TestFloat, 0); //빼기도 가능.
            //o.Emission = half3(1,0,0);
            //// Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
