using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;

namespace PlainCore.Graphics
{
    public class VertexArray<T> where T: struct
    {
        public VertexArray(GraphicsDevice device, GeometryType geometryType = GeometryType.Points, int initalCapacity = 24)
        {
            this.device = device;
            factory = device.ResourceFactory;
            GeometryType = geometryType;
            CreateResources(initalCapacity);
        }

        private GraphicsDevice device;
        private ResourceFactory factory;

        private Pipeline pipeline;
        private CommandList commands;
        private DeviceBuffer vertexBuffer;
        private DeviceBuffer worldMatrixBuffer;

        private List<T> vertices;

        #region Public properties

        public GeometryType GeometryType;
        public int Count { get => vertices.Count; }

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

        public void Draw(IRenderTarget target)
        {

        }

        #endregion

        private void CreateResources(int capacity)
        {
            
        }
    }
}
