using PlainCore.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics
{
    public class View
    {
        public View(Vector2 size, Vector2 center, float rotation = 0f)
        {
            this.size = size;
            this.center = center;
            this.rotation = rotation;
        }

        public View()
        {
        }

        private bool updateNeeded = true;
        private bool updateViewport = false;

        protected Vector2 size = new Vector2(800, 600);
        protected Vector2 center = new Vector2(0, 0);
        protected FloatRectangle viewportRect = new FloatRectangle();
        protected float rotation = 0f;

        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                updateNeeded = true;
            }
        }

        public Vector2 Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
                updateNeeded = true;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                updateNeeded = true;
            }
        }

        public FloatRectangle Viewport
        {
            get
            {
                return viewportRect;
            }
            set
            {
                viewportRect = value;
                updateViewport = true;
            }
        }

        protected Matrix4x4 transform = Matrix4x4.Identity;
        protected Viewport viewport = new Viewport();

        public Matrix4x4 GetTransformationMatrix()
        {
            if(updateNeeded)
            {
                transform = ComputeTransform();
                updateNeeded = false;
            }

            return transform;
        }

        public Viewport GetViewport()
        {
            if(updateViewport)
            {
                updateViewport = false;
                viewport = ComputeViewport();
            }

            return viewport;
        }

        protected Matrix4x4 ComputeTransform()
        {
            var hx = Size.X / 2;
            var hy = Size.Y / 2;
            var rotationMatrix = Matrix4x4.CreateRotationZ(Rotation);
            var scaleMatrix = Matrix4x4.CreateScale(2 / Size.X, 2 / Size.Y, 0f);
            var translationMatrix = Matrix4x4.CreateTranslation(Center.X - hx, Center.Y - hy, 0f);
            return translationMatrix * scaleMatrix * rotationMatrix;
        }

        protected Viewport ComputeViewport()
        {
            return new Viewport(viewportRect.Position.X, viewportRect.Position.Y, viewportRect.Size.X, viewportRect.Size.Y, 1f, -1f);
        }
    }
}
