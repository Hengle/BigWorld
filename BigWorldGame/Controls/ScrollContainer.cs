using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace BigWorldGame.Controls
{
    public class ScrollContainer : Control
    {
        new private List<Control> Children;

        public Control Content { get; set; }

        public int ScrollOffsetX { get; set; }
        public int ScrollOffsetY { get; set; }

        public override void Update()
        {
            base.Update();

        }

        public override void Draw(SpriteBatch batch, Rectangle destinationRectangle, float alpha)
        {
            
        }

        internal override void InternalMouseDown(Point mousePosition)
        {
            OnMouseDown(mousePosition);
            Content.InternalMouseDown(mousePosition);
        }

        internal override void InternalMouseEnter(Point mousePosition)
        {
            OnMouseEnter(mousePosition);
            Content.InternalMouseEnter(mousePosition);
        }

        internal override void InternalMouseLeave(Point mousePosition)
        {
            OnMouseLeave(mousePosition);
            Content.InternalMouseLeave(mousePosition);
        }

        internal override void InternalMouseMove(Point mousePosition)
        {
            Content.InternalMouseMove(mousePosition);
        }

        internal override void InternalMouseUp(Point mousePosition)
        {
            OnMouseUp(mousePosition);
            Content.InternalMouseUp(mousePosition);
        }
    }
}
