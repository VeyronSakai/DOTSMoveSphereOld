Shader "Unlit/SphereShader"
{
    Properties
    {

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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 CheckBoard(float2 UV, float4 ColorA, float4 ColorB, int Frequency_X, int Frequency_Y)
            {
                float index_x = floor(UV.x * Frequency_X);
                float index_y = floor(UV.y * Frequency_Y);
                int black_or_white = index_x % 2 + index_y % 2;
                if(black_or_white == 2)
                {
                    black_or_white = 0;
                }
                return lerp(ColorA, ColorB, black_or_white);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 colA = float4(1, 1, 1, 1);
                float4 colB = float4(0, 0, 0, 1);
                return CheckBoard(i.uv, colA, colB, 10, 10);
            }
            ENDCG
        }
    }
}
