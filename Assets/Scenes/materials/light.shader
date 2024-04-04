Shader "Custom/MyLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightColor ("Light Color", Color) = (1,1,1,1)
        _LightPosition ("Light Position", Vector) = (0,0,0)
        _LightIntensity ("Light Intensity", Float) = 50.0
        _Color ("Base Color", Color) = (1,1,1,1)
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
            };

            struct v2f
            {
                float3 worldPos : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _LightColor;
            float3 _LightPosition;
            float _LightIntensity;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = mul(v.normal, (float3x3)UNITY_MATRIX_IT_MV);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Normal normalization
                float3 normalizedNormal = normalize(i.normal);

                // Light direction and distance
                float3 lightDir = normalize(_LightPosition - i.worldPos);
                float distance = length(_LightPosition - i.worldPos);

                // Attenuation based on distance
                float attenuation = 1.0 / (1.0 + 0.1 * distance * distance);

                // Diffuse light calculation
                float diff = max(dot(normalizedNormal, lightDir), 0.0);

                // Ambient component
                float3 ambient = float3(0.1, 0.1, 0.1); // Simple constant ambient factor

                // Specular component
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 reflectDir = reflect(-lightDir, normalizedNormal);
                float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32); // Shininess factor of 32

                // Combining all components
                float3 lighting = ambient + (diff + spec) * attenuation;
                fixed4 texColor = tex2D(_MainTex, i.vertex.xy);
                fixed4 baseColor = _Color;
                fixed4 finalColor = baseColor * texColor * fixed4(texColor.rgb * _LightColor.rgb * lighting, texColor.a);

                return finalColor;
            }
            ENDCG
        }
    }
}
 