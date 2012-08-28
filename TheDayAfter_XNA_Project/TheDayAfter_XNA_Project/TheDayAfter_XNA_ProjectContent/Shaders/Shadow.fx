texture tex;
sampler input  : register(s1);
float2 mousepos; 

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
    // TODO: add your pixel shader code here.

    return float4(1, 0, 0, 1);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

       
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
