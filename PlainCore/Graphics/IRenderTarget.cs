using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IRenderTarget
    {
        int Height { get; }
        int Width { get; }

        void Draw(IDrawable drawable);
    }
}
