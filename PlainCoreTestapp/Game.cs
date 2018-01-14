using PlainCore.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;

namespace PlainCoreTestapp
{
    class Game
    {
        public void Run()
        {
            var window = new RenderWindow();

            var texture1 = PlainCore.Graphics.Texture.FromFile(window.Device, "Images/Planet.png");

            var batch = new SpriteBatch(window.Device);

            DeviceBuffer db = window.Factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));

            Matrix4x4 m = Matrix4x4.Identity;
            window.Device.UpdateBuffer(db, 0, m);
            batch.Init();
            batch.SetWorldMatrix(db);

            while(window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(RgbaFloat.CornflowerBlue);

                batch.Begin();
                batch.Render(texture1, 0.1f, 0.1f, 0.5f, 0.5f);
                batch.End();

                window.Display();
            }

            Environment.Exit(0);
        }
    }
}
