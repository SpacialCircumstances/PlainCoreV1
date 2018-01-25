using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Resources
{
    public class TruetypeFontResource : IFontResource
    {
        public TruetypeFontResource(string filename, string fontFamily, float size)
        {
            Filename = filename;
            FontFamily = fontFamily;
            Size = size;
        }

        public string Filename;
        public string FontFamily;
        public float Size;

        public Font CreateFont()
        {
            var collection = new FontCollection();
            collection.Install(Filename);
            var ttFont = collection.CreateFont(FontFamily, Size);

            var glyphs = new List<GlyphLayout>();

            

            var font = new Font(null, null);
            
            return font;
        }
    }
}
