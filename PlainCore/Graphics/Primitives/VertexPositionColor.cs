using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.Primitives
{
    public class VertexPositionColor
    {
        public VertexPositionColor(Vector2 position, RgbaFloat color)
        {
            Position = position;
            Color = color;
        }

        public Vector2 Position;
        public RgbaFloat Color;

        public const uint Size = 24;
    }
}
