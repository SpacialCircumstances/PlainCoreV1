using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public class View : ITransformable
    {
        public View(Vector2 pos, float rot = 0f)
        {
            position = pos;
            rotation = rot;
            scale = new Vector2(1f);
        }

        public View(Vector2 pos, float rot, Vector2 scal)
        {
            position = pos;
            rotation = rot;
            scale = scal;
        }

        protected Vector2 position;
        protected float rotation;
        protected Vector2 scale;

        public Vector2 Position { get => position; set => position = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public Vector2 Scale { get => scale; set => scale = value; }

        public Matrix4x4 GetTransformationMatrix()
        {
            var rotationMatrix = Matrix4x4.CreateRotationZ(rotation);
            var scaleMatrix = Matrix4x4.CreateScale(scale.X, scale.Y, 0f);
            var translationMatrix = Matrix4x4.CreateTranslation(position.X, position.Y, 0f);
            return scaleMatrix * rotationMatrix * translationMatrix;
        }
    }
}
