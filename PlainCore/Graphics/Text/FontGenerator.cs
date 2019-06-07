using System;
using System.Collections.Generic;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SharpFont;
using System.IO;
using System.Runtime.InteropServices;
using SixLabors.Primitives;

namespace PlainCore.Graphics.Text
{
    public enum Antialiasing
    {
        None,
        MonocolorAlpha,
        Clamp
    }

    public class FontGenerator
    {
        public FontGenerator(string filename, int fontSize = 40, Antialiasing aa = Antialiasing.MonocolorAlpha)
        {
            this.filename = filename;
            this.fontSize = fontSize;
            this.aa = aa;
        }

        private string filename;
        private int fontSize;
        private Antialiasing aa;
        private const int MAX_BITMAP_WIDTH = 1024;
        private const int HORIZONTAL_OFFSET = 2; //Reduces artifacts when scaling up

        public (Image<Rgba32>, FontDescription) Generate()
        {
            var font = new FontFace(File.OpenRead(filename));

            var glyphs = new Dictionary<string, GlyphLayout>();

            var currentX = 0;
            var currentY = 0;
            var maxY = 0;

            for (int i = 33; i < 127; i++)
            {
                char c = (char)i;
                string character = new string(c, 1);
                var (w, h) = GetGlyphSize(font, character, fontSize);

                //Glyph would be to big
                if (currentX + w + HORIZONTAL_OFFSET > MAX_BITMAP_WIDTH)
                {
                    currentY += maxY;
                    maxY = 0;
                    currentX = 0;
                }


                //Glyph is biggest in its line
                if (h > maxY)
                {
                    maxY = h;
                }

                var layout = new GlyphLayout(character, (currentX, currentY), (w, h));
                currentX += w + HORIZONTAL_OFFSET;

                glyphs.Add(character, layout);
            }

            var finalHeight = currentY + maxY;

            var bitmap = new Image<Rgba32>(MAX_BITMAP_WIDTH, finalHeight);

            bitmap.Mutate(ctx =>
            {
                foreach (var glyph in glyphs)
                {
                    var img = RenderGlyph(font, glyph.Key, fontSize);
                    var size = new Size(glyph.Value.GlyphSize.W, glyph.Value.GlyphSize.H);
                    var pos = new Point(glyph.Value.BitmapPosition.X, glyph.Value.BitmapPosition.Y);
                    ctx.DrawImage(img, pos, 1f);
                }
            });

            return (bitmap, new FontDescription(glyphs, 40));
        }

        protected unsafe Image<Rgba32> RenderGlyph(FontFace face, string character, int size)
        {
            var glyph = face.GetGlyph(character[0], size);
            var (w, h) = GetGlyphSize(face, character, size);

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
            var pixelData = ConvertToPixels(rawData);


            return Image.LoadPixelData<Rgba32>(pixelData, w, h);
        }

        protected (int, int) GetGlyphSize(FontFace face, string character, int size)
        {
            var glyph = face.GetGlyph(character[0], size);
            return (glyph.RenderWidth, glyph.RenderHeight);
        }

        protected byte[] ConvertToPixels(byte[] rawData)
        {
            if(aa == Antialiasing.MonocolorAlpha)
            {
                return ConvertAAMonocolorAlpha(rawData);
            }
            else if(aa == Antialiasing.Clamp)
            {
                return ConvertAAClamp(rawData);
            }
            else
            {
                return ConvertAANone(rawData);
            }
        }

        private byte[] ConvertAANone(byte[] rawData)
        {
            var len = rawData.Length;
            var pixelData = new byte[len * 4];
            int index = 0;
            for (int i = 0; i < len; i++)
            {
                byte c = rawData[i];
                pixelData[index++] = c;
                pixelData[index++] = c;
                pixelData[index++] = c;
                pixelData[index++] = c;
            }

            return pixelData;
        }

        private byte[] ConvertAAMonocolorAlpha(byte[] rawData)
        {
            var len = rawData.Length;
            var pixelData = new byte[len * 4];
            int index = 0;
            for (int i = 0; i < len; i++)
            {
                byte c = rawData[i];
                pixelData[index++] = 255;
                pixelData[index++] = 255;
                pixelData[index++] = 255;
                pixelData[index++] = c;
            }

            return pixelData;
        }

        private byte[] ConvertAAClamp(byte[] rawData)
        {
            var len = rawData.Length;
            var pixelData = new byte[len * 4];
            int index = 0;
            for (int i = 0; i < len; i++)
            {
                byte c = rawData[i];
                pixelData[index++] = c;
                pixelData[index++] = c;
                pixelData[index++] = c;
                if(c < 128)
                {
                    pixelData[index++] = 0;
                }
                else
                {
                    pixelData[index++] = c;
                }
            }

            return pixelData;
        }
    }
}
