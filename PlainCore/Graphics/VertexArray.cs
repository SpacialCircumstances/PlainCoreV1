using PlainCore.Graphics.BuiltIn;
using PlainCore.Graphics.Primitives;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics
{
    public class VertexArray<T> where T: struct
    {
        public VertexArray(GraphicsDevice device, int capacity, PrimitiveTopology geometryType = PrimitiveTopology.PointList)
        {
            
            this.device = device;
            factory = device.ResourceFactory;
            this.geometryType = geometryType;
            this.capacity = (uint)capacity;

            var vShader = BuiltinShaderRepository.GetBuiltinShader(typeof(T), ShaderStages.Vertex);
            var fShader = BuiltinShaderRepository.GetBuiltinShader(typeof(T), ShaderStages.Fragment);

            if(vShader == null || fShader == null)
            {
                throw new NotSupportedException("Not a supported vertex type");
            }

            Shaders = new List<Shader>
            {
                vShader,
                fShader
            };

            uint vertexSize = PrimitivesInfo.GetSize(typeof(T));

            if(vertexSize == 0)
            {
                throw new NotSupportedException("Not a supported vertex type");
            }

            var vld = PrimitivesInfo.GetVertexLayoutDescription(typeof(T));

            if (!vld.HasValue)
            {
                throw new NotSupportedException("Not a supported vertex type");
            }

            var resourceLayoutDescription = GetResourceLayoutDescription(typeof(T));

            CreateResources(vertexSize, resourceLayoutDescription, vld.Value);
        }

        public VertexArray(GraphicsDevice device, int capacity, PrimitiveTopology geometryType, List<Shader> shaders, uint vertexSize, ResourceLayoutDescription resourceLayoutDescription, VertexLayoutDescription vertexLayoutDescription, bool hasTexture)
        {
            this.device = device;
            factory = device.ResourceFactory;
            this.capacity = (uint)capacity;
            this.geometryType = geometryType;
            Shaders = shaders;
            this.hasTexture = hasTexture;
            CreateResources(vertexSize, resourceLayoutDescription, vertexLayoutDescription);
        }

        private GraphicsDevice device;
        private ResourceFactory factory;

        private Pipeline pipeline;
        private CommandList commands;
        private DeviceBuffer vertexBuffer;
        private DeviceBuffer worldMatrixBuffer;
        private ResourceSet worldResourceSet;
        private ResourceLayout worldResourceLayout;
        private ResourceLayout textureResourceLayout;

        private uint capacity;

        private List<T> vertices = new List<T>();
        private List<Shader> Shaders;
        private PrimitiveTopology geometryType;

        private bool hasTexture;

        #region Public properties

        public int Count { get => vertices.Count; }
        public PrimitiveTopology GeometryType {
            get => geometryType;
        }

        public T this[int index]
        {
            get => vertices[index];
            set => vertices[index] = value;
        }

        #endregion

        #region Public methods

        public void Add(T vertex)
        {
            vertices.Add(vertex);
        }

        public void Clear()
        {
            vertices.Clear();
        }

        public void Draw(IRenderTarget target, Texture texture)
        {
            device.UpdateBuffer(vertexBuffer, 0, vertices.ToArray());
            device.UpdateBuffer(worldMatrixBuffer, 0, target.GetView().GetTransformationMatrix());

            worldResourceSet = factory.CreateResourceSet(new ResourceSetDescription(worldResourceLayout, worldMatrixBuffer));

            commands.Begin();
            commands.SetFramebuffer(target.GetFramebuffer());
            commands.SetViewport(0, target.GetView().GetViewport());
            commands.SetPipeline(pipeline);
            commands.SetVertexBuffer(0, vertexBuffer);
            commands.SetGraphicsResourceSet(0, worldResourceSet);
            if (hasTexture)
            {
                commands.SetGraphicsResourceSet(1, factory.CreateResourceSet(new ResourceSetDescription(textureResourceLayout, texture.DeviceTextureView, device.Aniso4xSampler)));
            }
                commands.Draw((uint)vertices.Count);
            commands.End();

            device.SubmitCommands(commands);
        }

        #endregion

        protected void CreateResources(uint vertexSize, ResourceLayoutDescription resourceLayoutDescription, VertexLayoutDescription vertexLayoutDescription)
        {
            uint size = capacity;
            vertexBuffer = factory.CreateBuffer(new BufferDescription(size * vertexSize, BufferUsage.VertexBuffer));
            worldMatrixBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));

            worldResourceLayout = factory.CreateResourceLayout(new ResourceLayoutDescription(new ResourceLayoutElementDescription("World", ResourceKind.UniformBuffer, ShaderStages.Vertex)));

            GraphicsPipelineDescription description;

            if (hasTexture)
            {
                textureResourceLayout = factory.CreateResourceLayout(resourceLayoutDescription);

                description = new GraphicsPipelineDescription
                {
                    BlendState = BlendStateDescription.SingleOverrideBlend,
                    DepthStencilState = new DepthStencilStateDescription(true, true, ComparisonKind.LessEqual),
                    RasterizerState = new RasterizerStateDescription(FaceCullMode.None, PolygonFillMode.Solid, FrontFace.Clockwise, true, false),
                    PrimitiveTopology = GeometryType,
                    ResourceLayouts = new[] { worldResourceLayout, textureResourceLayout },
                    ShaderSet = new ShaderSetDescription(new VertexLayoutDescription[] { vertexLayoutDescription }, LoadShaders()),
                    Outputs = device.SwapchainFramebuffer.OutputDescription
                };
            }
            else
            {
                description = new GraphicsPipelineDescription
                {
                    BlendState = BlendStateDescription.SingleOverrideBlend,
                    DepthStencilState = new DepthStencilStateDescription(true, true, ComparisonKind.LessEqual),
                    RasterizerState = new RasterizerStateDescription(FaceCullMode.None, PolygonFillMode.Solid, FrontFace.Clockwise, true, false),
                    PrimitiveTopology = GeometryType,
                    ResourceLayouts = new[] { worldResourceLayout },
                    ShaderSet = new ShaderSetDescription(new VertexLayoutDescription[] { vertexLayoutDescription }, LoadShaders()),
                    Outputs = device.SwapchainFramebuffer.OutputDescription
                };
            }

            pipeline = factory.CreateGraphicsPipeline(description);

            commands = factory.CreateCommandList();
        }
        
        protected Veldrid.Shader[] LoadShaders()
        {
            var shaders = new Veldrid.Shader[Shaders.Count];

            for (int i = 0; i < Shaders.Count; i++)
            {
                shaders[i] = Shaders[i].CreateDeviceShader(device);
            }

            return shaders;
        }

        protected ResourceLayoutDescription GetResourceLayoutDescription(Type primitive)
        {
            if (primitive == typeof(VertexPositionColorTexture) || primitive == typeof(VertexPositionTexture))
            {
                hasTexture = true;
                return new ResourceLayoutDescription(new ResourceLayoutElementDescription("SpriteTexture", ResourceKind.TextureReadOnly, ShaderStages.Fragment), new ResourceLayoutElementDescription("SpriteSampler", ResourceKind.Sampler, ShaderStages.Fragment));
            }

            hasTexture = false;
            return new ResourceLayoutDescription();
        }
    }
}
