Shader "Custom/FractalLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1,0,0,1)
        _Color2 ("Color 2", Color) = (0,1,0,1)
        _Color3 ("Color 3", Color) = (0,0,1,1)
        _Color4 ("Color 4", Color) = (1,1,0,1)
        _Color5 ("Color 5", Color) = (0,1,1,1)
        _LightColor ("Light Color", Color) = (1,1,1,1)
        _LightPosition ("Light Position", Vector) = (0,0,0)
        _LightIntensity ("Light Intensity", Float) = 50.0
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
            float4 _MainTex_ST;
            fixed4 _Color1;
            fixed4 _Color2;
            fixed4 _Color3;
            fixed4 _Color4;
            fixed4 _Color5;
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
                // Blend colors based on texture
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 blendedColor = texColor.r * _Color1 + texColor.g * _Color2 + texColor.b * _Color3;
                blendedColor += texColor.a * _Color4;
                float total = texColor.r + texColor.g + texColor.b + texColor.a;
                blendedColor += (1 - total) * _Color5;

                // Lighting calculations
                float3 normalizedNormal = normalize(i.normal);
                float3 lightDir = normalize(_LightPosition - i.worldPos);
                float diff = max(dot(normalizedNormal, lightDir), 0.0);
                float distance = length(_LightPosition - i.worldPos);
                float attenuation = _LightIntensity / (1.0 + 0.1 * distance * distance);
                float3 ambient = float3(0.1, 0.1, 0.1); 
                float3 specular = float3(0.0, 0.0, 0.0); // Specular can be adjusted or calculated as needed
                float3 lighting = ambient + _LightColor.rgb * diff * attenuation + specular;

                // Combine blended color with lighting
                fixed4 finalColor = fixed4(blendedColor.rgb * lighting, blendedColor.a);

                return finalColor;
            }
            ENDCG
        }
    }
}
