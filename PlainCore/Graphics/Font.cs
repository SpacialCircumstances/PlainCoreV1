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

        public void Draw(SpriteBatch batch, string text, float x, float y, RgbaFloat color, float scale = 1f)
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
                batch.Draw(fontTexture, color, currentX, y, glyph.GlyphSize.W * scale, glyph.GlyphSize.H * scale, x1, y1, x2, y2);
                currentX += glyph.GlyphSize.W * scale;
            }
        }

        public void Draw(SpriteBatch batch, string text, float x, float y, float scale = 1f)
        {
            Draw(batch, text, x, y, RgbaFloat.White, scale);
        }

        public (float, float) MeasureText(string text, float scale = 1f)
        {
            float currentX = 0f;
            float currentY = 0f;

            for(int i = 0; i < text.Length; i++)
            {
                string character = text.Substring(0, 1);
                var glyph = FontDescription.GetGlyph(character);
                currentX += glyph.GlyphSize.W;

                if(currentY < glyph.GlyphSize.H)
                {
                    currentY = glyph.GlyphSize.H;
                }
            }

            return (currentX * scale, currentY * scale);
        }

        #endregion
    }
}
