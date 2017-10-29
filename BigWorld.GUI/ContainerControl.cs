using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace BigWorld.GUI
{
    public class ContainerControl : Control
    {
        public List<Control> Children => ChildCollection;

        protected override void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, GameTime gameTime)
        {
            foreach(var child in Children)
            {
                var childSize = child.GetActualSize(renderMask.Width, renderMask.Height);

                var offsetX = 0;

                switch (child.HorizontalAlignment)
                {
                    case Layout.HorizontalAlignment.Left:
                        break;
                    case Layout.HorizontalAlignment.Right:
                        offsetX = renderMask.Width - childSize.Width;
                        break;
                    case Layout.HorizontalAlignment.Center:
                    default:
                        offsetX = renderMask.Width / 2 - childSize.Width / 2;
                        break;
                }

                var offsetY = 0;

                switch (child.VerticalAlignment)
                {
                    case Layout.VerticalAlignment.Top:
                        break;
                    case Layout.VerticalAlignment.Bottom:
                        offsetY = renderMask.Height - childSize.Height;
                        break;
                    case Layout.VerticalAlignment.Center:
                    default:
                        offsetY = renderMask.Height / 2 - childSize.Height / 2;
                        break;
                }

                var childTransform = transform * Matrix.CreateTranslation(offsetX, offsetY, 0);

                var childRenderMask = renderMask.Intersection(new Rectangle(new Point(0, 0), childSize).Transform(childTransform));

                child.Draw(batch, childTransform, childRenderMask, gameTime);
            }
        }

    }
}
