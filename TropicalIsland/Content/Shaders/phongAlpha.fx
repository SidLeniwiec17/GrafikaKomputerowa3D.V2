// Matrix
float4x4 World;
float4x4 View;
float4x4 Projection;

// Light related
float4 AmbientColor;
float AmbientIntensity;

float3 LightDirection;
float4 DiffuseColor;
float DiffuseIntensity;

float4 SpecularColor;
float3 CameraPosition;

texture ModelTexture;
float TextureAlpha = 1.0f;

texture ModelTexture2;
float TextureAlpha2 = 1.0f;



sampler2D textureSampler = sampler_state {
	Texture = (ModelTexture);
	MagFilter = Linear;
	MinFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};
sampler2D textureSampler2 = sampler_state {
	Texture = (ModelTexture2);
	MagFilter = Linear;
	MinFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

// The input for the VertexShader
struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 TextureCoordinate : TEXCOORD0;	
};

// The output from the vertex shader, used for later processing
struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float3 Normal : TEXCOORD1;
	float3 View : TEXCOORD2;
	float2 TextureCoordinate : TEXCOORD0;
};

// The VertexShader.
VertexShaderOutput VertexShaderFunction(VertexShaderInput input, float3 Normal : NORMAL)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	float3 normal = normalize(mul(Normal, World));
	output.Normal = normal;
	output.View = normalize(float4(CameraPosition, 1.0) - worldPosition);
	output.TextureCoordinate = input.TextureCoordinate;
	return output;
}

// The Pixel Shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 normal = float4(input.Normal, 1.0);
	float4 diffuse = saturate(dot(-LightDirection,normal));
	float4 reflect = normalize(2 * diffuse*normal - float4(LightDirection,1.0));
	float4 specular = pow(saturate(dot(reflect,input.View)),15);
	float4 textureColor = tex2D(textureSampler, input.TextureCoordinate);
	textureColor.a = 1;

	float4 textureColor2 = tex2D(textureSampler2, input.TextureCoordinate);
	textureColor2.a = 1;

	float4 finColor = saturate(textureColor * (AmbientColor*AmbientIntensity + DiffuseIntensity*DiffuseColor*diffuse + SpecularColor*specular));
	finColor.a = TextureAlpha;
	
	float4 finColor2 = saturate(textureColor2 * (AmbientColor*AmbientIntensity + DiffuseIntensity*DiffuseColor*diffuse + SpecularColor*specular));
	finColor2.a = TextureAlpha2;
	
	float4 finColor3 = (finColor * TextureAlpha) + (finColor2 * TextureAlpha2);

	return finColor3;
}

// Our Techinique
technique Technique2
{
	pass Pass2
	{	
        AlphaBlendEnable = TRUE;
		DestBlend = SRCALPHA;
        SrcBlend = SRCALPHA;
		VertexShader = compile vs_4_0 VertexShaderFunction();
		PixelShader = compile ps_4_0 PixelShaderFunction();
	}
}
