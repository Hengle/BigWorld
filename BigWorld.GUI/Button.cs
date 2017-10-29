using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigWorld.GUI.Args;
using engenious;
using engenious.Input;
using BigWorld.GUI.Brushes;

namespace BigWorld.GUI
{
    public class Button : ContentControl
    {
        public Button()
        {
            Background = new BorderBrush(Color.White * 0.5f);
            HoveredBackground = new BorderBrush(Color.White * 0.7f);
            PressedBackground = new BorderBrush(Color.White * 0.9f);
            DisabledBrush = new BorderBrush(Color.Black * 0.5f);
        }

        public Button(string text): this()
        {
            Content = new Label(text);
            Content.HorizontalAlignment = Layout.HorizontalAlignment.Stretch;
            Content.VerticalAlignment = Layout.VerticalAlignment.Stretch;
        }

        protected override bool OnMouseButtonUp(MouseEventArgs mouseEventArgs, Point relativePosition, MouseButton button)
        {
            if(Enabled)
                OnClick?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public event EventHandler OnClick;

    }
}
