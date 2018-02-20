﻿using System;
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
        public Matrix4x4 Projection;
        [ResourceSet(0)]
        public Matrix4x4 View;

        [ResourceSet(1)]
        public Matrix4x4 World;
        [ResourceSet(1)]
        public Texture2DResource SurfaceTexture;
        [ResourceSet(1)]
        public SamplerResource SurfaceSampler;

        [VertexShader]
        public FragmentInput VS(VertexInput input)
        {
            FragmentInput output;
            Vector4 worldPosition = Mul(World, new Vector4(input.Position, 1));
            Vector4 viewPosition = Mul(View, worldPosition);
            Vector4 clipPosition = Mul(Projection, viewPosition);
            output.SystemPosition = clipPosition;
            output.TexCoords = input.TexCoords;

            return output;
        }

        [FragmentShader]
        public Vector4 FS(FragmentInput input)
        {
            return Sample(SurfaceTexture, SurfaceSampler, input.TexCoords);
        }

        public struct VertexInput
        {
            [PositionSemantic] public Vector3 Position;
            [TextureCoordinateSemantic] public Vector2 TexCoords;
        }

        public struct FragmentInput
        {
            [SystemPositionSemantic] public Vector4 SystemPosition;
            [TextureCoordinateSemantic] public Vector2 TexCoords;
        }
    }
}
