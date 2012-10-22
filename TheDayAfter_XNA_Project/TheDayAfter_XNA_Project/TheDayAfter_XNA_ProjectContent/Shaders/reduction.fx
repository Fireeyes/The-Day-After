sampler input;
float range;
float order;
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 current=tex2D(input,coords);
	/*  ditched old implentation
	float4 cmpr;
	if(coords.x%order==0)
		{
			if(coords.x<0.5)
				{
					cmpr=tex2D(input,float2( clamp( (coords.x+order), 0,0.5),coords.y));
				}
			if(coords.x>0.5)
				{
					cmpr=tex2D(input,float2(clamp( (coords.x-order),0.5,1),coords.y));
				}
		}
	return float4(min(current.r,cmpr.r),min(current.g,cmpr.g),0,1);
	kept here for old times sake 
	*/


 }

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

       
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
