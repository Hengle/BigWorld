using BigWorld.GUI.Layout;
using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI
{
    public class Label : Control
    {
        public string Text { get; set; }

        public Color TextColor { get; set; } = Color.White;

        public HorizontalAlignment HorizontalTextAlignment { get; set; }

        public VerticalAlignment VerticalTextAlignment { get; set; }

        public SpriteFont Font { get; set; }

        public Label()
        {
            //Padding = new Border(10);
        }

        public Label(string text) : this()
        {
            Text = text;
        }

        protected override void OnLoadContent(Game game)
        {
            base.OnLoadContent(game);

            if (Font == null)
                Font = game.Content.Load<SpriteFont>("Fonts\\GameFont");
        }

        public override Size GetActualSize(int? availableWidth = null, int? availableHeight = null)
        {
            var size = base.GetActualSize(availableWidth, availableHeight);

            if(size.Height == 0 && Font != null && Text != null)
            {
                size.Height = (int)Font.MeasureString(Text).Y + Padding.Vertical;
            }

            if(size.Width == 0 && Font != null && Text != null)
            {
                size.Width = (int)Font.MeasureString(Text).X + Padding.Horizontal;
            }

            return size;
        }

        protected override void OnDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            base.OnDraw(batch, clientSize, gameTime);

            if (Font == null || Text == null)
                return;

            var offsetX = 0;
            var offsetY = 0;

            var textSize = Font.MeasureString(Text);

            switch(HorizontalTextAlignment)
            {
                case HorizontalAlignment.Left:
                    offsetX = 0 + Padding.Left;
                    break;
                case HorizontalAlignment.Right:
                    offsetX = clientSize.Width - (int)textSize.X - Padding.Right;
                    break;
                default:
                    offsetX = clientSize.Width / 2 - (int)textSize.X / 2 - Padding.Horizontal / 2;
                    break;
            }

            switch(VerticalTextAlignment)
            {
                case VerticalAlignment.Top:
                    offsetY = 0 + Padding.Top;
                    break;
                case VerticalAlignment.Bottom:
                    offsetY = clientSize.Height - (int)textSize.Y - Padding.Bottom;
                    break;
                default:
                    offsetY = clientSize.Height / 2 - (int)textSize.Y / 2 - Padding.Vertical / 2;
                    break;
            }

            batch.DrawString(Font, Text, new Vector2(offsetX, offsetY), TextColor);
        }
    }
}
