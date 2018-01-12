using System;
using System.Collections.Generic;
using System.Text;

namespace PlainCore.Graphics.Resources
{
    public class ShaderMemoryResource: IShaderResource
    {
        public ShaderMemoryResource(byte[] bytes)
        {
            data = bytes;
        }

        private readonly byte[] data;

        public byte[] LoadBytes()
        {
            return data;
        }
    }
}
