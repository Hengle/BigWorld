using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace BigWorld.GUI.Controls
{
    public class ProgressBar : Control
    {
        public int MaxValue { get; set; }

        public int Value { get; set; }

        public int AnimationSpeed { get; set; } = 1;

        public bool Animated { get; set; }

        private int currentWidth = 0;

        protected override void OnDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            base.OnDraw(batch, clientSize, gameTime);

            var expectedWidth = (int)((clientSize.Width - Padding.Horizontal) /(float)MaxValue * Value);

            if (currentWidth != expectedWidth && Animated)
            {
                currentWidth += AnimationSpeed * gameTime.ElapsedGameTime.Milliseconds / 10;

                if (currentWidth > expectedWidth)
                    currentWidth = expectedWidth;
            }
            else if (currentWidth != expectedWidth)
                currentWidth = expectedWidth;

            var drawRect = new Rectangle(Padding.Left, Padding.Top, currentWidth, clientSize.Height - Padding.Vertical);

            batch.Draw(GuiRenderer.Pixel, drawRect, Color.White);
        }



    }
}
