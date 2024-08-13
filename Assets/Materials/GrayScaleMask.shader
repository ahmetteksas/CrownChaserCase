Shader "Unlit/GrayScaleMask"
{
    Properties
    {
        _MainTex ("Sprite", 2D) = "white" {}
        _MaskTex ("Mask", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] _ToggleMove("ToggleMove", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Cull Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _MaskTex;
            bool _ToggleMove;
            float4 _MainTex_ST;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // sample the texture

                float2 offset =  _ToggleMove * float2(((_SinTime.y + 1) / 2) , 0);

                fixed4 col = tex2D(_MainTex, i.uv) * tex2D(_MaskTex, i.uv + offset);
                return col;
            }
            ENDCG
        }
    }
}
