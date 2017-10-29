using BigWorld.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using BigWorldGame.Graphics;
using engenious.Graphics;

namespace BigWorldGame.GUI
{
    public class TileSelectControl : Control
    {
        public int ColumnCount { get; set; }

        private int internalColumnCount = 0;

        private int internalRowCount = 0;

        public int SizeFactor { get; set; } = 4;

        public Spritesheet SpriteSheet { get; set; }

        protected override void OnLoadContent(Game game)
        {
            base.OnLoadContent(game);
        }

        protected override void OnDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            base.OnDraw(batch, clientSize, gameTime);

            int i = 1;

            for(int y = 0; y < internalRowCount; y++)
            {
                for (int x = 0; x < internalColumnCount; x++)
                {
                    var destination = new Rectangle(x * SpriteSheet.TileWidth * SizeFactor, y * SpriteSheet.TileHeight * SizeFactor, SpriteSheet.TileWidth * SizeFactor, SpriteSheet.TileHeight * SizeFactor);

                    batch.Draw(SpriteSheet.TextureArray[i], destination, Color.White);

                    i++;
                }
            }
        }

        public override ControlSize GetActualSize(ControlSize controlSize)
        {
            var size = base.GetActualSize(controlSize);

            if (size.Width != null)
                internalColumnCount = (size.Width.Value-Padding.Horizontal) / (SpriteSheet.TileWidth * SizeFactor);
            else
            {
                internalColumnCount = ColumnCount;
                size.Width = internalColumnCount * SpriteSheet.TileWidth * SizeFactor + Padding.Horizontal;
            }

            if(size.Height == null)
            {
                internalRowCount = (SpriteSheet.Textures.Height * SpriteSheet.Textures.Height / internalColumnCount) +
                    (SpriteSheet.Textures.Height * SpriteSheet.Textures.Width % internalColumnCount != 0 ? 1 : 0);

                size.Height = internalRowCount * SpriteSheet.TileHeight * SizeFactor
                    + Padding.Vertical;
            }

            return size;
        }
    }
}
