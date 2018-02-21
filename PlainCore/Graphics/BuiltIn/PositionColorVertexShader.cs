using PlainCore.Graphics.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics.BuiltIn
{
    public class PositionColorVertexShader : Shader
    {
        public PositionColorVertexShader()
        {
            Stage = ShaderStages.Vertex;
            EntryPoint = "VS";
            GlslShader = new ShaderFileResource(BuiltinShaderRepository.SHADER_REPO + "PositionColor-vertex.330.glsl");
        }
    }
}
