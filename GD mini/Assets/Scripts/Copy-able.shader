Shader "Unlit/Copy-able"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Outline ("Outline", Range (0.0, 0.2)) = 0.1
        _OutlineColor ("Outline Color", Color) = (.7,.7,.7,1)
    }
    SubShader
    {
     //  Tags { "RenderType"="Opaque" }
        Pass
            {

            Blend SrcAlpha OneMinusSrcAlpha //Achieve transparency
            Zwrite Off //deactive z-buffer

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
          
            #include "UnityCG.cginc"
            
      

            struct meshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL; 
                float4 uv1 : TEXCOORD1;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };


            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Outline;
            float4 _OutlineColor;

            float4 make_outline(float4 vertexPos, float value){

                float4x4 thickness = float4x4
                (
                    1 + value, 0, 0, 0,
                    0, 1 + value, 0, 0,
                    0, 0, 1 + value, 0,
                    0, 0, 0, 1 + value
                );
                return mul(thickness, vertexPos);
            }

            Interpolators vert (meshData v)
            {
                Interpolators o;
                float4 vertexEnlarge = make_outline(v.vertex, _Outline);
                o.vertex = UnityObjectToClipPos(vertexEnlarge);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

          Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct meshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            Interpolators vert (meshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }

    }
}
