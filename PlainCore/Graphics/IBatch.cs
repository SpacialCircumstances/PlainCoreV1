using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IBatch: IDrawable
    {
        void Begin();
        void End();
    }
}
