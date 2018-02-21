using System;
using ShaderGen;
using System.Numerics;
using static ShaderGen.ShaderBuiltins;

[assembly: ShaderSet("PositionColor", "PlainCore.Shaders.PositionColor.VS", "PlainCore.Shaders.PositionColor.FS")]


namespace PlainCore.Shaders
{
    public class PositionColor
    {
        [ResourceSet(0)]
        public Matrix4x4 World;

        [VertexShader]
        public FragmentInput VS(VertexInput input)
        {
            FragmentInput output;
            output.frColor = input.Color;
            output.SystemPosition = Mul(World, new Vector4(input.Position, -1, 1));
            return output;
        }

        [FragmentShader]
        public Vector4 FS(FragmentInput input)
        {
            return input.frColor;
        }

        public struct VertexInput
        {
            [PositionSemantic] public Vector2 Position;
            [ColorSemantic] public Vector4 Color;
        }

        public struct FragmentInput
        {
            [SystemPositionSemantic] public Vector4 SystemPosition;
            [ColorSemantic] public Vector4 frColor;
        }
    }
}
