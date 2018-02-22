using System;
using ShaderGen;
using System.Numerics;
using static ShaderGen.ShaderBuiltins;

[assembly: ShaderSet("Position", "PlainCore.Shaders.Position.VS", "PlainCore.Shaders.Position.FS")]

namespace PlainCore.Shaders
{
    public class Position
    {
        [ResourceSet(0)]
        public Matrix4x4 World;

        [VertexShader]
        public FragmentInput VS(VertexInput input)
        {
            FragmentInput output;
            output.SystemPosition = Mul(World, new Vector4(input.Position, -1, 1));
            return output;
        }

        [FragmentShader]
        public Vector4 FS(FragmentInput input)
        {
            return new Vector4(1, 1, 1, 1);
        }

        public struct VertexInput
        {
            [PositionSemantic] public Vector2 Position;
        }

        public struct FragmentInput
        {
            [SystemPositionSemantic] public Vector4 SystemPosition;
        }
    }
}
