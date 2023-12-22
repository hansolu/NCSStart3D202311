Shader "Custom/ShowLightShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0

        _LightDirection("Light Direction", Vector) = (0,0,1,0)
            _LightPosition("Light Position", Vector) = (0,0,0,0)
            _LightAngle("Light Angle", Range(0,180)) = 45
            _LightPower("Brightness", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 200

        CGPROGRAM
        
        #pragma surface surf Standard fullforwardshadows alpha:fade                    
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos; //제공되는 변수. 월드 위치.
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float4 _LightDirection;
        float4 _LightPosition;
        float _LightAngle;
            float _LightPower;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 Dir = normalize(_LightPosition - IN.worldPos);
            float Scale = dot(Dir, _LightDirection);
            float Brightness = Scale - cos(_LightAngle * (3.14 / 360));
            Brightness = min(max(_LightPower * Brightness, 0), 1);           

            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Emission = c.rgb * c.a * Brightness;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a * Brightness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
