Shader "Custom/CustomIlluminated"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _LightColor ("Light Color", Color) = (1,1,1,1)
        _LightPositionWorld ("Light Position", Vector) = (0,0,0,1)
        _LightIntensity ("Light Intensity", Float) = 50.0
        _Color ("Base Color", Color) = (1,1,1,1) 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

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
            float4 _LightColor;
            float3 _LightPositionWorld;
            float _LightIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Texture color
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Light calculations
                float3 lightDir = normalize(_LightPositionWorld - i.worldPos);
                float diff = max(dot(i.normal, lightDir), 0.0);
                float distance = length(_LightPositionWorld - i.worldPos);
                float attenuation = _LightIntensity / (1.0 + 0.1 * distance * distance);
                float3 ambient = float3(0.1, 0.1, 0.1);

                // Specular component (optional, adjust as needed)
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 reflectDir = reflect(-lightDir, i.normal);
                float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);

                // Combining color with light effect
                float3 lighting = ambient + _LightColor.rgb * (diff + spec) * attenuation;
                fixed4 col = texColor;
                col.rgb *= lighting;
                return col;
            }
            ENDCG
        }
    }
}
