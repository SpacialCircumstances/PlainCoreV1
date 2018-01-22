using PlainCore.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.BuiltIn
{
    public class PositionFragmentShader : Shader
    {
        public PositionFragmentShader()
        {
            Stage = ShaderStages.Fragment;
            EntryPoint = "FS";
            GlslShader = new ShaderMemoryResource(Encoding.ASCII.GetBytes(glslShader));
        }

        private const string glslShader =
            @"#version 330 core

out vec4 fsout_Color;

void main()
{
    fsout_Color = vec4(1, 1, 1, 1);
}

";
    }
}
