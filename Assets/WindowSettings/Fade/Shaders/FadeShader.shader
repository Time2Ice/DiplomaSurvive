Shader "Custom/FadeShader"
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
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

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
				float2 mask : TEXCOORD0;
			};

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
				o.mask = v.uv;
				return o;
			}
			
 
			float _MaskValue;
			float _MaskSpread;
			float4 _MaskColor;
			int _MaskStep;

			fixed4 frag (v2f i) : SV_Target
			{
			    fixed4 col = _MaskColor;
				i.mask.x = i.mask.x * _ScaleX + _OffsetX;
				i.mask.y = i.mask.y * _ScaleY + _OffsetY;
				
				float4 mask = tex2D(_MaskTex, i.mask);

				float alpha = mask.a * (1 - 1/255.0 * _MaskStep);

				float weight = smoothstep(_MaskValue - _MaskSpread, _MaskValue, alpha);

			#if !INVERT_MASK
				weight = 1 - weight;
			#endif
				col.a = col.a * weight;
				return col;
			}
			ENDCG
		}
	}
}
