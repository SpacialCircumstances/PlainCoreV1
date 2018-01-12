using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.Primitives
{
    public struct VertexPositionColorTexture
    {
        public VertexPositionColorTexture(Vector2 position, RgbaFloat color, Vector2 texCoords)
        {
            Position = position;
            Color = color;
            TextureCoordinates = texCoords;
        }

        public Vector2 Position;
        public RgbaFloat Color;
        public Vector2 TextureCoordinates;

        public const uint Size = 32;
    }
}
