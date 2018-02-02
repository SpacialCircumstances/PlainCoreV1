﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using PlainCore.Graphics.Text;

namespace PlainCore.Graphics
{
    public class Font
    {
        protected internal Font(Texture fontTexture, Dictionary<string, GlyphLayout> glyphs)
        {
            this.fontTexture = fontTexture;
            this.glyphs = glyphs;
        }

        protected internal Texture fontTexture;
        protected internal Dictionary<string, GlyphLayout> glyphs;

        #region Public methods

        public void Draw(SpriteBatch batch, string text, int x, int y)
        {

        }

        #endregion
    }
}
