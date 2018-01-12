using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlainCore.Graphics.Resources
{
    public class ShaderStreamResource : IShaderResource
    {
        public ShaderStreamResource(Stream stream)
        {
            source = stream;
        }

        private readonly Stream source;

        public byte[] LoadBytes()
        {
            var memStream = new MemoryStream();
            source.CopyTo(memStream);
            return memStream.ToArray();
        }
    }
}
