using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;
using BigWorld.GUI.Layout;

namespace BigWorld.GUI
{
    public class Label : Control
    {
        public string Text { get; set; } = "";

        public HorizontalAlignment HorizontalTextAlignment { get; set; }
        public VerticalAlignment VerticalTextAlignment { get; set; }

        public Color TextColor { get; set; } = Color.White;

        public SpriteFont Font = null;

        public override void LoadContent(Game game)
        {
            base.LoadContent(game);

            if (Font == null)
                Font = game.Content.Load<SpriteFont>("Fonts\\GameFont");
        }

        protected override void OnDraw(SpriteBatch batch, Rectangle destinationRectangle, float alpha)
        {
            base.OnDraw(batch, destinationRectangle, alpha);

            if (Font != null && !String.IsNullOrEmpty(Text))
            {
                var textSize = Font.MeasureString(Text);

                var destX = destinationRectangle.X;
                var destY = destinationRectangle.Y;

                switch(HorizontalTextAlignment)
                {
                    case HorizontalAlignment.Left:
                        destX = Padding.Left;
                        break;
                    case HorizontalAlignment.Right:
                        destX = destinationRectangle.X + destinationRectangle.Width - (int)textSize.X - Padding.Right;
                        break;
                    default:
                    case HorizontalAlignment.Center:
                        destX = destinationRectangle.X + (destinationRectangle.Width - (int)textSize.X - Padding.Horizontal) / 2;
                        break;
                }

                switch(VerticalTextAlignment)
                {
                    case VerticalAlignment.Top:
                        destY = Padding.Top;
                        break;
                    case VerticalAlignment.Bottom:
                        destY = destinationRectangle.Y + destinationRectangle.Height - (int)textSize.Y - Padding.Bottom;
                        break;
                    default:
                    case VerticalAlignment.Center:
                        destY = destinationRectangle.Y + (destinationRectangle.Height - (int)textSize.Y - Padding.Vertical) / 2;
                        break;
                }

                batch.DrawString(Font, Text, new Vector2(destX, destY), TextColor);
            }
        }

        public override Point CalculateRequiredClientSpace()
        {
            var width = Width;
            var height = Height;

            var fontMeasurement = Font.MeasureString(Text);

            if (width == null && Text != null)
                width = (int)fontMeasurement.X + Padding.Horizontal;
            if (height == null && Text != null)
                height = (int)fontMeasurement.Y + Padding.Vertical;

            return new Point(width ?? 0, height ?? 0);
        }
    }
}
