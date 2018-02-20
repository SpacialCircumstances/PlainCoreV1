using System;
using System.Collections.Generic;
using System.Text;
using ShaderGen;
using System.Numerics;
using static ShaderGen.ShaderBuiltins;

[assembly: ShaderSet("PositionColorTexture", "PlainCore.Shaders.PositionColorTexture.VS", "PlainCore.Shaders.PositionColorTexture.FS")]

namespace PlainCore.Shaders
{
    public class PositionColorTexture
    {
        [ResourceSet(0)]
        public Matrix4x4 World;
        [ResourceSet(1)]
        public Texture2DResource SurfaceTexture;
        [ResourceSet(1)]
        public SamplerResource SurfaceSampler;

        [VertexShader]
        public FragmentInput VS(VertexInput input)
        {
            FragmentInput output;
            output.UV = input.TexCoords;
            output.frColor = input.Color;
            output.SystemPosition = Mul(World, new Vector4(input.Position, -1, 1));
            return output;
        }

        [FragmentShader]
        public Vector4 FS(FragmentInput input)
        {
            return Sample(SurfaceTexture, SurfaceSampler, input.UV) * input.frColor;
        }

        public struct VertexInput
        {
            [PositionSemantic] public Vector2 Position;
            [TextureCoordinateSemantic] public Vector2 TexCoords;
            [ColorSemantic] public Vector4 Color;
        }

        public struct FragmentInput
        {
            [SystemPositionSemantic] public Vector4 SystemPosition;
            [TextureCoordinateSemantic] public Vector2 UV;
            [ColorSemantic] public Vector4 frColor;
        }
    }
}
