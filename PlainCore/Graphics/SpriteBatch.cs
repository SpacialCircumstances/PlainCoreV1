using PlainCore.Graphics.BuiltIn;
using PlainCore.Graphics.Primitives;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics
{
    public class SpriteBatch : IBatch
    {

        private const int MAX_BATCH = 1024;

        public SpriteBatch(GraphicsDevice device)
        {
            this.device = device;
            CreateResources();
            vertices = new List<VertexPositionTexture>();
        }

        private DeviceBuffer vertexBuffer;
        private DeviceBuffer indexBuffer;
        private DeviceBuffer worldMatrixBuffer;
        private Veldrid.Shader vertexShader;
        private Veldrid.Shader fragmentShader;
        private ResourceLayout resourceLayout;
        private Pipeline pipeline;
        private ResourceSet resourceSet;
        private Texture texture;
        private ResourceLayout worldResourceLayout;
        private ResourceSet worldResourceSet;
        private CommandList commandList;

        private GraphicsDevice device;
        private bool drawing;
        private uint index;
        private IRenderTarget target;

        private List<VertexPositionTexture> vertices;

        private ushort[] indices;

        private Vector2 nullVector = new Vector2(0, 0);

        public void Begin(IRenderTarget target)
        {
            if (drawing)
            {
                throw new InvalidOperationException("Multiple calls to Begin");
            }

            this.target = target;

            drawing = true;
            index = 0;
            device.UpdateBuffer(worldMatrixBuffer, 0, target.GetView().GetTransformationMatrix());
        }

        public void End()
        {
            Flush();
            drawing = false;
            texture = null;
        }

        public void Draw(IBatchable batchable, float x, float y, float width, float height, float texX1 = 0f, float texY1 = 0f, float texX2 = 1f, float texY2 = 1f)
        {
            CheckForFlush(batchable.GetTexture());
            float w = width;
            float h = height;

            float lowerX = texX1 * batchable.GetLowerCoordinates().X;
            float upperX = texX2 * batchable.GetUpperCoordinates().X;
            float lowerY = texY1 * batchable.GetLowerCoordinates().Y;
            float upperY = texY2 * batchable.GetUpperCoordinates().Y;

            PushVertex(x, y + h, lowerX, lowerY);
            PushVertex(x + w, y + h, upperX, lowerY);
            PushVertex(x, y, lowerX, upperY);
            PushVertex(x + w, y, upperX, upperY);
            index++;
        }

        public void Draw(IBatchable batchable, float x, float y, float width, float height, float originX, float originY, float rotation, float texX1, float texY1, float texX2, float texY2)
        {
            CheckForFlush(batchable.GetTexture());

            float lowerX = texX1 * batchable.GetLowerCoordinates().X;
            float upperX = texX2 * batchable.GetUpperCoordinates().X;
            float lowerY = texY1 * batchable.GetLowerCoordinates().Y;
            float upperY = texY2 * batchable.GetUpperCoordinates().Y;

            float ox = originX * width;
            float oy = originY * height;

            var rotationMatrix = Matrix3x2.CreateRotation(rotation);

            var ld = rotationMatrix.MultiplyVector(new Vector2(-ox, -oy));
            var lu = rotationMatrix.MultiplyVector(new Vector2(-ox, height - oy));
            var ru = rotationMatrix.MultiplyVector(new Vector2(width - ox, -oy));
            var rd = rotationMatrix.MultiplyVector(new Vector2(width - ox, height - oy));

            var position = new Vector2(x, y);
            ld += position;
            lu += position;
            ru += position;
            rd += position;

            PushVertex(lu, lowerX, lowerY);
            PushVertex(rd, upperX, lowerY);
            PushVertex(ld, lowerX, upperY);
            PushVertex(ru, upperX, upperY);

            index++;
        }

        public void Draw(IBatchable batchable, float x, float y, float width, float height, float originX, float originY, float rotation)
        {
            Draw(batchable, x, y, width, height, originX, originY, rotation, 0f, 0f, 1f, 1f);
        }

        public void Draw(Sprite sprite)
        {
            Draw(sprite.Texture, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y, sprite.Origin.X, sprite.Origin.Y, sprite.Rotation);
        }

        private void PushVertex(float x, float y, float tx, float ty)
        {
            vertices.Add(new VertexPositionTexture(new Vector2(x, y), new Vector2(tx, ty)));
        }

        private void PushVertex(Vector2 pos, float tx, float ty)
        {
            vertices.Add(new VertexPositionTexture(pos, new Vector2(tx, ty)));
        }

        protected void Flush()
        {
            if (!drawing)
            {
                throw new InvalidOperationException("Batch is not in drawing state");
            }

            indices = new ushort[index * 6];
            for (int i = 0; i < index; i++)
            {
                int idx = i * 6;
                int inst = i * 4;
                indices[idx] = (ushort)inst;
                indices[idx + 1] = (ushort)(inst + 1);
                indices[idx + 2] = (ushort)(inst + 2);
                indices[idx + 3] = (ushort)(inst + 1);
                indices[idx + 4] = (ushort)(inst + 3);
                indices[idx + 5] = (ushort)(inst + 2);
            }

            

            device.UpdateBuffer(vertexBuffer, 0, vertices.ToArray());
            device.UpdateBuffer(indexBuffer, 0, indices);

            resourceSet = device.ResourceFactory.CreateResourceSet(new ResourceSetDescription(resourceLayout, texture.DeviceTextureView, device.Aniso4xSampler));

            commandList.Begin();
            commandList.SetFramebuffer(target.GetFramebuffer());
            commandList.SetFullViewports();
            commandList.SetVertexBuffer(0, vertexBuffer);
            commandList.SetIndexBuffer(indexBuffer, IndexFormat.UInt16);
            commandList.SetPipeline(pipeline);
            commandList.SetGraphicsResourceSet(0, worldResourceSet);
            commandList.SetGraphicsResourceSet(1, resourceSet);
            commandList.DrawIndexed(index * 6, index * 2, 0, 0, 0);
            commandList.End();

            device.SubmitCommands(commandList);

            vertices.Clear();
            index = 0;
        }

        private void CreateResources()
        {
            ResourceFactory factory = device.ResourceFactory;

            vertexBuffer = factory.CreateBuffer(new BufferDescription(MAX_BATCH * VertexPositionTexture.Size, BufferUsage.VertexBuffer));
            indexBuffer = factory.CreateBuffer(new BufferDescription(MAX_BATCH * sizeof(ushort), BufferUsage.IndexBuffer));
            worldMatrixBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));

            var vertexLayoutDescription = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.Position, VertexElementFormat.Float2),
                new VertexElementDescription("TextureCoords", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2)
                );

            LoadShaders();

            resourceLayout = factory.CreateResourceLayout(new ResourceLayoutDescription(new ResourceLayoutElementDescription("SpriteTexture", ResourceKind.TextureReadOnly, ShaderStages.Fragment), new ResourceLayoutElementDescription("SpriteSampler", ResourceKind.Sampler, ShaderStages.Fragment)));

            worldResourceLayout = factory.CreateResourceLayout(new ResourceLayoutDescription(new ResourceLayoutElementDescription("World", ResourceKind.UniformBuffer, ShaderStages.Vertex)));

            worldResourceSet = device.ResourceFactory.CreateResourceSet(new ResourceSetDescription(worldResourceLayout, worldMatrixBuffer));

            var description = new GraphicsPipelineDescription();
            description.BlendState = BlendStateDescription.SingleOverrideBlend;
            description.DepthStencilState = new DepthStencilStateDescription(true, true, ComparisonKind.LessEqual);
            description.RasterizerState = new RasterizerStateDescription(FaceCullMode.None, PolygonFillMode.Solid, FrontFace.Clockwise, true, false);
            description.PrimitiveTopology = PrimitiveTopology.TriangleList;
            description.ResourceLayouts = new[] { worldResourceLayout, resourceLayout };
            description.ShaderSet = new ShaderSetDescription(new VertexLayoutDescription[] { vertexLayoutDescription }, new Veldrid.Shader[] { vertexShader, fragmentShader });
            description.Outputs = device.SwapchainFramebuffer.OutputDescription;

            pipeline = factory.CreateGraphicsPipeline(description);

            commandList = factory.CreateCommandList();
        }

        private void LoadShaders()
        {
            var vShader = new SpriteBatchVertexShader();
            vertexShader = vShader.CreateDeviceShader(device);

            var fShader = new SpriteBatchFragmentShader();
            fragmentShader = fShader.CreateDeviceShader(device);
        }

        private void CheckForFlush(Texture tex)
        {
            if (texture == null)
            {
                texture = tex;
                return;
            }

            if (tex != texture || index == MAX_BATCH)
            {
                Flush();
            }
            texture = tex;
        }
    }
}
