sampler input;
float2 mousepos; 

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color;
	color=tex2D(input,coords.xy);
	return color;

    
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

       
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
