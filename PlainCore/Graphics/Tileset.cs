using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics
{
    public class Tileset
    {
        protected Tileset()
        {

        }

        private Texture texture;
        private IReadOnlyDictionary<string, TextureRegion> tiles;

        public static Tileset LoadFromFile(GraphicsDevice device, string regionsFile, string textureFile)
        {
            var texture = Texture.FromFile(device, textureFile);
            var regionsContent = File.ReadAllText(regionsFile);
            var regions = JsonConvert.DeserializeObject<Dictionary<string, TextureRegion>>(regionsContent);

            return Create(regions, texture);
        }

        public static Tileset Create(Dictionary<string, TextureRegion> regions, Texture texture)
        {
            return new Tileset
            {
                texture = texture,
                tiles = regions
            };
        }

        public static void Save(Dictionary<string, TextureRegion> regions, Image<Rgba32> image, string regionsFile, string bitmapFile)
        {
            image.Save(bitmapFile);
            var json = JsonConvert.SerializeObject(regions);
            File.WriteAllText(regionsFile, json);
        }

        public Texture TilesetTexture => texture;
        public IReadOnlyDictionary<string, TextureRegion> Tiles => tiles;
    }
}
