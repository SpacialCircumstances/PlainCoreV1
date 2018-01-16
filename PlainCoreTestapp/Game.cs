using PlainCore.Graphics;
using PlainCore.System;
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
            var texture2 = PlainCore.Graphics.Texture.FromFile(window.Device, "Images/Moon.png");
            var batch = new SpriteBatch(window.Device);

            var clock = new Clock();

            while(window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(RgbaFloat.CornflowerBlue);
                window.GetView().Rotation += 1f * (float)clock.Restart().TotalSeconds;
                batch.Begin(window);
                batch.Draw(texture1, 0.1f, 0.1f, 0.5f, 0.5f);
                batch.Draw(texture2, -0.6f, -0.4f, 0.4f, 0.4f);
                batch.End();

                window.Display();
            }

            Environment.Exit(0);
        }
    }
}
