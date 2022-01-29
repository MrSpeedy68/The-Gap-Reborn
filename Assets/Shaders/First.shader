// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/First"
{
    Properties
    {
        // Color property for material inspector, default to white
        _PulseMultiplier ("Pulse Multiplier", float) = 0
    }
    
    SubShader
    {
        Pass
        {
            CGPROGRAM
            // use "vert" function as the vertex shader
            #pragma vertex vert
            // use "frag" function as the pixel (fragment) shader
            #pragma fragment frag

            #include "UnityCG.cginc"

            // vertex shader outputs ("vertex to fragment")
            struct v2f
            {
                // we'll output world space normal as one of regular ("texcoord") interpolators
                half3 worldNormal : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            float bnoise(float x)
            {
                // setup    
                float i = floor(x);
                float f = frac(x);
                float s = sign(frac(x/2.0)-0.5);
                
                float k = frac(i*.1731);

                // quartic polynomial
                return s*f*(f-1.0)*((16.0*k-4.0)*f*(f-1.0)-1.0);
            }

            float average(float3 v)
            {
                float a = v.r + v.g + v.b;
                return a * 0.33;
            }

            float _PulseMultiplier;
            
            // vertex shaders
            v2f vert (float4 vertex: POSITION, float3 normal : NORMAL)
            {
                v2f o;
                float normalizedTime = (_SinTime.a + 1) * 0.5;
                vertex.rgb += normal.rgb * (normalizedTime * _PulseMultiplier);
                o.pos = UnityObjectToClipPos(vertex);
                o.worldNormal = UnityObjectToWorldNormal(normal);
                return o;
            }
            
            // texture we will sample
            sampler2D _MainTex;

            fixed4 _Color;

            // pixel shader; returns low precision ("fixed4" type)
            // color ("SV_Target" semantic)
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = (0, 0, 0, 0);
                col.rgb = (i.worldNormal + 1) * 0.5;

                return col;
            }
            
            ENDCG
        }
    }
}