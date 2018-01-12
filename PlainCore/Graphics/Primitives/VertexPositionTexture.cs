using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Primitives
{
    public struct VertexPositionTexture
    {
        public VertexPositionTexture(Vector2 position, Vector2 texCoords)
        {
            Position = position;
            TextureCoordinates = texCoords;
        }

        public Vector2 Position;
        public Vector2 TextureCoordinates;

        public const uint Size = 16;
    }
}
