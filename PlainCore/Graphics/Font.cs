using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using PlainCore.Graphics.Text;
using Veldrid;

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

        public void Draw(SpriteBatch batch, string text, float x, float y)
        {
            float currentX = x;
            for (int i = 0; i < text.Length; i++)
            {
                string character = text.Substring(i, 1);
                var glyph = FontDescription.GetGlyph(character);
                var fx = 1f / fontTexture.Width;
                var fy = 1f / fontTexture.Height;
                var x1 = fx * (float)glyph.BitmapPosition.X;
                var y1 = fy * (float)glyph.BitmapPosition.Y;
                var x2 = x1 + (fx * (float)glyph.GlyphSize.W);
                var y2 = y1 + (fy * (float)glyph.GlyphSize.H);
                batch.Draw(fontTexture, RgbaFloat.White, currentX, y, glyph.GlyphSize.W, glyph.GlyphSize.H, x1, y1, x2, y2);
                currentX += glyph.GlyphSize.W;
            }
        }
        #endregion
    }
}
