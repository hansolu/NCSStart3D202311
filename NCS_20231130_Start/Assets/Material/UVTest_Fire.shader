Shader "Custom/UVTest_Fire"
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
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        
        CGPROGRAM        
        #pragma surface surf Standard alpha:blend //���� �׸��ڰ� ���� �̻��ϴϱ�.

        sampler2D _MainTex;
        sampler2D _MainTex2;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
        };

        float _FloatTest_X;
        float _FloatTest_Y;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

            fixed4 noise = tex2D(_MainTex2, float2(IN.uv_MainTex2.x, IN.uv_MainTex2.y - _Time.y * _FloatTest_X)); //������ �׸��� ���� �ҷ�����            
            fixed4 mainTexture = tex2D(_MainTex, IN.uv_MainTex + noise.r * _FloatTest_Y); //���� �� �����ؽ��Ŀ� �ش� ����� ������.

            o.Emission = mainTexture.rgb /** noise.rgb*/; //�ؼ� ��ü�� �� �����ؽ��� ���� ����
            o.Alpha = mainTexture.a * noise.a;         
        }
        ENDCG
    }
    FallBack "Diffuse"
}
