Shader "Custom/UVTest_Water"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}  //���� �ؽ���
        _MainTex2("Albedo 2", 2D) = "white" {} //�ұ�Ģ���� �������� ���� �ؽ��� �ϳ� �� �߰�.        
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
            fixed4 noise = tex2D(_MainTex2, IN.uv_MainTex2 - _Time.y * _FloatTest_X); //������ �׸��� ���� �ҷ�����            
            fixed4 mainTexture = tex2D(_MainTex, IN.uv_MainTex + noise.r * _FloatTest_Y); //���� �� �����ؽ��Ŀ� �ش� ����� ������.

            o.Emission = mainTexture.rgb; //�ؼ� ��ü�� �� �����ؽ��� ���� ����

            //Albedo�� ������ ����.
            //Emission�� �����ҽ��� Ȱ��ɼ��ִ� ���+�������� ��¦ �����°�ó�� ǥ���� �� ����.
        }
        ENDCG
    }
    FallBack "Diffuse"
}
