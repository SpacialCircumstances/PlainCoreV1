using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IBatch
    {
        void Begin(IRenderTarget target);
        void End();
    }
}
