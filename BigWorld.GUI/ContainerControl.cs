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
    public class ContainerControl : Control
    {
        public List<Control> Children => ChildCollection;

        protected override void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, ControlSize availableSize, GameTime gameTime)
        {
            foreach(var child in Children)
            {
                var childSize = child.GetActualSize(availableSize);

                var offsetX = LayoutHelper.CalculateXOffset(child.HorizontalAlignment, availableSize, childSize);

                var offsetY = LayoutHelper.CalculateYOffset(child.VerticalAlignment, availableSize, childSize);

                var childTransform = transform * Matrix.CreateTranslation(offsetX, offsetY, 0);

                var childRenderMask = renderMask.Intersection(new Rectangle(new Point(0, 0), childSize).Transform(childTransform));

                child.Draw(batch, childTransform, childRenderMask, childRenderMask.Size, gameTime);
            }
        }

    }
}
