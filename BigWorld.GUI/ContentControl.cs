using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;
using engenious.Helper;
using BigWorld.GUI.Layout;

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

        protected override void OnDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            base.OnDraw(batch, clientSize, gameTime);
        }

        protected override void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, ControlSize availableSize, GameTime gameTime)
        {
            if (Content == null)
                return;

            //Get the real size of the content
            var size = Content.GetActualSize(availableSize);

            //Calculate the horizontal offset based on HorizontalAlignment
            var offsetX = LayoutHelper.CalculateXOffset(Content.HorizontalAlignment, availableSize, size);

            //Calculate the vertical offset based on VerticalAlignment
            var offsetY = LayoutHelper.CalculateYOffset(Content.VerticalAlignment, availableSize, size);

            //Create the child-transform based on our transform and the offset
            var childTransform = transform * Matrix.CreateTranslation(offsetX, offsetY, 0);

            //Calculate the renderMask for the child
            var childRenderMask = renderMask.Intersection(new Rectangle(new Point(0, 0), size).Transform(childTransform));

            Content.Draw(batch, childTransform, childRenderMask, size, gameTime);
        }

        public override ControlSize GetActualSize(ControlSize availableSize)
        {
            var size = base.GetActualSize(availableSize);

            if(Content != null && (!size.Width.HasValue || !size.Height.HasValue))
            {
                var contentSize = Content.GetActualSize(size);

                if (!size.Height.HasValue)
                    size.Height = contentSize.Height;

                if (!size.Width.HasValue)
                    size.Width = contentSize.Width;
            }

            return size;
        }
    }
}
