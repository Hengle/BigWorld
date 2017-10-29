using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;
using BigWorld.GUI.Layout;

namespace BigWorld.GUI
{
    public class StackPanel : ContainerControl
    {
        public int ItemSpacing { get; set; } = 0;

        protected override void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, ControlSize availableSize, GameTime gameTime)
        {
            var offset = 0;
            foreach(var child in Children)
            {
                var size = child.GetActualSize(new ControlSize(availableSize.Width, null));

                var offsetX = LayoutHelper.CalculateXOffset(child.HorizontalAlignment, availableSize, size);

                var childTransform = transform * Matrix.CreateTranslation(offsetX, offset, 0);

                var childRender = renderMask.Intersection(new Rectangle(new Point(0, 0), size).Transform(childTransform));

                child.Draw(batch, childTransform , childRender, size,gameTime);
                offset += (size.Height ?? 0) + ItemSpacing;
            }
        }

        public override ControlSize GetActualSize(ControlSize controlSize)
        {
            var estimatedSize = base.GetActualSize(controlSize);

            if(!estimatedSize.Height.HasValue || !estimatedSize.Width.HasValue)
            {
                List<ControlSize> childSizes = new List<ControlSize>();

                foreach (var child in Children)
                {
                    childSizes.Add( child.GetActualSize(new ControlSize(controlSize.Width, null)));
                }

                if (!estimatedSize.Height.HasValue)
                {
                    int sum = 0;

                    foreach (var size in childSizes)
                        sum += size.Height ?? 0;

                    sum += ItemSpacing * (Children.Count - 1);

                    estimatedSize.Height = sum + Padding.Vertical;

                    if (sum + Padding.Vertical == 0)
                        estimatedSize.Height = null;
                }

                if(!estimatedSize.Width.HasValue)
                {
                    var maxWidth = 0;

                    foreach(var size in childSizes)
                    {
                        //Check if != availableWidth is to prevent child "stretch" if no width is set.
                        if (size.Width > maxWidth && size.Width != controlSize.Width)
                            maxWidth = size.Width ?? 0;
                    }

                    estimatedSize.Width = maxWidth + Padding.Horizontal;

                    if (maxWidth + Padding.Horizontal== 0)
                        estimatedSize.Width = null;
                }
            }
            return estimatedSize;
        }
    }
}
