// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "leesue/MyWater"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_RefTexture("_RefTexture", 2D) = "white"{}
		_WaveDirectionX("wave direction x", Vector) = (1.0, 1.0, 0.0, 0.0)
		_WaveDirectionZ("wave direction z", Vector) = (1.0, 1.0, 0.0, 0.0)
		_Q("WaveSharp", float) = 1.0
		_Amp("A", Vector) = (1.0, 1.0, 1.0, 1.0)
		_Speed("Speed", Vector) = (1.0, 1.0, 1.0, 1.0)
		_Length("Length", Vector) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
#define _PI 3.14159265
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 ScreenPos:TEXCOORD1;
			};

			sampler2D _RefTexture;
			float4 _RefTexture_ST;
			fixed4 _Color;

			float _Q;
			float4 _WaveDirectionX;
			float4 _WaveDirectionZ;
			float4 _Amp;
			float4 _Speed;
			float4 _Length;

			float4 changeToWavePosition(float4 pos){
                float4 wSpeed = 2*_PI/_Length,
                    lSpeed = _Speed*2*_PI/_Length;
				float4 dirRes = wSpeed*_WaveDirectionX * pos.x + wSpeed*_WaveDirectionZ * pos.z + lSpeed*_Time.x;
				float y = dot(_Amp, sin(dirRes)),
					x = dot(_Q * _Amp*_WaveDirectionX, cos(dirRes)),
					z = dot(_Q * _Amp*_WaveDirectionZ, cos(dirRes));
				return float4(x, y, z, 0)+pos;
			}

			v2f vert (appdata v)
			{
				v2f o;
                /* o.uv = float2(0.0f, 0.0f); */
		        o.uv = TRANSFORM_TEX(v.uv, _RefTexture);
				o.ScreenPos = ComputeScreenPos(UnityObjectToClipPos(v.vertex));
				o.vertex = mul( unity_ObjectToWorld, v.vertex);
				//o.vertex.xyz = CalculateWavesDisplacement(o.vertex.xyz);
				o.vertex = changeToWavePosition(o.vertex);
				o.vertex = mul(unity_WorldToObject, o.vertex);
				o.vertex = UnityObjectToClipPos(o.vertex);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = (tex2D(_RefTexture, i.ScreenPos.xy/i.ScreenPos.w)*_Color);
				float2 refuv = i.uv;
				refuv = float2(refuv.y, 1 - refuv.x);
				//fixed4 col = (tex2D(_RefTexture, refuv)*_Color);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
