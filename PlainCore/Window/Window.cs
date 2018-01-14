using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;
using Veldrid.StartupUtilities;
using Veldrid.Sdl2;

namespace PlainCore.Window
{
    public class Window
    {
        public Window(int width = 800, int height = 600, string title = "PlainCore")
        {
            this.width = width;
            this.height = height;
            this.title = title;
            CreateWindow();
        }

        private int width;
        private int height;
        private string title;
        protected internal Sdl2Window window;

        #region Properties

        public int Width
        {
            set => width = value;
            get => width;
        }

        public int Height
        {
            set => height = value;
            get => height;
        }

        public string Title
        {
            set => title = value;
            get => title;
        }

        public bool IsOpen
        {
            get => window.Exists;
        }

        #endregion

        public InputSnapshot DispatchEvents()
        {
            return window.PumpEvents();
        }

        protected virtual void CreateWindow()
        {
            var wci = new WindowCreateInfo()
            {
                WindowHeight = Height,
                WindowWidth = Width,
                WindowTitle = Title,
                X = 100,
                Y = 100
            };

            window = VeldridStartup.CreateWindow(ref wci);
        }
    }
}
