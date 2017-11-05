using BigWorld.GUI;
using BigWorldGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;
using BigWorld.GUI.Args;
using engenious.Input;

namespace BigWorldGame.GUI
{
    public class TileSelectControl : Control
    {

        public Spritesheet SpriteSheet { get; set; }

        public int SizeMultiplicator { get; set; } = 2;

        public Vector2? SelectedTileIndex { get; private set; }

        private Point lastMousePosition;

        public TileSelectControl()
        {
        }

        protected override void OnDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            base.OnDraw(batch, clientSize, gameTime);

            var drawSize = new Size(
                Math.Min(clientSize.Width - Padding.Horizontal, SpriteSheet.Texture.Width * SizeMultiplicator),
                Math.Min(clientSize.Height - Padding.Vertical, SpriteSheet.Texture.Height * SizeMultiplicator));

            batch.Draw(SpriteSheet.Texture, new Rectangle(Padding.Left, Padding.Top, drawSize.Width, drawSize.Height), Color.White);

            var tileIndex = GetTileIndex(lastMousePosition);
            if (tileIndex != null)
            {

                var selectorRectangle = new Rectangle((int)tileIndex.Value.X * (SpriteSheet.TileSpacing + SpriteSheet.TileWidth) * SizeMultiplicator + Padding.Left,
                    (int)tileIndex.Value.Y * (SpriteSheet.TileSpacing + SpriteSheet.TileHeight) * SizeMultiplicator + Padding.Top,
                    SpriteSheet.TileWidth * SizeMultiplicator, SpriteSheet.TileHeight * SizeMultiplicator);
                batch.Draw(GuiRenderer.Pixel, selectorRectangle, Color.White * 0.5f);
            }

            if(SelectedTileIndex != null)
            {
                var selectorRectangle = new Rectangle((int)SelectedTileIndex.Value.X * (SpriteSheet.TileSpacing + SpriteSheet.TileWidth) * SizeMultiplicator + Padding.Left,
                   (int)SelectedTileIndex.Value.Y * (SpriteSheet.TileSpacing + SpriteSheet.TileHeight) * SizeMultiplicator + Padding.Top,
                   SpriteSheet.TileWidth * SizeMultiplicator, SpriteSheet.TileHeight * SizeMultiplicator);
                batch.Draw(GuiRenderer.Pixel, selectorRectangle, Color.Red * 0.5f);
            }
        }

        private Vector2? GetTileIndex(Point mousePosition)
        {
            var hoveredTileX = (lastMousePosition.X - Padding.Left) / ((SpriteSheet.TileWidth + SpriteSheet.TileSpacing) * SizeMultiplicator);
            var hoveredTileY = (lastMousePosition.Y - Padding.Top) / ((SpriteSheet.TileHeight + SpriteSheet.TileSpacing) * SizeMultiplicator);

            if (hoveredTileX >= 0 && hoveredTileX < SpriteSheet.Width && hoveredTileY >= 0 && hoveredTileY < SpriteSheet.Height)
            {
                return new Vector2(hoveredTileX, hoveredTileY);
            }

            return null;
        }

        public override ControlSize GetActualSize(ControlSize controlSize)
        {
            var size = base.GetActualSize(controlSize);

            if (!size.Height.HasValue && SpriteSheet != null)
                size.Height = SpriteSheet.Texture.Height * SizeMultiplicator;

            if (!size.Width.HasValue && SpriteSheet != null)
                size.Width = SpriteSheet.Texture.Width * SizeMultiplicator;

            return size;
        }

        protected override bool OnMouseButtonUp(MouseEventArgs mouseEventArgs, Point relativePosition, MouseButton button)
        {
            base.OnMouseButtonUp(mouseEventArgs, relativePosition, button);

            SelectedTileIndex = GetTileIndex(relativePosition);

            return true;
        }

        protected override void OnMouseMove(MouseEventArgs mouseEventArgs, Point relativePosition)
        {
            base.OnMouseMove(mouseEventArgs, relativePosition);

            lastMousePosition = relativePosition;
        }
    }
}
