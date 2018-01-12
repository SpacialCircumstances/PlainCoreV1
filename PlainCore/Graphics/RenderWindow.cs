using System;
using System.Collections.Generic;
using System.Text;
using PlainCore.Window;
using Veldrid;
using Veldrid.StartupUtilities;

namespace PlainCore.Graphics
{
    public class RenderWindow: Window.Window, IRenderTarget
    {
        public RenderWindow(int width = 800, int height = 600, string title = "PlainCore", GraphicsDeviceOptions options = new GraphicsDeviceOptions()): base(width, height, title)
        {
            graphicsDeviceOptions = options;
        }

        private GraphicsDevice device;
        private GraphicsDeviceOptions graphicsDeviceOptions;
        private CommandList clearCommandList;

        #region Properties

        public GraphicsDevice Device
        {
            get => device;
        }

        public ResourceFactory Factory
        {
            get => device.ResourceFactory;
        }

        #endregion

        protected override void CreateWindow()
        {
            base.CreateWindow();
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

        public void Draw(IDrawable drawable)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
