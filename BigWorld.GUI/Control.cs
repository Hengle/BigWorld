using engenious;
using engenious.Graphics;
using System.Collections.Generic;
using System;
using BigWorld.GUI.Layout;
using System.Collections.ObjectModel;

namespace BigWorld.GUI
{
    public class Control
    {

        #region Properties
        /// <summary>
        /// The relative client rectangle as it is calculated (including margins & layouting, not clipped)
        /// </summary>
        public Rectangle ActualClientRectangle { get; set; } = Rectangle.Empty;

        /// <summary>
        /// The relative client rectangle as it is rendered (including margins & layouting, clipped)
        /// </summary>
        public Rectangle RenderedClientRectangle { get; set; } = Rectangle.Empty;

        public int? Height
        {
            get => height;
            set {
                if (height == value)
                    return;

                height = value;
                Invalidate();
            }
        }

        private int? height = null;

        public int? Width
        {
            get => width;
            set
            {
                if (width == value)
                    return;

                width = value;
                Invalidate();
            }
        }

        private int? width = null;

        public Border Padding { get; set; } = new Border(0);

        public Border Margin { get; set; } = new Border(0);

        public ObservableCollection<Control> Children { get; private set; } = new ObservableCollection<Control>();

        public Color BackgroundColor { get; set; } = Color.Transparent;

        public Color? HoveredBackgroundColor { get; set; } = null;

        public Color? PressedBackgroundColor { get; set; } = null;

        protected Color ActiveBackgroundColor
        {
            get => activeBackgroundColor;
            set
            {
                if (activeBackgroundColor == value)
                    return;

                activeBackgroundColor = value;
            }
        }

        private Color activeBackgroundColor = Color.Transparent;

        public Texture2D Pixel { get; private set; }

        public bool MouseHovered { get; private set; } = false;

        public bool MouseDown { get; private set; } = false;

        protected bool IsInvalid { get; private set; } = false;

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public VerticalAlignment VerticalAlignment { get; set; }

        #endregion

        protected void Invalidate()
        {
            IsInvalid = true;
            ControlInvalidated?.Invoke(this, null);
        }

        public Control()
        {
            Children.CollectionChanged += (s, e) => Invalidate();
        }

