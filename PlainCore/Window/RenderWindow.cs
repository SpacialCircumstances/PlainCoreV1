using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using PlainCore.Window;
using Veldrid;
using Veldrid.StartupUtilities;
using PlainCore.Graphics;
using Veldrid.Sdl2;

namespace PlainCore.Window
{
    public class RenderWindow: IRenderTarget
    {
        public RenderWindow(int width = 800, int height = 600, string title = "PlainCore", GraphicsDeviceOptions options = new GraphicsDeviceOptions())
        {
            this.width = width;
            this.height = height;
            this.title = title;
            graphicsDeviceOptions = options;
            view = new View(new Vector2(width, height), new Vector2(0, 0));
            view.Viewport = new System.FloatRectangle(new Vector2(0, 0), new Vector2(width, height));
            CreateWindow();
        }

        private int width;
        private int height;
        private string title;
        protected internal Sdl2Window window;
        private GraphicsDevice device;
        private GraphicsDeviceOptions graphicsDeviceOptions;
        private CommandList clearCommandList;
        private View view;

        #region Properties

        public GraphicsDevice Device
        {
            get => device;
        }

        public ResourceFactory Factory
        {
            get => device.ResourceFactory;
        }

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

        protected void CreateWindow()
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

            device = VeldridStartup.CreateGraphicsDevice(window, graphicsDeviceOptions, GraphicsBackend.OpenGL);

            //Only support OpenGL
            if (device.BackendType != GraphicsBackend.OpenGL)
            {
                throw new NotSupportedException("Only OpenGL backend is supported at the moment");
            }

            clearCommandList = device.ResourceFactory.CreateCommandList();
        }

        #region Public methods

        public void Display()
        {
            device.SwapBuffers();
        }

        public void Resize(int width, int height)
        {
            device.ResizeMainWindow((uint)width, (uint)height);
        }

        public void Clear(RgbaFloat color)
        {
            clearCommandList.Begin();
            clearCommandList.SetFramebuffer(device.SwapchainFramebuffer);
            clearCommandList.SetFullViewports();
            clearCommandList.ClearColorTarget(0, color);
            clearCommandList.End();

            device.SubmitCommands(clearCommandList);
        }

        public Framebuffer GetFramebuffer()
        {
            return device.SwapchainFramebuffer;
        }

        public View GetView()
        {
            return view;
        }

        public InputSnapshot DispatchEvents()
        {
            return window.PumpEvents();
        }

        #endregion
    }
}
