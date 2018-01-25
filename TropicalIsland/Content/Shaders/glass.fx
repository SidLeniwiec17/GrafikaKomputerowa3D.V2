
#define VS_SHADERMODEL vs_4_0
#define PS_SHADERMODEL ps_4_0


float4x4 World;
float4x4 View;
float4x4 Projection;

float3 CameraPosition;
float3 LightDirection = float3(1, 0, 0);

float3 DiffuseColor = float3(.85, .85, .85);


Texture ReflectionCubeMap;
samplerCUBE ReflectionCubeMapSampler = sampler_state
{
	texture = <ReflectionCubeMap>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
	AddressU = Mirror;
	AddressV = Mirror;
};

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
	float3 Normal : NORMAL0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
	float3 Normal : TEXCOORD1;
	float4 WorldPosition : TEXCOORD2;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	output.WorldPosition = worldPosition;
	output.UV = input.UV;
	output.Normal = mul(input.Normal, World);

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 v = normalize(mul(input.Position - float4(CameraPosition, 1.0), World));
	float3 r = 1.14;
	float3 d = mul(input.Normal, v);
	float3 reflect = 2 * input.Normal * d - v;
	float3 z = (r*d - sqrt(1 - pow(r, 2) * (1 - pow(d, 2))))*input.Normal - r*v;
	
	float3 reflection = texCUBE(ReflectionCubeMapSampler, z).xyz;
	float4 finColor3 = float4(reflection, 0.5);

	float4 diffuse = saturate(dot(-LightDirection, input.Normal));

	finColor3 = (diffuse * 0.1) + (finColor3 * 0.9);
	return finColor3;
}

technique Ambient
{
	pass Pass1
	{
		VertexShader = compile VS_SHADERMODEL VertexShaderFunction();
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
}

