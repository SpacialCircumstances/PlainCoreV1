using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.Primitives
{
    public static class PrimitivesInfo
    {
        public static uint GetSize(Type primitive)
        {
            if (primitive == typeof(VertexPosition))
            {
                return VertexPosition.Size;
            }
            else if (primitive == typeof(VertexPositionColor))
            {
                return VertexPositionColor.Size;
            }
            else if (primitive == typeof(VertexPositionTexture))
            {
                return VertexPositionTexture.Size;
            }
            else if (primitive == typeof(VertexPositionColorTexture))
            {
                return VertexPositionColorTexture.Size;
            }

            return 0;
        }

        public static VertexLayoutDescription? GetVertexLayoutDescription(Type primitive, GraphicsDevice device)
        {
            if (primitive == typeof(VertexPosition))
            {
                return new VertexLayoutDescription(
                    new VertexElementDescription("Position", VertexElementSemantic.Position, VertexElementFormat.Float2)
                    );
            }
            else if (primitive == typeof(VertexPositionColor))
            {
                return new VertexLayoutDescription(
                    new VertexElementDescription("Position", VertexElementSemantic.Position, VertexElementFormat.Float2),
                    new VertexElementDescription("Color", VertexElementSemantic.Color, VertexElementFormat.Float4)
                    );
            }
            else if (primitive == typeof(VertexPositionTexture))
            {
                return new VertexLayoutDescription(
                    new VertexElementDescription("Position", VertexElementSemantic.Position, VertexElementFormat.Float2),
                    new VertexElementDescription("TextureCoords", VertexElementSemantic.Color, VertexElementFormat.Float2)
                    );
            }
            else if (primitive == typeof(VertexPositionColorTexture))
            {
                return new VertexLayoutDescription(
                    new VertexElementDescription("Position", VertexElementSemantic.Position, VertexElementFormat.Float2),
                    new VertexElementDescription("Color", VertexElementSemantic.Color, VertexElementFormat.Float4),
                    new VertexElementDescription("TextureCoords", VertexElementSemantic.Color, VertexElementFormat.Float2)
                    );
            }

            return null;
        }
    }
}
