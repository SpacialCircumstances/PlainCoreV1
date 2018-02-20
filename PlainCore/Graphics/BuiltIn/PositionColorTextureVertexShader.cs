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
            GlslShader = new ShaderFileResource(BuiltinShaderRepository.SHADER_REPO + "PositionColorTexture-vertex.330.glsl");
        }
    }
}
