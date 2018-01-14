using PlainCore.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCoreTestapp
{
    class Game
    {
        public void Run()
        {
            var window = new RenderWindow();

            while(window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(RgbaFloat.CornflowerBlue);
                window.Display();
            }
        }
    }
}
