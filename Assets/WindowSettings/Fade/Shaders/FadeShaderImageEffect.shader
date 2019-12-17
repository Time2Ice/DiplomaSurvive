Shader "Custom/FadeShaderImageEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MaskTex ("Mask Texture", 2D) = "white" {}
		_MaskValue ("Mask Value", Range(0,1)) = 0.5
		_MaskSpread ("Mask Spread", Range(0,1)) = 0.5
		_MaskColor ("Mask Color", Color) = (0,0,0,1)
		_MaskStep ("Mask Step", Int) = 1
		_OffsetX ("OffsetX", Float) = 0
		_OffsetY ("OffsetY", Float) = 0
		_ScaleX ("OffsetX", Float) = 1
		_ScaleY ("OffsetY", Float) = 1
		[Toggle(INVERT_MASK)] _INVERT_MASK ("Mask Invert", Float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag		
			#include "UnityCG.cginc"

			#pragma shader_feature INVERT_MASK


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv     : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv     : TEXCOORD0;
				float2 mask : TEXCOORD0;
			};

			sampler2D _MainTex;
			uniform float4 _MainTex_ST; 
			sampler2D _MaskTex;
			uniform float4 _MaskTex_ST;
			float _OffsetX;
			float _OffsetY;
			float _ScaleX;
			float _ScaleY;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
				o.mask = v.uv;
				return o;
			}
			
 
			float _MaskValue;
			float _MaskSpread;
			float4 _MaskColor;
			int _MaskStep;

			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);
				
				i.mask.x = i.mask.x * _ScaleX + _OffsetX;
				i.mask.y = i.mask.y * _ScaleY + _OffsetY;
				
				float4 mask = tex2D(_MaskTex, i.mask);

				float alpha = mask.a * (1 - 1/255.0 * _MaskStep);

				float weight = smoothstep(_MaskValue - _MaskSpread, _MaskValue, alpha);

			#if INVERT_MASK
				weight = 1 - weight;
			#endif
				col.rgb = lerp(col.rgb, lerp(_MaskColor.rgb, col.rgb, weight), _MaskColor.a);

				return col;
			}
			ENDCG
		}
	}
}
