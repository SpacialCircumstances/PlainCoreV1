using PlainCore.System;
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
            rectangle = new FloatRectangle(lower, upper - lower);
        }

        public TextureRegion(Texture texture, FloatRectangle rect)
        {
            this.texture = texture;
            rectangle = rect;
        }

        protected Texture texture;
        protected FloatRectangle rectangle;

        public Texture Texture => texture;

        public FloatRectangle Area => rectangle;
    }
}
