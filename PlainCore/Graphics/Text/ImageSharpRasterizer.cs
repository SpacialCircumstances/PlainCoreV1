using NRasterizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Text
{
    class ImageSharpRasterizer : IGlyphRasterizer
    {
        public int Resolution => throw new NotImplementedException();

        public void BeginRead(int countourCount)
        {
            throw new NotImplementedException();
        }

        public void CloseFigure()
        {
            throw new NotImplementedException();
        }

        public void Curve3(double p2x, double p2y, double x, double y)
        {
            throw new NotImplementedException();
        }

        public void Curve4(double p2x, double p2y, double p3x, double p3y, double x, double y)
        {
            throw new NotImplementedException();
        }

        public void EndRead()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void LineTo(double x, double y)
        {
            throw new NotImplementedException();
        }

        public void MoveTo(double x, double y)
        {
            throw new NotImplementedException();
        }
    }
}
