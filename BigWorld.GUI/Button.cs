using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigWorld.GUI.Args;
using engenious;

namespace BigWorld.GUI
{
    public class Button : ContentControl
    {
        public Button()
        {
            BackgroundColor = Color.White * 0.5f;
            HoveredBackgroundColor = Color.White * 0.7f;
            PressedBackgroundColor = Color.White * 0.9f;
        }

        public Button(string text): this()
        {
            Content = new Label(text);
            Content.HorizontalAlignment = Layout.HorizontalAlignment.Stretch;
            Content.VerticalAlignment = Layout.VerticalAlignment.Stretch;
        }

    }
}
