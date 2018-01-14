﻿using PlainCore.Graphics.BuiltIn;
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

        private const int MAX_BATCH = 1000;

        public SpriteBatch(GraphicsDevice device)
        {
            this.device = device;
        }

        private DeviceBuffer vertexBuffer;
        private DeviceBuffer indexBuffer;
        private Veldrid.Shader vertexShader;
        private Veldrid.Shader fragmentShader;
        private ResourceLayout resourceLayout;
        private Pipeline pipeline;
        private ResourceSet resourceSet;
        private Texture texture;
        private ResourceLayout worldResourceLayout;
        private ResourceSet worldResourceSet;

        private GraphicsDevice device;
        private bool drawing = false;
        private uint index;

        private List<VertexPositionTexture> vertices;

        private ushort[] indices;

        public void Init()
        {
            CreateResources();
            vertices = new List<VertexPositionTexture>();
        }

        public void Begin()
        {
            drawing = true;
            index = 0;
            vertices.Clear();
        }

        public void End()
        {
            Flush();
            drawing = false;
            texture = null;
        }

        public void Render(Texture texture, float x, float y, float width, float height)
        {
            CheckForFlush(texture);
            float w = width;
            float h = height;

            vertices.Add(new VertexPositionTexture(new Vector2(x, y + h), new Vector2(0, 0)));
            vertices.Add(new VertexPositionTexture(new Vector2(x + w, y + h), new Vector2(1, 0)));
            vertices.Add(new VertexPositionTexture(new Vector2(x, y), new Vector2(0, 1)));
            vertices.Add(new VertexPositionTexture(new Vector2(x + w, y), new Vector2(1, 1)));

            index++;
        }

        protected void Flush()
        {
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

            var commandList = device.ResourceFactory.CreateCommandList();

            device.UpdateBuffer(vertexBuffer, 0, vertices.ToArray());
            device.UpdateBuffer(indexBuffer, 0, indices);

            resourceSet = device.ResourceFactory.CreateResourceSet(new ResourceSetDescription(resourceLayout, texture.DeviceTextureView, device.Aniso4xSampler));

            commandList.Begin();
            commandList.SetFramebuffer(device.SwapchainFramebuffer);
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

            var vertexLayoutDescription = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.Position, VertexElementFormat.Float2),
                new VertexElementDescription("TextureCoords", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2)
                );

            LoadShaders();

            resourceLayout = factory.CreateResourceLayout(new ResourceLayoutDescription(new ResourceLayoutElementDescription("SpriteTexture", ResourceKind.TextureReadOnly, ShaderStages.Fragment), new ResourceLayoutElementDescription("SpriteSampler", ResourceKind.Sampler, ShaderStages.Fragment)));

            worldResourceLayout = factory.CreateResourceLayout(new ResourceLayoutDescription(new ResourceLayoutElementDescription("World", ResourceKind.UniformBuffer, ShaderStages.Vertex)));

            var description = new GraphicsPipelineDescription();
            description.BlendState = BlendStateDescription.SingleOverrideBlend;
            description.DepthStencilState = new DepthStencilStateDescription(true, true, ComparisonKind.LessEqual);
            description.RasterizerState = new RasterizerStateDescription(FaceCullMode.None, PolygonFillMode.Solid, FrontFace.Clockwise, true, false);
            description.PrimitiveTopology = PrimitiveTopology.TriangleList;
            description.ResourceLayouts = new[] { worldResourceLayout, resourceLayout };
            description.ShaderSet = new ShaderSetDescription(new VertexLayoutDescription[] { vertexLayoutDescription }, new Veldrid.Shader[] { vertexShader, fragmentShader });
            description.Outputs = device.SwapchainFramebuffer.OutputDescription;

            pipeline = factory.CreateGraphicsPipeline(description);
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

            if (tex != texture)
            {
                Flush();
            }
            texture = tex;
        }

        public void SetWorldMatrix(DeviceBuffer buffer)
        {
            worldResourceSet = device.ResourceFactory.CreateResourceSet(new ResourceSetDescription(worldResourceLayout, buffer));
        }
    }
}