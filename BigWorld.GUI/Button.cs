using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace BigWorld.GUI
{
    public class Button : Control
    {
        Label l;

        public string Text { get => l.Text; set => l.Text = value; }

        public Button(string text)
        {
            l = new Label() { Text = text, HorizontalAlignment = Layout.HorizontalAlignment.Stretch, VerticalAlignment = Layout.VerticalAlignment.Stretch};
            Children.Add(l);

            BackgroundColor = Color.LightGray * 0.5f;
            HoveredBackgroundColor = Color.Red * 0.5f;
            PressedBackgroundColor = Color.Red;
        }

        protected override void OnMouseDown(Point mousePosition)
        {
            base.OnMouseUp(mousePosition);
            if (ClientRectangle.Contains(mousePosition))
                ButtonClick?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ButtonClick;
    }
}
