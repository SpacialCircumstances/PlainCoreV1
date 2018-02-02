using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using PlainCore.Graphics.Text;

namespace PlainCore.Graphics
{
    public class Font
    {
        protected internal Font(Texture fontTexture, FontDescription description)
        {
            this.fontTexture = fontTexture;
            this.FontDescription = description;
        }

        protected internal Texture fontTexture;
        public FontDescription FontDescription;

        #region Public methods

        public void Draw(SpriteBatch batch, string text, int x, int y)
        {

        }

        #endregion
    }
}
