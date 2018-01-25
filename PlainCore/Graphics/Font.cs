using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using SixLabors.Fonts;

namespace PlainCore.Graphics
{
    public class Font
    {
        protected internal Font(Texture fontTexture, List<GlyphLayout> glyphs)
        {
            this.fontTexture = fontTexture;
            this.glyphs = glyphs;
        }

        protected Texture fontTexture;
        protected List<GlyphLayout> glyphs;

        #region Public methods

        public void Draw(SpriteBatch batch, string text, int x, int y)
        {

        }

        #endregion
    }
}
