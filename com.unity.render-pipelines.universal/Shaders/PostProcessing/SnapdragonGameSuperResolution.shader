Shader "Hidden/Universal Render Pipeline/Snapdragon Game Super Resolution"
{
    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/Common.hlsl"


        #if SHADER_TARGET < 5
        #error Snapdragon Game Super Resolution requires a shader target of 5 or greater
        #endif

        float4 _SourceSize;

        #define SGSR_INPUT_TEXTURE _BlitTexture
        #define SGSR_INPUT_SAMPLER sampler_LinearClamp

        //#include "Packages/com.unity.render-pipelines.core/Runtime/PostProcessing/Shaders/FSRCommon.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/SGSR/sgsr1_shader_mobile.hlsl"

        half4 FragSGSR(Varyings input) : SV_Target
        {
            UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

            float2 uv = UnityStereoTransformScreenSpaceTex(input.texcoord);
            //uint2 integerUv = uv * _ScreenParams.xy;

            return SnapdragonGameSuperResolution(uv);
        }

    ENDHLSL

    /// Shader that performs the EASU (upscaling) component of the two part FidelityFX Super Resolution technique
    /// The second part of the technique (RCAS) is handled in the FinalPost shader
    /// Note: This shader requires shader target 4.5 because it relies on texture gather instructions
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZTest Always ZWrite Off Cull Off

        Pass
        {
            Name "SGSR"

            HLSLPROGRAM
                //#pragma vertex VertSGSR
                #pragma vertex Vert
                #pragma fragment FragSGSR
                #pragma target 5.0
            ENDHLSL
        }
    }

    Fallback Off
}
