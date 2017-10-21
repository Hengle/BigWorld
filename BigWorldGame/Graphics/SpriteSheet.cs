using engenious.Content;
using engenious.Graphics;

namespace BigWorldGame.Graphics
{
    public class Spritesheet
    {
        public Texture2DArray Textures { get; }
        public int Width { get; }
        public int Height { get; }

        public Spritesheet(GraphicsDevice graphicsDevice, ContentManager content, string assetName, int tileWidth,
            int tileHeight, int tileSpacing = 1)
        {
            var text = content.Load<Texture2D>(assetName);

            var data = new uint[text.Width * text.Height];
            text.GetData(data);

            Width = (text.Width + tileSpacing) / (tileWidth + tileSpacing);
            Height = (text.Height + tileSpacing) / (tileHeight + tileSpacing);

            int tileCount = Width * Height;

            Textures = new Texture2DArray(graphicsDevice, 1, tileWidth, tileHeight, tileCount + 1);
            var tileData = new uint[tileWidth * tileHeight];
            Textures.SetData(tileData, 0);
            int yOffset = 0, xOffset = 0;
            for (int i = 0; i < tileCount; i++)
            {
                for (int y = 0; y < tileHeight; y++)
                {
                    int tileDataIndex = y * tileWidth;
                    int dataIndex = (y + yOffset) * text.Width + xOffset;
                    for (int x = 0; x < tileHeight; x++)
                    {
                        tileData[tileDataIndex++] = data[dataIndex++];
                    }
                }

                Textures.SetData(tileData, i + 1);

                xOffset += tileWidth + tileSpacing;
                if (xOffset + tileWidth > text.Width)
                {
                    xOffset = 0;
                    yOffset += tileHeight + tileSpacing;
                }
            }

        }

        public uint GetIndex(int x, int y)
        {
            return (uint) (y * Width + x + 1);
        }
    }
}