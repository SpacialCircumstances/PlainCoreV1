using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore
{
    public static class MathExtensions
    {
        public static Vector2 MultiplyVector(this Matrix3x2 matrix, Vector2 vector)
        {
            var vec = new Vector2();
            vec.X = (vector.X * matrix.M11) + (vector.Y * matrix.M12);
            vec.Y = (vector.X * matrix.M21) + (vector.Y * matrix.M22);
            return vec;
        }
    }
}
