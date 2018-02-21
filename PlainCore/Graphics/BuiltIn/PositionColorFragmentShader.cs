using PlainCore.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.BuiltIn
{
    public class PositionColorFragmentShader : Shader
    {
        public PositionColorFragmentShader()
        {
            Stage = ShaderStages.Fragment;
            EntryPoint = "FS";
            GlslShader = new ShaderFileResource(BuiltinShaderRepository.SHADER_REPO + "PositionColor-fragment.330.glsl");
        }
    }
}
