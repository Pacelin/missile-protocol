Shader "Sprites/DissolveWithMask"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0
        _EdgeWidth ("Edge Width", Range(0,0.3)) = 0.05
        _EdgeColor ("Edge Color", Color) = (1,1,1,1)

        // Маска: белые области = эффект dissolve разрешён
        _MaskTex ("Mask Texture", 2D) = "white" {}
        _MaskOffsetX ("Mask Offset X (pixels)", Float) = 0
        _MaskOffsetY ("Mask Offset Y (pixels)", Float) = 0

        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            fixed4 _Color;
            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _DissolveAmount;
            float _EdgeWidth;
            fixed4 _EdgeColor;

            sampler2D _MaskTex;
            float _MaskOffsetX;
            float _MaskOffsetY;
            float4 _MaskTex_TexelSize; // x = 1/width, y = 1/height, z = width, w = height

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 main = tex2D(_MainTex, IN.texcoord) * IN.color;
                if (main.a < 0.05)
                    discard;

                // --- Проверка маски: разрешён ли dissolve в этой точке? ---
                bool dissolveAllowed = true; // по умолчанию разрешён

                // Вычисляем UV маски с учётом пиксельного сдвига
                float2 maskUV = IN.texcoord + float2(_MaskOffsetX * _MaskTex_TexelSize.x, _MaskOffsetY * _MaskTex_TexelSize.y);
                fixed4 maskColor = tex2D(_MaskTex, maskUV);
                // Если маска чёрная (или любое значение R < 0.5) — dissolve запрещён
                if (maskColor.r < 0.5)
                    dissolveAllowed = false;

                // Применяем dissolve только если разрешено маской
                if (dissolveAllowed)
                {
                    float noise = tex2D(_NoiseTex, IN.texcoord).r;
                    float threshold = _DissolveAmount;
                    float edge = threshold + _EdgeWidth;

                    if (noise < threshold)
                        discard;

                    if (noise < edge)
                    {
                        float t = (noise - threshold) / _EdgeWidth;
                        main.rgb = lerp(_EdgeColor.rgb, main.rgb, t);
                        main.a = lerp(1.0, main.a, t);
                    }
                }

                return main;
            }
            ENDCG
        }
    }

    Fallback "Sprites/Diffuse"
}