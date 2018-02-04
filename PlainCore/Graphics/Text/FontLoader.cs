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

        public static Font LoadFromTruetypeFont(GraphicsDevice device, string filename, int fontSize)
        {
            var gen = new FontGenerator(filename, fontSize);
            var (img, glyphs) = gen.Generate();
            return Create(device, img, glyphs);
        }

        public static Font Create(GraphicsDevice device, Image<Rgba32> image, FontDescription description)
        {
            var texture = Texture.FromImage(device, image);
            return CreateFromTexture(texture, description);
        }

        public static Font CreateFromTexture(Texture texture, FontDescription description)
        {
            return new Font(texture, description);
        }

        public static void SaveFont(Font font, string imageFile, string glyphFile)
        {
            string json = JsonConvert.SerializeObject(font.FontDescription);
        }

        public static void SaveFontDefinition(Image<Rgba32> bitmap, FontDescription description, string imageFile, string glyphFile)
        {
            string json = JsonConvert.SerializeObject(description);
            bitmap.Save(imageFile);
            File.WriteAllText(glyphFile, json);
        }

        public static FontDescription LoadGlyphs(string glyphDefinition)
        {
            return JsonConvert.DeserializeObject<FontDescription>(glyphDefinition);
        }
    }
}
