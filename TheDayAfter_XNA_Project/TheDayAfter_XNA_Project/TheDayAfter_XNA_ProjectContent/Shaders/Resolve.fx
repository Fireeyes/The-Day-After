	sampler input;
	float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
	bool vertical=0;
	float rangex=coords.x*2-1;
	float rangey=coords.y*2-1;
	if(abs(rangex)<abs(rangey))
		{
			vertical=0;
		}
	float distance=sqrt( pow((coords.x-0.5f),2)+pow((coords.y-0.5f),2));
	rangey=rangey/(abs(rangex));
	rangey=(rangey+1)/2;
	float2 comparecoords;
	//horizontal info was stored in the Red component
	if(vertical==0)
		{
			comparecoords=float2( coords.x<0.5f?0:1,rangey);
			
		}
	else
		{
			comparecoords=float2( coords.y>0.5f?0:1,rangey);
			//comparecoords = float2(coords.y,v0);
		}	
	float4 comparecolor=tex2D(input,comparecoords);
	if(vertical==1)
		{
			comparecolor.r=comparecolor.g;
		}
	if(distance*2<comparecolor.r)
	{
		return float4(1,1,1,1);	
	}
	else
	{
		return float4(0,0,0,1);
	}
}

technique Technique1
{
    pass Pass1
    { 
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
