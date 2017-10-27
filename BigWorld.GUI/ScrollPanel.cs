using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;
using engenious.Input;

namespace BigWorld.GUI
{
    public class ScrollPanel : Control
    {
        new protected ObservableCollection<Control> Children { get; set; } = new ObservableCollection<Control>();

        public Control Content
        {
            get => Children.FirstOrDefault();
            set
            {
                if (Children.FirstOrDefault() == value)
                    return;

                Children.Clear();
                Children.Add(value);
            }
        }

        public int ScrollPositionX
        {
            get => scrollPositionX;
            set
            {
                if (scrollPositionX == value)
                    return;

                scrollPositionX = value;
                RecalculateScrolling();
            }
        }

        private int scrollPositionX;

        public int ScrollPositionY
        {
            get => scrollPositionY;
            set
            {
                if (scrollPositionY == value)
                    return;

                scrollPositionY = value;

                RecalculateScrolling();
            }
        }

        private int scrollPositionY;

        private int? originalPosX = null;
        private int? originalPosY = null;

        private void RecalculateScrolling()
        {
            if (Content == null)
                return;

            if (originalPosX == null)
                originalPosX = Content.ActualClientRectangle.X;
            if (originalPosY == null)
                originalPosY = Content.ActualClientRectangle.Y;

            var posX = (int)originalPosX - ScrollPositionX;
            var posY = (int)originalPosY- ScrollPositionY;

            Content.ActualClientRectangle = new Rectangle(new Point(posX, posY), Content.ActualClientRectangle.Size);
        }

        public override void PerformLayout()
        {
            var contentSize = Content.CalculateRequiredClientSpace();

            var contentPosition = new Rectangle(0 + Padding.Left, 0 + Padding.Top,
                contentSize.X, contentSize.Y);

            Content.ActualClientRectangle = contentPosition;


        }

        public override void PreDraw(SpriteBatch batch, Rectangle parentArea, float alpha)
        {
            batch.GraphicsDevice.ScissorRectangle = RenderedClientRectangle;
        }

        protected override void OnDraw(SpriteBatch batch, Rectangle controlArea, float alpha)
        {
            base.OnDraw(batch, controlArea, alpha);

            
        }

        protected override void OnMouseWheel(int wheelPosition, int delta)
        {
            base.OnMouseWheel(wheelPosition, delta * 4);

            ScrollPositionY -= delta;
        }
    }
}
