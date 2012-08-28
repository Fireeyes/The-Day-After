texture tex;
sampler input  : register(s1);
float red;
float2 mousepos; 
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
    float4 color;
	color=tex2D(input,coords.xy);
	if(sqrt(
		pow((mousepos.x-coords.x),2)+
		pow((mousepos.y-coords.y),2)
		)<0.2)		
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