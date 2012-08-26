sampler s0;
float pct;
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
    float4 color = tex2D(s0, coords);
	color.r = color.r * (sin(pct) + 2);
	color.b = color.b * (sin(pct) + 2);
	color.g = color.g * (sin(pct) + 2);
    return color;
}



technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
	pass Pass2
    {
        
    }
}