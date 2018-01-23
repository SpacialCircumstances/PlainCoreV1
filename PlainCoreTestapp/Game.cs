using PlainCore.Graphics;
using PlainCore.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;
using PlainCore.Graphics.Primitives;

namespace PlainCoreTestapp
{
    class Game
    {
        public void Run()
        {
            var window = new RenderWindow();

            var texture1 = PlainCore.Graphics.Texture.FromFile(window.Device, "Images/Planet.png");
            var texture2 = PlainCore.Graphics.Texture.FromFile(window.Device, "Images/Moon.png");
            var varray = new VertexArray<VertexPositionColor>(window.Device, 3, PrimitiveTopology.TriangleList);
            varray.Add(new VertexPositionColor(new Vector2(0.5f, 0.5f), RgbaFloat.Red));
            varray.Add(new VertexPositionColor(new Vector2(-0.5f, 0.5f), RgbaFloat.Red));
            varray.Add(new VertexPositionColor(new Vector2(0.5f, -0.5f), RgbaFloat.Red));

            var clock = new Clock();

            while(window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(RgbaFloat.CornflowerBlue);
                window.GetView().Rotation += (float)clock.Restart().TotalSeconds * 0.2f;

                varray.Draw(window, varray.EmptyTexture);

                window.Display();
            }

            Environment.Exit(0);
        }
    }
}
