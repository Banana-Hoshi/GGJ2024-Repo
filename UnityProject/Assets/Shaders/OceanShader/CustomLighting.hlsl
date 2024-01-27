#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

struct CustomLightingData
{
    //Surface Attributes
    float3 albedo;
};

float3 CalculateCustomLighting(CustomLightingData d)
{
    return d.albedo;
}

void CalculateCustomLighting_float(float3 albedo, out float3 Color)
{
    CustomLightingData d;
    d.albedo = albedo;
    
    Color = CalculateCustomLighting(d);
}

#endif