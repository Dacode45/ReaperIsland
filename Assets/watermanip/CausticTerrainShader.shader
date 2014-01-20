Shader "Hidden/TerrainEngine/Splatmap/Lightmap-FirstPass" {

Properties {

    _Control ("Control (RGBA)", 2D) = "red" {}

    _Splat3 ("Layer 3 (A)", 2D) = "white" {}

    _Splat2 ("Layer 2 (B)", 2D) = "white" {}

    _Splat1 ("Layer 1 (G)", 2D) = "white" {}

    _Splat0 ("Layer 0 (R)", 2D) = "white" {}

    // used in fallback on old cards

    _MainTex ("BaseMap (RGB)", 2D) = "white" {}

    _Color ("Main Color", Color) = (1,1,1,1)

    

    _SpecColor ("Specular Color", Color) = (1.0, 0.5, 0.5, 1)

}

    

SubShader {

    Tags {

        "SplatCount" = "4"

        "Queue" = "Geometry-100"

        "RenderType" = "Opaque"

    }

CGPROGRAM

#pragma surface surf BlinnPhong vertex:vert

#pragma target 3.0

 

struct Input {

    float2 uv_BumpMap;

    float2 uv_Control : TEXCOORD0;

    float2 uv_Splat0 : TEXCOORD1;

    float2 uv_Splat1 : TEXCOORD2;

    float2 uv_Splat2 : TEXCOORD3;

    float2 uv_Splat3 : TEXCOORD4;

};

 

void vert (inout appdata_full v) {

    float3 T1 = float3(1, 0, 1);

    float3 Bi = cross(T1, v.normal);

    float3 newTangent = cross(v.normal, Bi);

    normalize(newTangent);

    v.tangent.xyz = newTangent.xyz;

    if (dot(cross(v.normal,newTangent),Bi) < 0)

        v.tangent.w = -1.0f;

    else

        v.tangent.w = 1.0f;

}

 

sampler2D _Control;

sampler2D _Splat0, _Splat1, _Splat2, _Splat3;

sampler2D _Bump00, _Bump01, _Bump02, _Bump03;

sampler2D _DetailMap;

 

float _SpecPow00, _SpecPow01, _SpecPow02, _SpecPow03;

float _TileX00, _TileX01, _TileX02, _TileX03, _TileZ00, _TileZ01, _TileZ02, _TileZ03;

float _TerX, _TerZ;

half4 _SpecCol;

 

void surf (Input IN, inout SurfaceOutput o) {

    half4 splat_control = tex2D (_Control, IN.uv_Control);

    half3 col;

    _SpecColor = _SpecCol;

    

    col  = splat_control.r * tex2D (_Splat0, IN.uv_Splat0).rgb;

    o.Normal = splat_control.r * UnpackNormal (tex2D (_Bump00, float2(IN.uv_Control.x * (_TerX/_TileX00), IN.uv_Control.y * (_TerZ/_TileZ00))));

    o.Gloss = _SpecPow00 * splat_control.r;

    o.Specular = _SpecPow00 * splat_control.r;

    

    col += splat_control.g * tex2D (_Splat1, IN.uv_Splat1).rgb;

    o.Normal += splat_control.g * UnpackNormal (tex2D (_Bump01, float2(IN.uv_Control.x * (_TerX/_TileX01), IN.uv_Control.y * (_TerZ/_TileZ01))));

    o.Gloss += _SpecPow01 * splat_control.g;

    o.Specular += _SpecPow01 * splat_control.g;

    

    col += splat_control.b * tex2D (_Splat2, IN.uv_Splat2).rgb;

    o.Normal += splat_control.b * UnpackNormal (tex2D (_Bump02, float2(IN.uv_Control.x * (_TerX/_TileX02), IN.uv_Control.y * (_TerZ/_TileZ02))));

    o.Gloss += _SpecPow02 * splat_control.b;

    o.Specular += _SpecPow02 * splat_control.b;

    

    col += splat_control.a * tex2D (_Splat3, IN.uv_Splat3).rgb;

    col += splat_control.a * tex2D (_DetailMap, IN.uv_Splat3).rgb;

    o.Normal += splat_control.a * UnpackNormal (tex2D (_Bump03, float2(IN.uv_Control.x * (_TerX/_TileX03), IN.uv_Control.y * (_TerZ/_TileZ03))));

    o.Gloss += _SpecPow03 * splat_control.a;

    o.Specular += _SpecPow03 * splat_control.a;

    o.Albedo = col;

    o.Alpha = 0.0;

}

ENDCG  

}

 

// Fallback to Diffuse

Fallback "Diffuse"

}