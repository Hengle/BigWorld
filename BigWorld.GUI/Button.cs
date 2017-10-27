using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using BigWorld.GUI.Layout;

namespace BigWorld.GUI
{
    public class Button : Control
    {
        public Label Label { get; private set; }

        public string Text { get => Label.Text; set => Label.Text = value; }

        public Button(string text)
        {
            BackgroundColor = Color.White * 0.5f;
            HoveredBackgroundColor = Color.White * 0.7f;
            PressedBackgroundColor = Color.White * 0.9f;
           // Padding = new Border(20);

            Label = new Label()
            {
                Padding = new Border(20,30,20,30),
                BackgroundColor = Color.Transparent,
                Text = text,
                HorizontalAlignment = Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Layout.VerticalAlignment.Stretch
            };

            Children.Add(Label);
        }

        protected override void OnMouseDown(Point mousePosition)
        {
            base.OnMouseDown(mousePosition);

            Label.TextColor = Color.Black;
        }

        protected override void OnMouseUp(Point mousePosition)
        {
            base.OnMouseUp(mousePosition);

            Label.TextColor = Color.White;

            if (ActualClientRectangle.Contains(mousePosition))
                ButtonClick?.Invoke(this, EventArgs.Empty);
        }

        public override Point CalculateRequiredClientSpace()
        {
            var height = Height;
            var width = Width;

            var labelSpace = Label.CalculateRequiredClientSpace();

            if (width == null)
                width = labelSpace.X + Padding.Horizontal;
            if (height == null)
                height = labelSpace.Y + Padding.Vertical;

            return new Point(width ?? 0, height ?? 0);
        }

        public event EventHandler ButtonClick;
    }
}
