using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.Text
{
    public static class FontLoader
    {
        public static Font LoadFromDefinition(string bitmapFile, string glyphFile)
        {
            return null;
        }

        public static Font LoadFromTruetypeFont(string filename)
        {
            return null;
        }

        public static Font Create(GraphicsDevice device, Image<Rgba32> image, string glyphDefinition)
        {
            var texture = Texture.FromImage(device, image);
            return CreateFromTexture(texture, glyphDefinition);
        }

        public static Font CreateFromTexture(Texture texture, string glyphDefinition)
        {
            return null;
        }

        public static void SaveFont(Font font, string imageFile, string glyphFile)
        {

        }

        public static List<GlyphLayout> LoadGlyphs(string glyphDefinition)
        {
            return new List<GlyphLayout>();
        }
    }
}
