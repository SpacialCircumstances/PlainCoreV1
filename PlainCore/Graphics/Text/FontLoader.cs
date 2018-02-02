using Newtonsoft.Json;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.Text
{
    public static class FontLoader
    {
        public static Font LoadFromDefinition(GraphicsDevice device, string bitmapFile, string glyphFile)
        {
            var image = Image.Load(bitmapFile);
            var glyphFileContent = File.ReadAllText(glyphFile);
            var glyphs = LoadGlyphs(glyphFileContent);
            return Create(device, image, glyphs);
        }

        public static Font LoadFromTruetypeFont(GraphicsDevice device, string filename)
        {
            var gen = new FontGenerator(filename);
            var (img, glyphs) = gen.Generate();
            return Create(device, img, glyphs);
        }

        public static Font Create(GraphicsDevice device, Image<Rgba32> image, Dictionary<string, GlyphLayout> glyphDefinition)
        {
            var texture = Texture.FromImage(device, image);
            return CreateFromTexture(texture, glyphDefinition);
        }

        public static Font CreateFromTexture(Texture texture, Dictionary<string, GlyphLayout> glyphDefinition)
        {
            return new Font(texture, glyphDefinition);
        }

        public static void SaveFont(Font font, string imageFile, string glyphFile)
        {
            string json = JsonConvert.SerializeObject(font.glyphs);
        }

        public static Dictionary<string, GlyphLayout> LoadGlyphs(string glyphDefinition)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, GlyphLayout>>(glyphDefinition);
        }
    }
}
