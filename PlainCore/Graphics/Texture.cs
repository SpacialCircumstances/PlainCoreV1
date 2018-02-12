using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;
using Veldrid.ImageSharp;

namespace PlainCore.Graphics
{
    public class Texture: IBatchable
    {
        protected Texture()
        {

        }

        private Vector2 lowerCoords = new Vector2(0f);
        private Vector2 upperCoords = new Vector2(1f);

        #region Static factory methods

        public static Texture FromFile(GraphicsDevice device, string filename)
        {
            var tex = new Texture();
            var factory = device.ResourceFactory;
            var image = new ImageSharpTexture(filename);
            tex.DeviceTexture = image.CreateDeviceTexture(device, factory);
            tex.DeviceTextureView = factory.CreateTextureView(new TextureViewDescription(tex.DeviceTexture));

            return tex;
        }

        public static Texture FromImage(GraphicsDevice device, Image<Rgba32> img)
        {
            var tex = new Texture();
            var factory = device.ResourceFactory;
            var image = new ImageSharpTexture(img);
            tex.DeviceTexture = image.CreateDeviceTexture(device, factory);
            tex.DeviceTextureView = factory.CreateTextureView(new TextureViewDescription(tex.DeviceTexture));

            return tex;
        }

        #endregion

        #region Properties

        public int Width
        {
            get => (int)DeviceTexture.Width;
        }

        public int Height
        {
            get => (int)DeviceTexture.Height;
        }

        Texture IBatchable.Texture => this;

        public Vector2 LowerCoordinates => lowerCoords;

        public Vector2 UpperCoordinates => upperCoords;

        public Veldrid.Texture DeviceTexture;
        public TextureView DeviceTextureView;

        #endregion
    }
}
