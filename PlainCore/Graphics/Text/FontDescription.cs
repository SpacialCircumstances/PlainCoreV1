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

        public IReadOnlyDictionary<string, GlyphLayout> GlyphDefinition { get; set; }
        public int FontSize { get; set; }

        public GlyphLayout GetGlyph(string character)
        {
            return GlyphDefinition[character];
        }
    }
}
