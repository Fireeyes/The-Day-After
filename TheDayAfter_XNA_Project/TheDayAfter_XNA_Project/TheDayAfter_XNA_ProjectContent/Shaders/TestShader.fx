texture tex;
sampler input  : register(s0);
float red;
float2 mousepos; 
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
    float4 color;
	color=tex2D(input,coords.xy);
	if(coords.x>=0.3 && coords.y<=0.2)
		
		{
		color.rgb+=red;
		color.r+=red;
		}
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