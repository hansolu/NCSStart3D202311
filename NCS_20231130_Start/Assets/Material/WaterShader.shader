Shader "Custom/WaterShader"
{
    Properties
    {
        _BumpMap("BumpMap", 2D) = "Bump"{} //노멀맵. 잔파도 용...
        _WaveSpeed("Wave Speed", float) = 0.05 
        _WavePower("Wave Power", float) = 0.2
        _WaveTilling("Wave Tilling", float) = 25

        _CubeMap("CubeMap", Cube) = ""{}  //스카이박스. 물에 비치는 배경 위함

        _SpacPow("Spacular Power", float) = 2 //반사하는 힘
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" } //불투명
            LOD 200

            GrabPass{} //그리려는 위치의 화면을 캡쳐해놓는 역할.

            CGPROGRAM
            #pragma surface surf WLight vertex:vert noambient noshadow 

            #pragma target 3.0

            sampler2D _BumpMap;
            float _WaveSpeed;
            float _WavePower;
            float _WaveTilling;

            samplerCUBE _CubeMap;

            sampler2D _GrabTexture; //GrabPass{} 에서 캡쳐한 텍스쳐는 이것으로 들어가게됨

            float _SpacPow;

            float dotData;

            struct Input
            {
                float2 uv_BumpMap;                
                float4 screenPos; //스크린 자체를 UV로 활용하기 위함. r =x , g =y,b=z,  a = z   == 알파값에 카메라와의 거리가 담김
                float3 viewDir; //카메라가 보는 방향

                float3 worldRefl; //이걸 사용하기 위해서는
                INTERNAL_DATA //얘가 필요함
            };

            void vert(inout appdata_full v) //texcoord 텍스쳐 좌표. == uv 
            {
                v.vertex.y = sin(abs(v.texcoord.x * 2 - 1) * _WaveTilling + _Time.y) * _WavePower;
            }

            void surf(Input IN, inout SurfaceOutput o)
            {
                float4 nor1 = tex2D(_BumpMap, IN.uv_BumpMap + float2(_Time.y * _WaveSpeed, 0));
                float4 nor2 = tex2D(_BumpMap, IN.uv_BumpMap - float2(_Time.y * _WaveSpeed, 0));
                o.Normal = UnpackNormal((nor1 + nor2) * 0.5);

                float4 sky = texCUBE(_CubeMap, WorldReflectionVector(IN, o.Normal));  //배경 반사 위한
                float4 refrection = tex2D(_GrabTexture, (IN.screenPos / IN.screenPos.a).xy + o.Normal.xy * 0.03); //배경 반사적용

                dotData = pow(saturate(1 - dot(o.Normal, IN.viewDir)), 0.6);
                float3 water = lerp(refrection, sky, dotData).rgb;

                o.Albedo = water;
            }


            //스페큘러 구현. 물의 반사는, 물 밖에서 내리쬐는 빛을 반사한 것.. =물 아래쪽이 비춰지는 부분에서는 스페큘러가 나오지 않도록.
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
