Shader "Hidden/Shader/CinematicBarsEffect_RLPRO"
{
    HLSLINCLUDE

        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/Common.hlsl"
    SAMPLER(_InputTexture);
	half _Stripes;
	half _Fade;

    float4 CustomPostProcess(Varyings input) : SV_Target
    {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

        half2 positionSS = input.uv;
        float4 outColor = float4(0,0,0,1);
		float4 outColor2 = tex2D(_InputTexture, positionSS);		
		outColor = lerp(outColor, outColor2, (1 - ceil(saturate(abs(input.uv.y - 0.5) - _Stripes))));
		return lerp(outColor2, outColor, _Fade);        
    }

    ENDHLSL

    SubShader
    {
        Pass
        {
            Name "#CinematicBars#"
			Cull Off ZWrite Off ZTest Always

            HLSLPROGRAM
                #pragma fragment CustomPostProcess
                #pragma vertex Vert
            ENDHLSL
        }
    }
    Fallback Off
}