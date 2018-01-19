using PlainCore.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.BuiltIn
{
    public class PositionColorTextureVertexShader : Shader
    {
        public PositionColorTextureVertexShader()
        {
            Stage = ShaderStages.Vertex;
            EntryPoint = "VS";
            GlslShader = new ShaderMemoryResource(Encoding.ASCII.GetBytes(glslShader));
        }

        private const string glslShader = @"
#version 330 core
in vec2 Position;
in vec2 TextureCoords;
in vec4 Color;

        uniform World
        {
            mat4 field_World;
        };

out vec2 UV;
out vec4 frColor;

        void main()
        {
            gl_Position = field_World * vec4(Position, 0, 1);
            gl_Position.z = gl_Position.z * 2.0 - gl_Position.w;
            UV = TextureCoords;
            frColor = Color;
        }
";
    }
}
