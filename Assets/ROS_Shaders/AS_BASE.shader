// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_LightmapInd', a built-in variable
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D
// Upgrade NOTE: replaced tex2D unity_LightmapInd with UNITY_SAMPLE_TEX2D_SAMPLER

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:3,uamb:True,mssp:True,lmpd:True,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:True,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:33875,y:32653,varname:node_1,prsc:2|diff-3-RGB,spec-320-OUT,gloss-3-A,normal-20-RGB,emission-425-OUT,amspl-738-OUT;n:type:ShaderForge.SFN_Tex2d,id:3,x:32805,y:32214,ptovrint:False,ptlb:DIFFUSE_Gloss_A,ptin:_DIFFUSE_Gloss_A,varname:node_1527,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9,x:32760,y:32594,ptovrint:False,ptlb:SPEC_G,ptin:_SPEC_G,varname:node_3818,prsc:2,ntxv:1,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:20,x:33288,y:32689,ptovrint:False,ptlb:NORMAL,ptin:_NORMAL,varname:node_8853,prsc:2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Multiply,id:320,x:33106,y:32532,varname:node_320,prsc:2|A-378-OUT,B-9-RGB;n:type:ShaderForge.SFN_Slider,id:378,x:32699,y:32485,ptovrint:False,ptlb:SPEC MULT,ptin:_SPECMULT,varname:node_5698,prsc:2,min:0,cur:0.7850373,max:2;n:type:ShaderForge.SFN_Tex2d,id:385,x:32921,y:32955,ptovrint:False,ptlb:EMISSIVE,ptin:_EMISSIVE,varname:node_3468,prsc:2,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Multiply,id:425,x:33433,y:32918,varname:node_425,prsc:2|A-429-OUT,B-385-RGB;n:type:ShaderForge.SFN_Slider,id:429,x:32881,y:32817,ptovrint:False,ptlb:EMISSIVE MULT,ptin:_EMISSIVEMULT,varname:node_1417,prsc:2,min:0,cur:200,max:200;n:type:ShaderForge.SFN_Cubemap,id:617,x:33472,y:33202,ptovrint:False,ptlb:CUBEMAP,ptin:_CUBEMAP,varname:node_6403,prsc:2|DIR-668-OUT,MIP-632-OUT;n:type:ShaderForge.SFN_Fresnel,id:632,x:33089,y:33194,varname:node_632,prsc:2|NRM-684-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:668,x:33226,y:33323,varname:node_668,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:684,x:32883,y:33267,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:738,x:33607,y:33012,varname:node_738,prsc:2|A-378-OUT,B-617-RGB,C-632-OUT;proporder:3-20-9-378-385-429-617;pass:END;sub:END;*/

