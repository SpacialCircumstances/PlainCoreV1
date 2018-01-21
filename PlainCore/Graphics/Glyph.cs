using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public struct Glyph
    {
        public Glyph(char character, Vector2 size)
        {
            Character = character;
            Size = size;
        }

        public char Character;
        public Vector2 Size;
    }
}
