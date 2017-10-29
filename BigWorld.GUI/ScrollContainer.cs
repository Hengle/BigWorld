using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigWorld.GUI.Args;
using engenious;
using engenious.Graphics;
using engenious.Input;
using BigWorld.GUI.Layout;

namespace BigWorld.GUI
{
    public class ScrollContainer : ContentControl
    {
        public int ScrollPositionX {
            get => scrollPositionX;
            set
            {
                if (scrollPositionX == value)
                    return;

                if(value < 0)
                {
                    scrollPositionX = 0;
                    return;
                }

                if(value > scrollHeight - RenderedClientRectangle.Height)
                {
                    scrollPositionX = scrollHeight - RenderedClientRectangle.Height;
                    return;
                }

                scrollPositionX = value;
            }
        }

        private int scrollPositionX = 0;

        private bool needsScrollingX = false;

        private int scrollHeight = 0;

        const int MIN_SCROLLER_SIZE = 10;

        public Rectangle ThumbAreaX { get; private set; }

        protected override void OnDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            base.OnDraw(batch, clientSize, gameTime);

            if (scrollHeight == 0)
                return;

            float scrollRatio = (float)clientSize.Height / scrollHeight;
            int scrollerHeight = (int)(scrollRatio * clientSize.Height);

            int scrollerPosition = (int)(((float)ScrollPositionX / (scrollHeight - clientSize.Height)) * (clientSize.Height-scrollerHeight));

            var scrollRectangle = new Rectangle(clientSize.Width - 10, scrollerPosition, 10, scrollerHeight);

            ThumbAreaX = scrollRectangle;

            batch.Draw(GuiRenderer.Pixel, ThumbAreaX, Color.White);

        }

        protected override void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, ControlSize availableSize, GameTime gameTime)
        {
            if (Content == null)
                return;

            availableSize.Width = availableSize.Width - 10;

            var childSize = Content.GetActualSize(new ControlSize(availableSize.Width, null));

            if(childSize.Height > renderMask.Height)
            {
                needsScrollingX = true;
                scrollHeight = childSize.Height ?? 0;
            }
            else
            {
                needsScrollingX = false;
            }

            var xOffset = LayoutHelper.CalculateXOffset(Content.HorizontalAlignment, availableSize, childSize);

            var childTransform = transform * Matrix.CreateTranslation(xOffset, -ScrollPositionX, 0);
            //var childRect = new Rectangle(0, 0, childSize.Width, childSize.Height).Transform(childTransform);

            Content.Draw(batch, childTransform, renderMask, childSize, gameTime);
        }

        protected override bool OnMouseScroll(MouseEventArgs mouseEventArgs, int scrollDelta)
        {
            ScrollPositionX += scrollDelta * 4;

            return true;
        }

        bool grabbing = false;
        int thumbOffset = 0;

        protected override bool OnMouseButtonDown(MouseEventArgs mouseEventArgs, Point relativePosition, MouseButton button)
        {
            if (button == MouseButton.Left && ThumbAreaX.Intersects(relativePosition))
            {
                grabbing = true;
                thumbOffset = relativePosition.Y - ThumbAreaX.Y;
            }
            return true;
        }

        protected override bool OnMouseButtonUp(MouseEventArgs mouseEventArgs, Point relativePosition, MouseButton button)
        {
            if (button == MouseButton.Left)
                grabbing = false;

            return true;
        }

        protected override void OnMouseMove(MouseEventArgs mouseEventArgs, Point relativePosition)
        {
            if(grabbing)
            {
                //ScrollPositionX = (scrollHeight / ClientRectangle.Height) * relativePosition.X - thumbOffset;
            }
        }
    }
}
