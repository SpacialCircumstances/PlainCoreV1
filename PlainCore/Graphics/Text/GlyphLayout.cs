using System;

namespace PlainCore.Graphics.Text
{
    public struct GlyphLayout
    {
        public GlyphLayout(string character, (int x, int y) bitmapPosition, (int w, int h) size)
        {
            Character = character;
            BitmapPosition = bitmapPosition;
            GlyphSize = size;
        }

        public string Character { get; set; }
        public (int X, int Y) BitmapPosition { get; set; }
        public (int W, int H) GlyphSize { get; set; }
    }
}
