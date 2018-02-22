using PlainCore.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.BuiltIn
{
    public class PositionVertexShader : Shader
    {
        public PositionVertexShader()
        {
            Stage = ShaderStages.Vertex;
            EntryPoint = "VS";
            GlslShader = new ShaderFileResource(BuiltinShaderRepository.SHADER_REPO + "Position-vertex.330.glsl");
        }
    }
}
