// Monogame HLSL Shader for Texture Distortion

// Texture to be distorted
texture Texture;

// Sampler for the texture
sampler Sampler = sampler_state
{
    Texture = <Texture>;
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Linear;
    AddressU = Wrap; // Clamp to edge to avoid repeating distortion
    AddressV = Wrap;
};

// WorldViewProjection matrix (standard for 2D rendering in Monogame)
float4x4 WorldViewProjection;

// Time elapsed, used for animation of the distortion
float Time;

// --- Vertex Shader ---
// Input structure for the vertex shader
struct VertexShaderInput
{
    float4 Position : POSITION0; // Vertex position
    float2 TexCoord : TEXCOORD0; // Texture coordinates
};

// Output structure for the vertex shader (and input for the pixel shader)
struct VertexShaderOutput
{
    float4 Position : POSITION0; // Transformed vertex position
    float2 TexCoord : TEXCOORD0; // Original texture coordinates
};

// Main vertex shader function
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    // Transform the vertex position by the WorldViewProjection matrix
    output.Position = mul(input.Position, WorldViewProjection);

    // Pass the original texture coordinates directly to the pixel shader
    output.TexCoord = input.TexCoord;

    return output;
}

// --- Pixel Shader ---
// Main pixel shader function
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // Define distortion strength and frequency
    float distortionStrength = 0.05; // How much the texture will be distorted
    float distortionFrequency = 5.0; // How many waves of distortion

    // Calculate a distortion offset based on texture coordinates and time
    // Using sine and cosine to create a wavy, animated distortion effect
    float2 distortedTexCoord = input.TexCoord;
    distortedTexCoord.x += sin(input.TexCoord.y * distortionFrequency + Time) * distortionStrength;
    distortedTexCoord.y += cos(input.TexCoord.x * distortionFrequency + Time) * distortionStrength;

    // Sample the texture at the new, distorted coordinates
    float4 color = tex2D(Sampler, distortedTexCoord);

    return color;
}

// --- Techniques ---
// A technique defines a rendering pass using specific shaders
technique BasicDistortion
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction(); // Compile vertex shader for Shader Model 3.0
        PixelShader = compile ps_3_0 PixelShaderFunction(); // Compile pixel shader for Shader Model 3.0
    }
}