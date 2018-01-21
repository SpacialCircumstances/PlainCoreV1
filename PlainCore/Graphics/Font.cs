using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using SixLabors.Fonts;

namespace PlainCore.Graphics
{
    public class Font
    {
        protected Font()
        {

        }

        public static Font CreateFromFile(string filename, string family, float size)
        {
            var font = new Font();

            var fc = new FontCollection();
            fc.Install(filename);
            font.internalFont = fc.CreateFont(family, size);
            font.CreateFontBitmap();
            return font;
        }

        protected SixLabors.Fonts.Font internalFont;
        protected Texture fontTexture;
        protected List<GlyphLayout> glyphs;

        #region Public methods

        public void Draw(SpriteBatch batch, string text, int x, int y)
        {

        }

        #endregion

        protected void CreateFontBitmap()
        {
            var glyphs = new List<Glyph>();
            var ro = new RendererOptions(internalFont);

            for(byte b = 0; b < 128; b++)
            {
                char c = Convert.ToChar(b);
                var size = TextMeasurer.Measure(Convert.ToString(c), ro);
                glyphs.Add(new Glyph(c, new Vector2(size.Width, size.Height)));
            }
        }
    }
}
