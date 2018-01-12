using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics.Primitives
{
    public struct VertexPosition
    {
        public VertexPosition(Vector2 position)
        {
            Position = position;
        }

        public Vector2 Position;

        public const uint Size = 8;
    }
}
