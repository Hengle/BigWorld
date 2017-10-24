using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace BigWorldGame.Controls
{
    public class Button : Control
    {
        Label l;

        public string Text { get => l.Text; set => l.Text = value; }

        public Button(string text)
        {
            l = new Label() { Text = text};
            l.ClientRectangle = new Rectangle(10,10,100,100);
            Children.Add(l);
        }

        protected override void OnMouseEnter(Point mousePosition)
        {
            base.OnMouseEnter(mousePosition);

            BackgroundColor = Color.Blue;
        }

        protected override void OnMouseLeave(Point mousePosition)
        {
            base.OnMouseLeave(mousePosition);

            BackgroundColor = Color.Red;
        }

        protected override void OnMouseDown(Point mousePosition)
        {
            base.OnMouseUp(mousePosition);
            BackgroundColor = Color.Yellow;

            if (ClientRectangle.Contains(mousePosition))
                ButtonClick?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseUp(Point mousePosition)
        {
            base.OnMouseUp(mousePosition);

            BackgroundColor = Color.Plum;
        }

        public event EventHandler ButtonClick;
    }
}
