Shader "Custom/WaterShader"
{
    Properties
    {
        _BumpMap("BumpMap", 2D) = "Bump"{} //��ָ�. ���ĵ� ��...
        _WaveSpeed("Wave Speed", float) = 0.05 
        _WavePower("Wave Power", float) = 0.2
        _WaveTilling("Wave Tilling", float) = 25

        _CubeMap("CubeMap", Cube) = ""{}  //��ī�̹ڽ�. ���� ��ġ�� ��� ����

        _SpacPow("Spacular Power", float) = 2 //�ݻ��ϴ� ��
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" } //������
            LOD 200

            GrabPass{} //�׸����� ��ġ�� ȭ���� ĸ���س��� ����.

            CGPROGRAM
            #pragma surface surf WLight vertex:vert noambient noshadow 

            #pragma target 3.0

            sampler2D _BumpMap;
            float _WaveSpeed;
            float _WavePower;
            float _WaveTilling;

            samplerCUBE _CubeMap;

            sampler2D _GrabTexture; //GrabPass{} ���� ĸ���� �ؽ��Ĵ� �̰����� ���Ե�

            float _SpacPow;

            float dotData;

            struct Input
            {
                float2 uv_BumpMap;                
                float4 screenPos; //��ũ�� ��ü�� UV�� Ȱ���ϱ� ����. r =x , g =y,b=z,  a = z   == ���İ��� ī�޶���� �Ÿ��� ���
                float3 viewDir; //ī�޶� ���� ����

                float3 worldRefl; //�̰� ����ϱ� ���ؼ���
                INTERNAL_DATA //�갡 �ʿ���
            };

            void vert(inout appdata_full v) //texcoord �ؽ��� ��ǥ. == uv 
            {
                v.vertex.y = sin(abs(v.texcoord.x * 2 - 1) * _WaveTilling + _Time.y) * _WavePower;
            }

            void surf(Input IN, inout SurfaceOutput o)
            {
                float4 nor1 = tex2D(_BumpMap, IN.uv_BumpMap + float2(_Time.y * _WaveSpeed, 0));
                float4 nor2 = tex2D(_BumpMap, IN.uv_BumpMap - float2(_Time.y * _WaveSpeed, 0));
                o.Normal = UnpackNormal((nor1 + nor2) * 0.5);

                float4 sky = texCUBE(_CubeMap, WorldReflectionVector(IN, o.Normal));  //��� �ݻ� ����
                float4 refrection = tex2D(_GrabTexture, (IN.screenPos / IN.screenPos.a).xy + o.Normal.xy * 0.03); //��� �ݻ�����

                dotData = pow(saturate(1 - dot(o.Normal, IN.viewDir)), 0.6);
                float3 water = lerp(refrection, sky, dotData).rgb;

                o.Albedo = water;
            }


            //����ŧ�� ����. ���� �ݻ��, �� �ۿ��� �����ش� ���� �ݻ��� ��.. =�� �Ʒ����� �������� �κп����� ����ŧ���� ������ �ʵ���.
            float4 LightingWLight(SurfaceOutput s, float3 lightDIr, float3 viewDir, float atten)
            {
                float3 refVec = s.Normal * dot(s.Normal, viewDir) * 2 - viewDir; 
                refVec = normalize(refVec);

                float spcr = lerp(0, pow(saturate(dot(refVec, lightDIr)),256), dotData) * _SpacPow;

                return float4(s.Albedo + spcr.rrr,1);
            }
            ENDCG
        }
            FallBack "Diffuse"
}
