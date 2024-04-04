Shader "Custom/AdvancedTextureMapping"
{
    Properties
    {
        _MainTex ("Texture 1", 2D) = "white" {}
        _SecondTex ("Texture 2", 2D) = "white" {}
        _BlendFactor ("Blend Factor", Range(0,1)) = 0.5
        _LightColor ("Light Color", Color) = (1,1,1,1)
        _LightPosition ("Light Position", Vector) = (0,0,0)
        _LightIntensity ("Light Intensity", Float) = 1.0
        _Color ("Color", Color) = (1,1,1,1) 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _SecondTex;
            float4 _MainTex_ST;
            float4 _SecondTex_ST;
            float _BlendFactor;
            fixed4 _LightColor;
            float3 _LightPosition;
            float _LightIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Texture blending
                float blend = sin(i.worldPos.x * _BlendFactor) * sin(i.worldPos.z * _BlendFactor);
                fixed4 tex1 = tex2D(_MainTex, i.uv);
                fixed4 tex2 = tex2D(_SecondTex, i.uv);
                fixed4 color = lerp(tex1, tex2, blend);

                // Advanced lighting calculations
                float3 normalizedNormal = normalize(i.normal);
                float3 lightDir = normalize(_LightPosition - i.worldPos);
                float distance = length(_LightPosition - i.worldPos);
                float attenuation = _LightIntensity / (1.0 + 0.1 * distance * distance);
                float diff = max(dot(normalizedNormal, lightDir), 0.0);
                float3 ambient = float3(0.1, 0.1, 0.1);
                float3 lighting = ambient + _LightColor.rgb * (diff * attenuation);

                // Apply lighting to the color
                color.rgb *= lighting;

                return color;
            }
            ENDCG
        }
    }
}
