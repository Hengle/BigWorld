using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigWorld.GUI.Args;
using engenious;
using engenious.Graphics;

namespace BigWorld.GUI
{
    public class ScrollContainer : ContentControl
    {
        public int ScrollPositionX { get; set; }

        private bool needsScrollingX = false;

        private int scrollHeight = 0;

        const int MIN_SCROLLER_SIZE = 10;

        protected override void OnDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            base.OnDraw(batch, clientSize, gameTime);

            if (scrollHeight == 0)
                return;

            float scrollRatio = (float)clientSize.Height / scrollHeight;
            var scrollRectangle = new Rectangle(clientSize.Width - 10, (int)(ScrollPositionX * scrollRatio), 10, (int)Math.Round(scrollRatio * clientSize.Width));
            batch.Draw(GuiRenderer.Pixel, scrollRectangle, Color.White);

        }

        protected override void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, GameTime gameTime)
        {
            if (Content == null)
                return;

            var childSize = Content.GetActualSize(null, null);

            if(childSize.Height > renderMask.Height)
            {
                needsScrollingX = true;
                scrollHeight = childSize.Height;

                var childTransform = transform * Matrix.CreateTranslation(0, -ScrollPositionX, 0);

                var childRect = new Rectangle(0, 0, childSize.Width-10, childSize.Height).Transform(childTransform);

                Content.Draw(batch, childTransform, childRect, gameTime);
            }
            else
            {
                needsScrollingX = false;
            }
        }

        public override Size GetActualSize(int? availableWidth = null, int? availableHeight = null)
        {
            return base.GetActualSize(availableWidth, availableHeight);
        }

        protected override bool OnMouseScroll(MouseEventArgs mouseEventArgs, int scrollDelta)
        {
            ScrollPositionX += scrollDelta * 4;

            return true;
        }
    }
}
