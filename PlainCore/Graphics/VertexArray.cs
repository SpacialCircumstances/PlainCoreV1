using PlainCore.Graphics.BuiltIn;
using PlainCore.Graphics.Primitives;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics
{
    public class VertexArray
    {
        public VertexArray(GraphicsDevice device, int capacity, GeometryType geometryType = GeometryType.Points)
        {
            this.device = device;
            factory = device.ResourceFactory;
            this.geometryType = geometryType;
            this.capacity = (uint)capacity;
            EmptyTexture = Texture.FromImage(device, Image.LoadPixelData<Rgba32>(new byte[]{ (byte)255, (byte)255, (byte)255, (byte)255 }, 1, 1));
            Shaders = new List<Shader>
            {
                BuiltinShaderRepository.GetBuiltinShader(typeof(VertexPositionColorTexture), ShaderStages.Vertex),
                BuiltinShaderRepository.GetBuiltinShader(typeof(VertexPositionColorTexture), ShaderStages.Fragment)
            };
            CreateResources();
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

        private List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>();
        private List<Shader> Shaders;
        private GeometryType geometryType;

        #region Public properties

        public int Count { get => vertices.Count; }
        public GeometryType GeometryType {
            get => geometryType;
            set
            {
                geometryType = value;
                CreateResources();
            }
        }

        public Texture EmptyTexture;

        public VertexPositionColorTexture this[int index]
        {
            get => vertices[index];
            set => vertices[index] = value;
        }

        #endregion

        #region Public methods

        public void Add(VertexPositionColorTexture vertex)
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
            commands.SetFullViewports();
            commands.SetPipeline(pipeline);
            commands.SetVertexBuffer(0, vertexBuffer);
            commands.SetGraphicsResourceSet(0, worldResourceSet);
            commands.SetGraphicsResourceSet(1, factory.CreateResourceSet(new ResourceSetDescription(textureResourceLayout, texture.DeviceTextureView, device.Aniso4xSampler)));
            commands.Draw((uint)vertices.Count);
            commands.End();

            device.SubmitCommands(commands);
        }

        public void SetShaders(List<Shader> shaders)
        {
            Shaders = shaders;
            CreateResources();
        }

        #endregion

        protected void CreateResources()
        {
            uint size = capacity;
            vertexBuffer = factory.CreateBuffer(new BufferDescription(size * VertexPositionColorTexture.Size, BufferUsage.VertexBuffer));
            worldMatrixBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));

            worldResourceLayout = factory.CreateResourceLayout(new ResourceLayoutDescription(new ResourceLayoutElementDescription("World", ResourceKind.UniformBuffer, ShaderStages.Vertex)));
            textureResourceLayout = factory.CreateResourceLayout(new ResourceLayoutDescription(new ResourceLayoutElementDescription("SpriteTexture", ResourceKind.TextureReadOnly, ShaderStages.Fragment), new ResourceLayoutElementDescription("SpriteSampler", ResourceKind.Sampler, ShaderStages.Fragment)));

            var vertexLayoutDescription = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.Position, VertexElementFormat.Float2),
                new VertexElementDescription("Color", VertexElementSemantic.Color, VertexElementFormat.Float4),
                new VertexElementDescription("TextureCoords", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2)
                );

            var description = new GraphicsPipelineDescription
            {
                BlendState = BlendStateDescription.SingleOverrideBlend,
                DepthStencilState = new DepthStencilStateDescription(true, true, ComparisonKind.LessEqual),
                RasterizerState = new RasterizerStateDescription(FaceCullMode.None, PolygonFillMode.Solid, FrontFace.Clockwise, true, false),
                PrimitiveTopology = GetPrimitiveTopology(),
                ResourceLayouts = new[] { worldResourceLayout, textureResourceLayout },
                ShaderSet = new ShaderSetDescription(new VertexLayoutDescription[] { vertexLayoutDescription }, LoadShaders()),
                Outputs = device.SwapchainFramebuffer.OutputDescription
            };

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

        private PrimitiveTopology GetPrimitiveTopology()
        {
            switch(geometryType)
            {
                case GeometryType.Lines:
                    return PrimitiveTopology.LineList;
                case GeometryType.LineStrip:
                    return PrimitiveTopology.LineStrip;
                case GeometryType.Points:
                    return PrimitiveTopology.PointList;
                case GeometryType.Triangles:
                    return PrimitiveTopology.TriangleList;
                case GeometryType.TriangleStrip:
                    return PrimitiveTopology.TriangleStrip;
            }

            return PrimitiveTopology.PointList;
        }
    }
}
