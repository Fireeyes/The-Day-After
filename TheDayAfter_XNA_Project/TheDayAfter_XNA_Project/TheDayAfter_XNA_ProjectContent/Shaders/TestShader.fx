sampler2D input ;
float red; 
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
    float4 color;
	color=tex2D(input,coords.xy);
	color.r=red;
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