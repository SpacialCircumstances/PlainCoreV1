using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IBatchable
    {
        Texture Texture { get; }
        Vector2 LowerCoordinates { get; }
        Vector2 UpperCoordinates { get; }
    }
}
