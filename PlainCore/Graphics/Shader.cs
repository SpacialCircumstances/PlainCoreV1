using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics
{
    public class Shader
    {
        public Shader()
        {

        }

        public Shader(ShaderStages stage, string entryPoint, IShaderResource glslShader, IShaderResource metalShader, IShaderResource hlslShader, IShaderResource spirvShader)
        {
            Stage = stage;
            EntryPoint = entryPoint;
            GlslShader = glslShader;
            MetalShader = metalShader;
            HlslShader = hlslShader;
            SpirvShader = spirvShader;
        }

        public Shader(ShaderStages stage, string entryPoint)
        {
            Stage = stage;
            EntryPoint = entryPoint;
        }

        public IShaderResource GlslShader;
        public IShaderResource MetalShader;
        public IShaderResource HlslShader;
        public IShaderResource SpirvShader;
        public ShaderStages Stage;
        public string EntryPoint;

        public Veldrid.Shader CreateDeviceShader(GraphicsDevice device)
        {
            ResourceFactory factory = device.ResourceFactory;

            switch (device.BackendType)
            {
                case GraphicsBackend.OpenGL:
                    return factory.CreateShader(new ShaderDescription(Stage, GlslShader.LoadBytes(), EntryPoint));
                case GraphicsBackend.Direct3D11:
                    return factory.CreateShader(new ShaderDescription(Stage, HlslShader.LoadBytes(), EntryPoint));
                case GraphicsBackend.Vulkan:
                    return factory.CreateShader(new ShaderDescription(Stage, SpirvShader.LoadBytes(), EntryPoint));
                case GraphicsBackend.Metal:
                    return factory.CreateShader(new ShaderDescription(Stage, MetalShader.LoadBytes(), EntryPoint));
               default:
                    return factory.CreateShader(new ShaderDescription(Stage, GlslShader.LoadBytes(), EntryPoint));
            }
        }
    }
}
