Shader "Line/DashedLocalX"
{
    Properties
    {
        _Color ("Line Color", Color) = (1,1,1,1)
        _DashSpacing ("Dash Spacing (world units)", Float) = 0.5
        _DashLength ("Dash Length (world units)", Float) = 0.3
        _Offset ("Phase Offset", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
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
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float xLocal : TEXCOORD0;
            };

            float4 _Color;
            float _DashSpacing;
            float _DashLength;
            float _Offset;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.xLocal = v.vertex.x;   // берём локальную X-координату
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Добавляем смещение, чтобы штрихи двигались вместе с волной
                float pos = i.xLocal + _Offset;
                float phase = fmod(pos, _DashSpacing);
                if (phase < 0) phase += _DashSpacing;

                if (phase > _DashLength)
                    discard;

                return _Color;
            }
            ENDCG
        }
    }
}