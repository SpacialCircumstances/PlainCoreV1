using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.System
{
    public class FloatRectangle
    {
        public FloatRectangle(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public FloatRectangle()
        {

        }

        public Vector2 Position = new Vector2(0, 0);
        public Vector2 Size = new Vector2(1, 1);
    }
}
