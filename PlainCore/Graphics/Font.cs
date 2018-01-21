using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.Fonts;

namespace PlainCore.Graphics
{
    public class Font
    {
        protected Font()
        {

        }

        public static Font CreateFromFile(string filename, string family, float size)
        {
            var font = new Font();

            var fc = new FontCollection();
            fc.Install(filename);
            font.internalFont = fc.CreateFont(family, size);

            return font;
        }

        protected SixLabors.Fonts.Font internalFont;
        
        public void Draw(SpriteBatch batch, string text, int x, int y)
        {

        }
    }
}
