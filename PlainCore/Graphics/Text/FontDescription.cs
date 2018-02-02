using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Text
{
    public class FontDescription
    {
        public FontDescription(Dictionary<string, GlyphLayout> glyphs, int fontSize)
        {
            GlyphDefinition = glyphs;
            FontSize = fontSize;
        }

        protected Dictionary<string, GlyphLayout> GlyphDefinition;
        public int FontSize;

        public GlyphLayout GetGlyph(string character)
        {
            return GlyphDefinition[character];
        }
    }
}
