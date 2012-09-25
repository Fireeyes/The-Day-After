sampler input;
float range;
float order;
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 current=tex2D(input,coords);
	float4 cmpr;
	if(coords.x<0.5)
		{
			cmpr=tex2D(input,float2( clamp( (coords.x+(order/2*range)), 0,0.5),coords.y));
		}
	if(coords.x>0.5)
		{
			cmpr=tex2D(input,float2(clamp( (coords.x-(float)(order/2*range)),0.5,1),coords.y));
		}
		//(float)(order/2*range)
    return float4(min(current.r,cmpr.r),min(current.g,cmpr.g),0,1);
	
 }

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

       
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
