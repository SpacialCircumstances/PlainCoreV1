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

        public FloatRectangle(float x, float y, float w, float h): this(new Vector2(x, y), new Vector2(w, h))
        {

        }

        public Vector2 Position = new Vector2(0, 0);
        public Vector2 Size = new Vector2(1, 1);
    }
}
