sampler input;
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	//translate u and v into [-1 , 1] domain
	  float u0 = coords.x * 2 - 1;
	  float v0 = coords.y * 2 - 1;
	  
	  //then, as u0 approaches 0 (the center), v should also approach 0 
	  v0 = v0 * abs(u0);

      //convert back from [-1,1] domain to [0,1] domain
	  v0 = (v0 + 1) / 2;

	  //we now have the coordinates for reading from the initial image
	  float2 newCoords = float2(coords.x, v0);

	  //read for both horizontal and vertical direction and store them in separate channels
	  float horizontal = tex2D(input, newCoords).r;
	  float vertical = tex2D(input, newCoords.yx).r;
      return float4(horizontal,vertical ,0,1);

    
}


technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

       
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
