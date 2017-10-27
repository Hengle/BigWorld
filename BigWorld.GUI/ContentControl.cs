using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;
using engenious.Helper;

namespace BigWorld.GUI
{
    public class ContentControl : Control
    {
        public Control Content
        {
            get => ChildCollection.FirstOrDefault();
            set
            {
                ChildCollection.Clear();
                ChildCollection.Add(value);
            }
        }

        protected override void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, GameTime gameTime)
        {
            if (Content == null)
                return;

            var size = Content.GetActualSize(renderMask.Width, renderMask.Height);

            var offsetX = 0;

            switch (Content.HorizontalAlignment)
            {
                case Layout.HorizontalAlignment.Left:
                    break;
                case Layout.HorizontalAlignment.Right:
                    offsetX = renderMask.Width - size.Width;
                    break;
                case Layout.HorizontalAlignment.Center:
                default:
                    offsetX = renderMask.Width / 2 - size.Width / 2;
                    break;
            }

            var offsetY = 0;

            switch(Content.VerticalAlignment)
            {
                case Layout.VerticalAlignment.Top:
                    break;
                case Layout.VerticalAlignment.Bottom:
                    offsetY = renderMask.Height - size.Height;
                    break;
                case Layout.VerticalAlignment.Center:
                default:
                    offsetY = renderMask.Height / 2 - size.Height / 2;
                    break;
            }

            var childTransform = transform * Matrix.CreateTranslation(offsetX, offsetY, 0);
            var childRenderMask = renderMask.Intersection(new Rectangle(new Point(0,0), size)).Transform(childTransform);

            Content.Draw(batch, childTransform, childRenderMask, gameTime);
        }
    }
}
