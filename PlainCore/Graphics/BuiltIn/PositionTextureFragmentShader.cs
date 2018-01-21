using PlainCore.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.BuiltIn
{
    public class PositionTextureFragmentShader : Shader
    {
        public PositionTextureFragmentShader()
        {
            Stage = ShaderStages.Fragment;
            EntryPoint = "FS";
            GlslShader = new ShaderMemoryResource(Encoding.ASCII.GetBytes(glslShader));
        }

        private const string glslShader =
            @"#version 330 core

in vec2 UV;
uniform sampler2D SpriteTexture;
out vec4 fsout_Color;

void main()
{
    fsout_Color = texture(SpriteTexture, UV);
}

";
    }
}
