Shader "RAA/GlassShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tint ("Tint", Color) = (1,1,1,1)
        
        // Blending state
        [HideInInspector] _Surface("__surface", Float) = 0.0
        [HideInInspector] _Blend("__blend", Float) = 0.0
        [HideInInspector] _AlphaClip("__clip", Float) = 0.0
        [HideInInspector] _SrcBlend("__src", Float) = 1.0
        [HideInInspector] _DstBlend("__dst", Float) = 0.0
        [HideInInspector] _ZWrite("__zw", Float) = 1.0
        [HideInInspector] _Cull("__cull", Float) = 2.0
    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent" 
            "RenderPipeline" = "LightweightPipeline"
            "IgnoreProjector" = "True"
            //"Queue" = "AlphaTest"
        }
        LOD 300

        Pass
        {
            Name "Forward"
            Tags
            {
                "RenderType"="Transparent" 
                "LightMode" = "LightweightForward"
            }
            
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite[_ZWrite]
            Cull[_Cull]
            
            
            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            //#pragma exclude_renderers d3d11_9x
            #pragma target 2.0
            
            // -------------------------------------
            // Lightweight Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            
            
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fwdbase
            
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                float3 normal : NORMAL;    
                float fillEdge : TEXCOORD2;
            };
            
            
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Tint;

            v2f vert (appdata v)
            {
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
                VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(v.normal);
                 
                half3 viewDirWS = GetCameraPositionWS() - vertexInput.positionWS;
                viewDirWS = SafeNormalize(viewDirWS);
                
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertex = vertexInput.positionCS;
				o.normal = vertexNormalInput.normalWS;
                o.viewDir = viewDirWS;
                return o;
            }


            float4 frag (v2f i) : SV_Target
            {
                 return tex2D(_MainTex, i.uv) * _Tint;
            }
            ENDHLSL
        }
    }
}
