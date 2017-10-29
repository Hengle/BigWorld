using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace BigWorld.GUI.Controls
{
    public class ColorPicker : Control
    {

        protected override void OnDrawBackground(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            for(int x = 0; x < clientSize.Width; x++)
            {
                for(int y = 0; y < clientSize.Height; y++)
                {
                    batch.Draw(GuiRenderer.Pixel, new Rectangle(x, y, 1, 1), new Color((float)(clientSize.Width / 255) * x,
                        (float)(clientSize.Height / 255) * y, (float)((clientSize.Height / 255) * x + (clientSize.Height / 255) * y)));
                }
            }
        }

    }
}
