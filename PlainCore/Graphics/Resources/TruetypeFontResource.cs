using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;

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

        private const int MAX_SIZE_X = 1024;

        public string Filename;
        public string FontFamily;
        public float Size;

        public Font CreateFont()
        {
            var collection = new FontCollection();
            collection.Install(Filename);
            var ttFont = collection.CreateFont(FontFamily, Size);

            var glyphs = new List<GlyphLayout>();

            var renderOptions = new RendererOptions(ttFont);
            renderOptions.ApplyKerning = false;

            float offsetX = 0;
            int yCount = 0;

            for (int i = 0; i < 128; i++)
            {
                var character = (char)i;
                var representation = Convert.ToString(character);
                var charSize = TextMeasurer.MeasureBounds(representation, renderOptions);
                var width = charSize.Right;
                var glyph = new Glyph(character, new Vector2(width, charSize.Bottom));

                if(offsetX + width > MAX_SIZE_X)
                {
                    offsetX = 0;
                    yCount++;
                }

                offsetX += width;

                var gLayout = new GlyphLayout()
                {
                    Glyph = glyph,
                    BitmapPosition = new Vector2(offsetX, yCount * (Size * 1.5f)),
                };

                glyphs.Add(gLayout);
            }

            var lastGlyph = glyphs[glyphs.Count - 1];
            var bitmapHeight = 400;

            var bitmap = new Image<Rgba32>(MAX_SIZE_X, bitmapHeight);
            bitmap.Mutate(i => {
                foreach(var glyph in glyphs)
                {
                    i.DrawText(Convert.ToString(glyph.Glyph.Character), ttFont, Rgba32.White, new SixLabors.Primitives.PointF(glyph.BitmapPosition.X, glyph.BitmapPosition.Y));
                }
            });



            var ts = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            bitmap.Save(ts + ".png");

            var font = new Font(null, glyphs);
            
            return font;
        }
    }
}
