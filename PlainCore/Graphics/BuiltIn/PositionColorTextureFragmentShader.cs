using PlainCore.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.BuiltIn
{
    public class PositionColorTextureFragmentShader : Shader
    {
        public PositionColorTextureFragmentShader()
        {
            Stage = ShaderStages.Fragment;
            EntryPoint = "FS";
            GlslShader = new ShaderFileResource(BuiltinShaderRepository.SHADER_REPO + "PositionColorTexture-fragment.330.glsl");
        }
    }
}
