using PlainCore.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IBatchable
    {
        Texture Texture { get; }
        FloatRectangle Area { get; }
    }
}
