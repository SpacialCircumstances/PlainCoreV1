using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public class TextureRegion: IBatchable
    {
        public TextureRegion(Texture texture, Vector2 lower, Vector2 upper)
        {
            this.texture = texture;
            lowerCoords = lower;
            upperCoords = upper;
        }

        protected Texture texture;
        protected Vector2 lowerCoords;
        protected Vector2 upperCoords;

        public Texture Texture => texture;

        public Vector2 LowerCoordinates => lowerCoords;

        public Vector2 UpperCoordinates => upperCoords;
    }
}
