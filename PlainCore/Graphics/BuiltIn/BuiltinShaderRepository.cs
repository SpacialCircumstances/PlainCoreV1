using PlainCore.Graphics.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.BuiltIn
{
    public static class BuiltinShaderRepository
    {
        public static Shader GetBuiltinShader(Type vertexType, ShaderStages stage)
        {
            if(vertexType == typeof(VertexPosition))
            {
                if(stage == ShaderStages.Vertex)
                {
                    return new PositionVertexShader();
                }
                else if (stage == ShaderStages.Fragment)
                {
                    return new PositionFragmentShader();
                }
            }
            else if(vertexType == typeof(VertexPositionColor))
            {
                if(stage == ShaderStages.Vertex)
                {
                    return new PositionColorVertexShader();
                }
                else if(stage == ShaderStages.Fragment)
                {
                    return new PositionColorFragmentShader();
                }
            }
            else if(vertexType == typeof(VertexPositionTexture))
            {
                if(stage == ShaderStages.Vertex)
                {
                    return new PositionTextureVertexShader();
                }
                else if(stage == ShaderStages.Fragment)
                {
                    return new PositionTextureFragmentShader();
                }
            }
            else if(vertexType == typeof(VertexPositionColorTexture))
            {
                if(stage == ShaderStages.Vertex)
                {
                    return new PositionColorTextureVertexShader();
                }
                else if(stage == ShaderStages.Fragment)
                {
                    return new PositionColorTextureFragmentShader();
                }
            }

            return null;
        }
    }
}
