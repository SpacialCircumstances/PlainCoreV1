
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlainCore.Graphics
{
    public class Sprite : ITransformable
    {
        public Sprite()
        {
            
        }

        public Sprite(IBatchable tex)
        {
            Texture = tex;
        }

        public Sprite(IBatchable texture, Vector2 position, float rotation, Vector2 scale) : this(texture)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public IBatchable Texture;

        protected Vector2 position = new Vector2();
        protected float rotation;
        protected Vector2 scale = new Vector2();
        protected Vector2 origin = new Vector2();

        public Vector2 Position { get => position; set => position = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public Vector2 Scale { get => scale; set => scale = value; }
        public Vector2 Origin { get => origin; set => origin = value; }

        public Matrix4x4 GetTransformationMatrix()
        {
            var rotationMatrix = Matrix4x4.CreateRotationZ(rotation);
            var scaleMatrix = Matrix4x4.CreateScale(scale.X, scale.Y, 0f);
            var translationMatrix = Matrix4x4.CreateTranslation(position.X, position.Y, 0f);
            return scaleMatrix * rotationMatrix * translationMatrix;
        }
    }
}