        public virtual void LoadContent(Game game)
        {
            Pixel = new Texture2D(game.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            foreach (var child in Children)
                child.LoadContent(game);
        }
        
        public void Update(GameTime gameTime)
        {
            if (IsInvalid)
                PerformLayout();

            OnUpdate(gameTime);

            foreach (var child in Children)
                child.Update(gameTime);
        }

        public virtual void PerformLayout()
        {
            var contentArea = new Rectangle(ActualClientRectangle.X + Padding.Left, ActualClientRectangle.Y + Padding.Top,
                ActualClientRectangle.Width - Padding.Horizontal, ActualClientRectangle.Height - Padding.Vertical);

            foreach(var child in Children)
            {
                var childSize = child.CalculateRequiredClientSpace();

                var positionX = child.Margin.Left;
                var positionY = child.Margin.Top;
                var height = childSize.Y;
                var width = childSize.X;

                switch (child.HorizontalAlignment)
                {
                    case HorizontalAlignment.Right:
                        positionX = contentArea.Width - width - child.Margin.Right;
                        break;
                    case HorizontalAlignment.Left:
                        positionX = 0 + child.Margin.Left;
                        break;
                    case HorizontalAlignment.Stretch:
                        positionX = 0 + child.Margin.Left;
                        width = contentArea.Width - child.Margin.Horizontal;
                        break;
                    case HorizontalAlignment.Center:
                    default:
                        positionX = contentArea.Width/2 - width/2 - child.Margin.Horizontal / 2;
                        break;
                }

                switch(child.VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        positionY = 0 + child.Margin.Top;
                        break;
                    case VerticalAlignment.Bottom:
                        positionY = contentArea.Height - height - child.Margin.Bottom;
                        break;
                    case VerticalAlignment.Stretch:
                        positionY = 0 + child.Margin.Top;
                        height = contentArea.Height - child.Margin.Vertical;
                        break;
                    case VerticalAlignment.Center:
                    default:
                        positionY = contentArea.Height/2 - height/2 - child.Margin.Vertical / 2;
                        break;
                }

                child.ActualClientRectangle = new Rectangle(positionX, positionY, width, height);

                child.PerformLayout();
            }

            IsInvalid = false;
        }

        public virtual Point CalculateRequiredClientSpace()
        {
            return new Point(Width ?? 0, Height ?? 0);
        }

        public void Draw(SpriteBatch batch, Rectangle parentArea, float alpha)
        {
            RenderedClientRectangle = new Rectangle(parentArea.X + ActualClientRectangle.X,
                parentArea.Y + ActualClientRectangle.Y, Math.Min(parentArea.Width, ActualClientRectangle.Width),
                Math.Min(parentArea.Height, ActualClientRectangle.Height));

            batch.GraphicsDevice.RasterizerState.ScissorTestEnable = true;
            batch.GraphicsDevice.ScissorRectangle = RenderedClientRectangle;

            batch.Begin();
            OnDraw(batch, RenderedClientRectangle, alpha);
            batch.End();

            var childRectangle = new Rectangle(RenderedClientRectangle.X + Padding.Left, RenderedClientRectangle.Y + Padding.Top,
                RenderedClientRectangle.Width - Padding.Horizontal, RenderedClientRectangle.Height - Padding.Vertical);

            foreach (var child in Children)
            {
                child.Draw(batch, childRectangle, alpha);
            }
        }

        protected virtual void OnDraw(SpriteBatch batch, Rectangle controlArea, float alpha)
        {
            if (MouseDown)
                ActiveBackgroundColor = PressedBackgroundColor ?? BackgroundColor;
            else if (MouseHovered)
                ActiveBackgroundColor = HoveredBackgroundColor ?? BackgroundColor;
            else
                ActiveBackgroundColor = BackgroundColor;

            batch.Draw(Pixel, controlArea, ActiveBackgroundColor);
        }

        #region Internal Mouse Methods
        internal virtual void InternalMouseEnter(Point mousePosition)
        {
            MouseHovered = true;
            OnMouseEnter(mousePosition);
        }

        internal virtual void InternalMouseLeave(Point mousePosition)
        {
            MouseHovered = false;
            OnMouseLeave(mousePosition);

            foreach (var child in Children)
            {
                if (child.MouseHovered)
                    child.InternalMouseLeave(mousePosition);
            }
        }

        internal void InternalMouseMove(Point mousePosition)
        {
            var relativeMousePosition = new Point(mousePosition.X - ActualClientRectangle.X, mousePosition.Y - ActualClientRectangle.Y);

            foreach (var child in Children)
            {
                if (child.ActualClientRectangle.Contains(relativeMousePosition.X, relativeMousePosition.Y) && !child.MouseHovered)
                    child.InternalMouseEnter(relativeMousePosition);
                else if (!child.ActualClientRectangle.Contains(relativeMousePosition.X, relativeMousePosition.Y) && child.MouseHovered)
                    child.InternalMouseLeave(relativeMousePosition);

                if (child.MouseHovered)
                    child.InternalMouseMove(relativeMousePosition);

            }
        }

        internal virtual void InternalMouseUp(Point mousePosition)
        {
            MouseDown = false;

            foreach(var child in Children)
            {
                if (child.MouseDown)
                    child.InternalMouseUp(mousePosition);
            }

            OnMouseUp(mousePosition);
        }

        internal virtual void InternalMouseDown(Point mousePosition)
        {
            MouseDown = true;

            foreach (var child in Children)
            {
                if (child.MouseHovered)
                    child.InternalMouseDown(mousePosition);
            }

            OnMouseDown(mousePosition);
        }
        #endregion

        #region Protected Virtual Methods
        protected virtual void OnMouseLeave(Point mousePosition){ }

        protected virtual void OnMouseEnter(Point mousePosition){}

        protected virtual void OnMouseUp(Point mousePosition){}

        protected virtual void OnMouseDown(Point mousePosition) { }

        protected virtual void OnUpdate(GameTime gameTime) { }
        #endregion

        public event EventHandler ControlInvalidated;
    }
}