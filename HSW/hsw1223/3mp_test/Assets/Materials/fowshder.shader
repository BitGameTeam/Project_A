Shader "Custom/fowshder"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)
        _MainTex ("cam2 (RGB) Trans (A)", 2D) = "white" {}
	 _MainTex2("cam (RGB) Trans (A)", 2D) = "white" {}
    }
    SubShader
    {
        /*Tags { "RenderType"="Opaque" }
        LOD 200*/

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert alpha:blend


        sampler2D _MainTex;
	    sampler2D _MainTex2;
		fixed4 _Color;
        struct Input
        {
            float2 uv_MainTex;
        };

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 colorcam2 = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 colorcam = tex2D(_MainTex2, IN.uv_MainTex);

			o.Albedo = _Color;
			float alpha = 1.0f - colorcam2.g - colorcam.g / 4;
			o.Alpha = alpha;
		}
        ENDCG
    }
}
