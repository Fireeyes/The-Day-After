sampler input;
int range;
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	int x;
	int y;
	float2 minim={1,1};
	float2 current;
	if(coords.x==0)
	{
		for(x=0;x<=range/2;x=x+1)
		{
			coords.x=x;
			current=tex2D(input,coords);
			minim=min(minim,current);		
		}
	}
	if(coords.x==1)
	{
		for(x=range/2;x<=range;x++)
		{
			coords.x=x;
			current=tex2D(input,coords);
			minim=min(minim,current);		
		}
	}
    return float4(minim,0,1);
 }

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

       
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
