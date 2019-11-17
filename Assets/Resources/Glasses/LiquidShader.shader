Shader "RAA/LiquidShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        
        _Tint ("Tint", Color) = (1,1,1,1)
        _FillAmount ("Fill Amount", Range(-10,10)) = 0.0
        [HideInInspector] _WobbleX ("WobbleX", Range(-1,1)) = 0.0
        [HideInInspector] _WobbleZ ("WobbleZ", Range(-1,1)) = 0.0
        _TopColor ("Top Color", Color) = (1,1,1,1)
        _FoamColor ("Foam Line Color", Color) = (1,1,1,1)
        _Rim ("Foam Line Width", Range(0,0.1)) = 0.0    
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Range(0,10)) = 0.0
        
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
            
            Zwrite On
            Cull Off // we want the front and back faces
            AlphaToMask On // transparency
            
            
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
            float _FillAmount, _WobbleX, _WobbleZ;
            float4 _TopColor, _RimColor, _FoamColor, _Tint;
            float _Rim, _RimPower;
            
            
            float4 RotateAroundYInDegrees(float4 vertex, float degrees)
            {
                float alpha = degrees * 3.1416 / 180;
                float sina, cosa;
                sincos(alpha, sina, cosa);
                float2x2 m = float2x2(cosa, sina, -sina, cosa);
                return float4(vertex.yz , mul(m, vertex.xz)).xzyw ;            
            }
            
            
            
            

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
                
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                // rotate it around XY
                float3 worldPosX = RotateAroundYInDegrees(float4(worldPos,0),360);
                // rotate around XZ
                float3 worldPosZ = float3 (worldPosX.y, worldPosX.z, worldPosX.x);     
                // combine rotations with worldPos, based on sine wave from script
                float3 worldPosAdjusted = worldPos + (worldPosX  * _WobbleX)+ (worldPosZ* _WobbleZ);
                // how high up the liquid is
                o.fillEdge =  worldPosAdjusted.y + _FillAmount;
                
                return o;
            }


            float4 frag (v2f i, float facing : VFACE) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv) * _Tint;
               
                // rim light
                float dotProduct = 1 - pow(dot(i.normal, i.viewDir), _RimPower);
                float4 RimResult = smoothstep(0.5, 1.0, dotProduct);
                RimResult *= _RimColor;
               
                // foam edge
                float4 foam = ( step(i.fillEdge, 0.5) - step(i.fillEdge, (0.5 - _Rim)))  ;
                float4 foamColored = foam * (_FoamColor * 0.9);
                // rest of the liquid
                float4 result = step(i.fillEdge, 0.5) - foam;
                float4 resultColored = result * col;
                // both together, with the texture
                float4 finalResult = resultColored + foamColored;               
                finalResult.rgb += RimResult;
     
                // color of backfaces/ top
                float4 topColor = _TopColor * (foam + result);
                //VFACE returns positive for front facing, negative for backfacing
                return facing > 0 ? finalResult: topColor;
            }
            ENDHLSL
        }
    }
}
