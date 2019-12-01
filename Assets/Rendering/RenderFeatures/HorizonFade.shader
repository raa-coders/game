Shader "LWRP/Depth"
{
	Properties 
	{
	    [HideInInspector]_MainTex ("Base (RGB)", 2D) = "white" {}
	    _Color ("Horizon color", Color) = (1,1,1,1)
	    
	    _Step ("Step", float) = 10
		_PosterizationCount ("Count", int) = 8
		
		_Exponent ("Exponent", Range(1.0, 4.0)) = 2
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass
		{
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/SurfaceInput.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            
            #pragma shader_feature RAW_OUTLINE
            #pragma shader_feature POSTERIZE
            
            TEXTURE2D(_CameraDepthTexture);
            SAMPLER(sampler_CameraDepthTexture);
            
#ifndef RAW_OUTLINE
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
#endif
            
            struct Attributes
            {
                float4 positionOS       : POSITION;
                float2 uv               : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv        : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            
            float SampleDepth(float2 uv)
            {
#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
                return SAMPLE_TEXTURE2D_ARRAY(_CameraDepthTexture, sampler_CameraDepthTexture, uv, unity_StereoEyeIndex).r;
#else
                return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv);
#endif
            }
            
            
            
            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                output.vertex = vertexInput.positionCS;
                output.uv = input.uv;
                
                return output;
            }
            
            
            
            float4 _Color;
            float _Step;
            int _PosterizationCount;
            
            float _Exponent;
            
            half4 frag (Varyings input) : SV_Target 
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                
                
                // if SampleDepth is close to one, it's near the camera
                
                float actualDepth = SampleDepth(input.uv);
                
                if (actualDepth < 0)
                    return half4(1, 0, 1, 1);
                    
                if (actualDepth == 0)
                    return col;
                             
                float depth = min(actualDepth * _Step, 1);
                float percent = 1 - depth;
                
                percent = pow(percent, _Exponent);
                percent = round(percent * _PosterizationCount) / _PosterizationCount;

                return (1 - percent) * col + percent * _Color; 
            }
            
			#pragma vertex vert
			#pragma fragment frag
			
			ENDHLSL
		}
	} 
	FallBack "Diffuse"
}
