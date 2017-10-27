using BigWorld.GUI.Layout;
using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI
{
    public class Control
    {
        public int? Width { get; set; }
        public int? Height { get; set; }

        public Point Position { get; set; } = Point.Zero;

        public Color BackgroundColor { get; set; } = Color.Transparent;

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public VerticalAlignment VerticalAlignment { get; set; } 

        protected readonly List<Control> ChildCollection = new List<Control>();

        public virtual Size GetActualSize(int? availableWidth = null, int? availableHeight = null )
        {
            var width = Width;
            var height = Height;

            if (!width.HasValue && availableWidth == 0)
                width = 0;
            else if (!width.HasValue && HorizontalAlignment == HorizontalAlignment.Stretch)
                width = availableWidth;

            if (!height.HasValue && availableHeight == 0)
                height = 0;
            else if (!height.HasValue && VerticalAlignment == VerticalAlignment.Stretch)
                height = availableHeight;

            return new Size(width ?? 0, height ?? 0);
        }

        public RasterizerState RasterizerState { get; set; } = new RasterizerState()
        {
            ScissorTestEnable = true,
        };

        public void Draw(SpriteBatch batch, Matrix transform, Rectangle renderMask, GameTime gameTime)
        {
            var clientSize = GetActualSize(renderMask.Width, renderMask.Height);

            transform *= Matrix.CreateTranslation(Position.X, Position.Y, 0);

            Rectangle clientRectangle = new Rectangle(0, 0, clientSize.Width, clientSize.Height);
            clientRectangle = clientRectangle.Transform(transform);

            Rectangle renderRec = renderMask.Intersection(clientRectangle);

            batch.GraphicsDevice.ScissorRectangle = renderRec;

            batch.Begin(rasterizerState:RasterizerState,transformMatrix: transform);
            OnDraw(batch,clientSize, gameTime);
            batch.End();

            OnDrawChildren(batch, transform, renderRec, gameTime);
        }

        protected virtual void OnDrawBackground(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            batch.Draw(GuiRenderer.Pixel, new Rectangle(0, 0, clientSize.Width, clientSize.Height), BackgroundColor);
        }

        protected virtual void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, GameTime gameTime)
        {
            foreach(var child in ChildCollection)
            {
                child.Draw(batch, transform, renderMask, gameTime);
            }
        }

        protected virtual void OnDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            OnDrawBackground(batch, clientSize, gameTime);
        }
    }
}
