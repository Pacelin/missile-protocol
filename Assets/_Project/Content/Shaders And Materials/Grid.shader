Shader "Sprites/Grid"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture (Mask)", 2D) = "white" {}
        _TilingU ("Tiling U (cells per width)", Float) = 10
        _TilingV ("Tiling V (cells per height)", Float) = 10
        _LineThickness ("Line Thickness (fraction of cell)", Range(0, 0.2)) = 0.02
        _LineColor ("Line Color", Color) = (1,1,1,1)
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

            sampler2D _MainTex;
            float _TilingU;
            float _TilingV;
            float _LineThickness;
            fixed4 _LineColor;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color; // сохраняем цвет спрайта для возможного tint, но не используем
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // Проверяем прозрачность спрайта-маски
                fixed4 mask = tex2D(_MainTex, IN.texcoord);
                if (mask.a < 0.05) discard; // не рисуем сетку в прозрачных областях

                // Вычисляем сетку в UV пространстве (от 0 до 1)
                float2 gridUV = IN.texcoord * float2(_TilingU, _TilingV);
                float2 fracPart = frac(gridUV);
                float2 distToEdge = min(fracPart, 1.0 - fracPart);
                float lineU = step(distToEdge.x, _LineThickness);
                float lineV = step(distToEdge.y, _LineThickness);
                float lineFactor = max(lineU, lineV);
                
                // Если пиксель на линии — рисуем цвет линии, иначе полностью прозрачный
                fixed4 finalColor = _LineColor;
                finalColor.a *= lineFactor;
                return finalColor;
            }
            ENDCG
        }
    }
}