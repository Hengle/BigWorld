using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious.Graphics;

namespace BigWorld.GUI.Brushes
{
    public class BorderBrush : Brush
    {
        public Color BackgroundColor { get; set; }

        public Color BorderColor { get; set; }

        public int BorderThickness { get; set; }

        public BorderBrush(Color backgroundColor)
        {
            BackgroundColor = backgroundColor;
            BorderColor = backgroundColor;
            BorderThickness = 0;
        }

        public BorderBrush(Color borderColor, int borderThickness, Color backgroundColor)
        {
            BorderColor = borderColor;
            BorderThickness = borderThickness;
            BackgroundColor = backgroundColor;
        }

        public override void Draw(SpriteBatch batch, Size size)
        {
            if(BorderThickness > 0)
            {
                batch.Draw(GuiRenderer.Pixel, new Rectangle(new Point(0, 0), size), BorderColor);
            }

            batch.Draw(GuiRenderer.Pixel, new Rectangle(BorderThickness, BorderThickness, size.Width - BorderThickness * 2, size.Height - BorderThickness * 1), BackgroundColor);
        }
    }
}
