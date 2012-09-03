sampler input;
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color=tex2D(input, coords);
	float distance;
	distance=sqrt( pow((coords.x-0.5f),2)+pow((coords.y-0.5f),2));
	color.rgb=color.rgb+2*distance;
    return color;
}

technique Technique1
{
    pass Pass1
    {
        
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
