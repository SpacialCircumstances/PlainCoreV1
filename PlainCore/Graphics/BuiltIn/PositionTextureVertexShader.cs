using PlainCore.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.BuiltIn
{
    public class PositionTextureVertexShader : Shader
    {
        public PositionTextureVertexShader()
        {
            Stage = ShaderStages.Vertex;
            EntryPoint = "VS";
            GlslShader = new ShaderFileResource(BuiltinShaderRepository.SHADER_REPO + "PositionTexture-vertex.330.glsl");
        }
    }
}
