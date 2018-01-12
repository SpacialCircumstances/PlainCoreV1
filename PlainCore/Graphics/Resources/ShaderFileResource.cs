using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlainCore.Graphics.Resources
{
    public class ShaderFileResource : IShaderResource
    {
        public ShaderFileResource(string filename)
        {
            this.filename = filename;

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Shader file " + filename + " not found");
            }
        }

        private readonly string filename;

        public byte[] LoadBytes()
        {
            return File.ReadAllBytes(filename);
        }
    }
}
