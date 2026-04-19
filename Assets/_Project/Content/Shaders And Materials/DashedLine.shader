Shader "Line/Dashed"
{
    Properties
    {
        _Color ("Line Color", Color) = (1, 1, 1, 1)
        _DashCount ("Dash Count", Float) = 10
        _DashRatio ("Dash Ratio (0 = gap, 1 = solid)", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv     : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float  _DashCount;
            float  _DashRatio;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // i.uv.x идёт от 0 до 1 вдоль всей линии
                float x = i.uv.x;
                float cycle = x * _DashCount;       // сколько циклов уместилось
                float phase = frac(cycle);          // позиция внутри цикла

                // Если текущая позиция попадает в промежуток – отбрасываем пиксель
                if (phase > _DashRatio)
                    discard;

                return _Color;
            }
            ENDCG
        }
    }
}