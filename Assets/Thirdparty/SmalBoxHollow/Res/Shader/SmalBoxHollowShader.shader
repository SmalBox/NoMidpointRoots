Shader "ImageEffect/SmalBoxHollowShader"
{
    Properties
    {
        _OverlayColor("OverlayColor", color) = (0, 0, 0, 1)
        _MainTex ("MainTexture", 2D) = "white" {}
        _SubTex ("SubTexture", 2D) = "white" {}
        _SubTex2 ("SubTexture2", 2D) = "white" {}
        _Scale ("Scale", float) = 1
        _Pos ("Pos", Vector) = (1, 1, 0, 0)
    }
    SubShader
    {
        // No culling or depth
        Tags { "Queue"="Transparent" "RenderType"="TransparentCutout" }
        //Cull Off ZWrite Off ZTest Always

        Pass
        {
            //ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 _OverlayColor;
            sampler2D _MainTex;
            sampler2D _SubTex;
            sampler2D _SubTex2;
            float _Scale;
            fixed2 _Pos;

            fixed4 _result;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainCol = tex2D(_MainTex, i.uv);
                fixed4 subCol = tex2D(_SubTex, i.uv / _Scale - (_Pos / _Scale) - (-0.5));
                fixed4 subCol2 = tex2D(_SubTex2, i.uv);
                // just invert the colors
                if (subCol.r > 0.5)
                {
                    _result = fixed4(mainCol.rgb * _OverlayColor, //(1 - subCol.r));
                        (1 - subCol.r) > subCol2.a ? subCol2.a : (1 - subCol.r));
                    return _result;
                }
                _result =
                    fixed4(mainCol.rgb * _OverlayColor, //mainCol.a);
                            mainCol.a > subCol2.a ? subCol2.a : mainCol.a);
                return _result;
            }
            ENDCG
        }
    }
}
