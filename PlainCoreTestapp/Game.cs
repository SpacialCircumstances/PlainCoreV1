using PlainCore.Graphics;
using PlainCore.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;
using PlainCore.Graphics.Primitives;
using PlainCore.Graphics.Resources;
using PlainCore.Graphics.Text;
using PlainCore.Window;
using PlainCore.Graphics.BuiltIn;

namespace PlainCoreTestapp
{
    public class Game
    {
        public void Run()
        {
            BuiltinShaderRepository.SHADER_REPO = "../PlainCore.Shaders/Shaders/";

            var window = new RenderWindow();
            window.View.Viewport = new FloatRectangle(0, 0, 800, 600);

            var texture1 = PlainCore.Graphics.Texture.FromFile(window.Device, "Images/Planet.png");
            var texture2 = PlainCore.Graphics.Texture.FromFile(window.Device, "Images/Moon.png");

            var batch = new SpriteBatch(window.Device);

            var font = FontLoader.LoadFromTruetypeFont(window.Device, "Fonts/OpenSans-Regular.ttf", 40);

            var clock = new Clock();

            while(window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(RgbaFloat.CornflowerBlue);
                batch.Begin(window);
                batch.Draw(texture1, 0, 0, 800f, 600f);
                font.Draw(batch, "Test.", 0, 0, RgbaFloat.Red);
                batch.End();
                window.Display();
            }

            Environment.Exit(0);
        }
    }
}