Shader "Shader Forge/SF_BASE" {
    Properties {
        _DIFFUSE_Gloss_A ("DIFFUSE_Gloss_A", 2D) = "white" {}
        _NORMAL ("NORMAL", 2D) = "bump" {}
        _SPEC_G ("SPEC_G", 2D) = "gray" {}
        _SPECMULT ("SPEC MULT", Range(0, 2)) = 0.7850373
        _EMISSIVE ("EMISSIVE", 2D) = "black" {}
        _EMISSIVEMULT ("EMISSIVE MULT", Range(0, 200)) = 200
        _CUBEMAP ("CUBEMAP", Cube) = "_Skybox" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            #ifndef LIGHTMAP_OFF
                // float4 unity_LightmapST;
                // sampler2D unity_Lightmap;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform sampler2D _DIFFUSE_Gloss_A; uniform float4 _DIFFUSE_Gloss_A_ST;
            uniform sampler2D _SPEC_G; uniform float4 _SPEC_G_ST;
            uniform sampler2D _NORMAL; uniform float4 _NORMAL_ST;
            uniform float _SPECMULT;
            uniform sampler2D _EMISSIVE; uniform float4 _EMISSIVE_ST;
            uniform float _EMISSIVEMULT;
            uniform samplerCUBE _CUBEMAP;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                #ifndef LIGHTMAP_OFF
                    float2 uvLM : TEXCOORD7;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                #ifndef LIGHTMAP_OFF
                    o.uvLM = v.texcoord1 * unity_LightmapST.xy + unity_LightmapST.zw;
                #endif
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NORMAL_var = UnpackNormal(tex2D(_NORMAL,TRANSFORM_TEX(i.uv0, _NORMAL)));
                float3 normalLocal = _NORMAL_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                #ifndef LIGHTMAP_OFF
                    float4 lmtex = UNITY_SAMPLE_TEX2D(unity_Lightmap,i.uvLM);
                    #ifndef DIRLIGHTMAP_OFF
                        float3 lightmap = DecodeLightmap(lmtex);
                        float3 scalePerBasisVector = DecodeLightmap(UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd,unity_Lightmap,i.uvLM));
                        UNITY_DIRBASIS
                        half3 normalInRnmBasis = saturate (mul (unity_DirBasis, normalLocal));
                        lightmap *= dot (normalInRnmBasis, scalePerBasisVector);
                    #else
                        float3 lightmap = DecodeLightmap(lmtex);
                    #endif
                #endif
                #ifndef LIGHTMAP_OFF
                    #ifdef DIRLIGHTMAP_OFF
                        float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                    #else
                        float3 lightDirection = normalize (scalePerBasisVector.x * unity_DirBasis[0] + scalePerBasisVector.y * unity_DirBasis[1] + scalePerBasisVector.z * unity_DirBasis[2]);
                        lightDirection = mul(lightDirection,tangentTransform); // Tangent to world
                    #endif
                #else
                    float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                #endif
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _DIFFUSE_Gloss_A_var = tex2D(_DIFFUSE_Gloss_A,TRANSFORM_TEX(i.uv0, _DIFFUSE_Gloss_A));
                float gloss = _DIFFUSE_Gloss_A_var.a;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_632 = (1.0-max(0,dot(i.normalDir, viewDirection)));
                float4 _SPEC_G_var = tex2D(_SPEC_G,TRANSFORM_TEX(i.uv0, _SPEC_G));
                float3 specularColor = (_SPECMULT*_SPEC_G_var.rgb);
                float specularMonochrome = dot(specularColor,float3(0.3,0.59,0.11));
                float HdotL = max(0.0,dot(halfDirection,lightDirection));
                float3 fresnelTerm = specularColor + ( 1.0 - specularColor ) * pow((1.0 - HdotL),5);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float3 fresnelTermAmb = specularColor + ( 1.0 - specularColor ) * ( pow((1.0 - NdotV),5) / (4-3*gloss) );
                float alpha = 1.0 / ( sqrt( (Pi/4.0) * specPow + Pi/2.0 ) );
                float visTerm = ( NdotL * ( 1.0 - alpha ) + alpha ) * ( NdotV * ( 1.0 - alpha ) + alpha );
                visTerm = 1.0 / visTerm;
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                #if !defined(LIGHTMAP_OFF) && defined(DIRLIGHTMAP_OFF)
                    float3 directSpecular = float3(0,0,0);
                #else
                    float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*fresnelTerm*visTerm*normTerm;
                #endif
                float3 indirectSpecular = (0 + (_SPECMULT*texCUBElod(_CUBEMAP,float4(viewReflectDirection,node_632)).rgb*node_632)) * fresnelTermAmb;
                float3 specular = (directSpecular + indirectSpecular) * specularColor;
                #ifndef LIGHTMAP_OFF
                    #ifndef DIRLIGHTMAP_OFF
                        specular *= lightmap;
                    #else
                        specular *= (floor(attenuation) * _LightColor0.xyz);
                    #endif
                #else
                    specular *= (floor(attenuation) * _LightColor0.xyz);
                #endif
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                #ifndef LIGHTMAP_OFF
                    float3 directDiffuse = float3(0,0,0);
                #else
                    float3 directDiffuse = max( 0.0, NdotL)*InvPi * attenColor;
                #endif
                #ifndef LIGHTMAP_OFF
                    #ifdef SHADOWS_SCREEN
                        #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) && defined(SHADER_API_MOBILE)
                            directDiffuse += min(lightmap.rgb, attenuation);
                        #else
                            directDiffuse += max(min(lightmap.rgb,attenuation*lmtex.rgb), lightmap.rgb*attenuation*0.5);
                        #endif
                    #else
                        directDiffuse += lightmap.rgb;
                    #endif
                #endif
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb*2; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _DIFFUSE_Gloss_A_var.rgb;
                diffuse *= 1-specularMonochrome;
////// Emissive:
                float4 _EMISSIVE_var = tex2D(_EMISSIVE,TRANSFORM_TEX(i.uv0, _EMISSIVE));
                float3 emissive = (_EMISSIVEMULT*_EMISSIVE_var.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            uniform sampler2D _DIFFUSE_Gloss_A; uniform float4 _DIFFUSE_Gloss_A_ST;
            uniform sampler2D _SPEC_G; uniform float4 _SPEC_G_ST;
            uniform sampler2D _NORMAL; uniform float4 _NORMAL_ST;
            uniform float _SPECMULT;
            uniform sampler2D _EMISSIVE; uniform float4 _EMISSIVE_ST;
            uniform float _EMISSIVEMULT;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NORMAL_var = UnpackNormal(tex2D(_NORMAL,TRANSFORM_TEX(i.uv0, _NORMAL)));
                float3 normalLocal = _NORMAL_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _DIFFUSE_Gloss_A_var = tex2D(_DIFFUSE_Gloss_A,TRANSFORM_TEX(i.uv0, _DIFFUSE_Gloss_A));
                float gloss = _DIFFUSE_Gloss_A_var.a;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _SPEC_G_var = tex2D(_SPEC_G,TRANSFORM_TEX(i.uv0, _SPEC_G));
                float3 specularColor = (_SPECMULT*_SPEC_G_var.rgb);
                float specularMonochrome = dot(specularColor,float3(0.3,0.59,0.11));
                float HdotL = max(0.0,dot(halfDirection,lightDirection));
                float3 fresnelTerm = specularColor + ( 1.0 - specularColor ) * pow((1.0 - HdotL),5);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float alpha = 1.0 / ( sqrt( (Pi/4.0) * specPow + Pi/2.0 ) );
                float visTerm = ( NdotL * ( 1.0 - alpha ) + alpha ) * ( NdotV * ( 1.0 - alpha ) + alpha );
                visTerm = 1.0 / visTerm;
                float normTerm = (specPow + 8.0 ) / (8.0 * Pi);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*fresnelTerm*visTerm*normTerm;
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL)*InvPi * attenColor;
                float3 diffuse = directDiffuse * _DIFFUSE_Gloss_A_var.rgb;
                diffuse *= 1-specularMonochrome;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
