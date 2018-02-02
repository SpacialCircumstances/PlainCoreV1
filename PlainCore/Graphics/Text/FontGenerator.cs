using System;
using System.Collections.Generic;
using System.Numerics;
using SixLabors.ImageSharp;
using SharpFont;
using System.IO;
using System.Runtime.InteropServices;
using SixLabors.Primitives;

namespace PlainCore.Graphics.Text
{
    public class FontGenerator
    {
        public FontGenerator(string filename)
        {
            this.filename = filename;
        }

        private string filename;
        private const int MAX_BITMAP_WIDTH = 1024;

        public (Image<Rgba32>, Dictionary<string, GlyphLayout>) Generate()
        {
            var font = new FontFace(File.OpenRead(filename));

            var glyphs = new Dictionary<string, GlyphLayout>();

            var currentX = 0;
            var currentY = 0;
            var maxY = 0;

            for(int i = 33; i < 127; i++)
            {
                char c = (char)i;
                string character = new string(c, 1);
                var (w, h) = GetGlyphSize(font, character);
                
                //Glyph would be to big
                if(currentX + w > MAX_BITMAP_WIDTH)
                {
                    currentY += maxY;
                    maxY = 0;
                    currentX = 0;
                }


                //Glyph is biggest in its line
                if(h > maxY)
                {
                    maxY = h;
                }

                var layout = new GlyphLayout(character, (currentX, currentY), (w, h));
                currentX += w;

                glyphs.Add(character, layout);
            }

            var finalHeight = currentY + maxY;

            var bitmap = new Image<Rgba32>(MAX_BITMAP_WIDTH, finalHeight);

            bitmap.Mutate(ctx =>
            {
                foreach (var glyph in glyphs)
                {
                    var img = RenderGlyph(font, glyph.Key);
                    var size = new Size(glyph.Value.GlyphSize.W, glyph.Value.GlyphSize.H);
                    var pos = new Point(glyph.Value.BitmapPosition.X, glyph.Value.BitmapPosition.Y);
                    ctx.DrawImage(img, 1f, size, pos);
                }
            });

            bitmap.Save("font.png");

            return (bitmap, glyphs);
        }

        protected unsafe Image<Rgba32> RenderGlyph(FontFace face, string character, int size = 40)
        {
            var glyph = face.GetGlyph(character[0], size);
            var (w, h) = GetGlyphSize(face, character);

            var surface = new Surface
            {
                Bits = Marshal.AllocHGlobal(w * h),
                Width = w,
                Height = h,
                Pitch = w
            };

            //Clear the memory
            var stuff = (byte*)surface.Bits;
            for (int i = 0; i < surface.Width * surface.Height; i++)
                *stuff++ = 0;

            glyph.RenderTo(surface);

            int len = w * h;
            var rawData = new byte[len];
            Marshal.Copy(surface.Bits, rawData, 0, len);
            var pixelData = new byte[len * 4];
            int index = 0;
            for (int i = 0; i < len; i++)
            {
                byte c = rawData[i];
                pixelData[index++] = c;
                pixelData[index++] = c;
                pixelData[index++] = c;
                pixelData[index++] = 255;
            }

            return Image.LoadPixelData<Rgba32>(pixelData, w, h);
        }

        protected (int, int) GetGlyphSize(FontFace face, string character, int size = 40)
        {
            var glyph = face.GetGlyph(character[0], size);
            return ((int)(glyph.HorizontalMetrics.LinearAdvance + glyph.HorizontalMetrics.Bearing.X), glyph.RenderHeight);
        }
    }
}
