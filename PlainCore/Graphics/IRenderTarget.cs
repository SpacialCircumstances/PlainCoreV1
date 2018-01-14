using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics
{
    public interface IRenderTarget
    {
        int Height { get; }
        int Width { get; }

        Framebuffer GetFramebuffer();
        View GetView();
    }
}
