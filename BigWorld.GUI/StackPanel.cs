using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace BigWorld.GUI
{
    public class StackPanel : ContainerControl
    {
        public int ItemSpacing { get; set; } = 0;

        protected override void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, GameTime gameTime)
        {
            var offset = 0;
            foreach(var child in Children)
            {
                var size = child.GetActualSize(renderMask.Width);

                var offsetX = 0;

                switch(child.HorizontalAlignment)
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

                var childTransform = transform * Matrix.CreateTranslation(offsetX, offset, 0);

                var childRender = renderMask.Intersection(new Rectangle(new Point(0, 0), size).Transform(childTransform));

                child.Draw(batch, childTransform , childRender, gameTime);
                offset += size.Height + ItemSpacing;
            }
        }

        public override Size GetActualSize(int? availableWidth = null, int? availableHeight = null)
        {
            var estimatedSize = base.GetActualSize(availableWidth, availableHeight);

            if(estimatedSize.Height == 0)
            {
                int sum = 0;

                foreach(var child in Children)
                {
                    sum += child.GetActualSize(availableWidth - Padding.Horizontal, null).Height;
                }

                sum += ItemSpacing * (Children.Count - 1);

                estimatedSize.Height = sum + Padding.Vertical;
            }

            return estimatedSize;
        }
    }
}
