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
            var font = new Font(null, null);
            
            return font;
        }
    }
}
