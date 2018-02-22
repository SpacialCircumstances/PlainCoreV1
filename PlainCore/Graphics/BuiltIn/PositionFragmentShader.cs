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
            GlslShader = new ShaderFileResource(BuiltinShaderRepository.SHADER_REPO + "Position-fragment.330.glsl");
        }
    }
}
