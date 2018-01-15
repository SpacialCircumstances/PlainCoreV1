using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public interface IBatchable
    {
        Texture GetTexture();
        Vector2 GetLowerCoordinates();
        Vector2 GetUpperCoordinates();
    }
}
