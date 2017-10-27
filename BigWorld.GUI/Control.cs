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
        /// <summary>
        /// The full relative client rectangle (excluding margins & layouting, not clipped)
        /// </summary>
        public Rectangle ClientRectangle { get; set; } = Rectangle.Empty;

        /// <summary>
        /// The relative client rectangle as it is calculated (including margins & layouting, not clipped)
        /// </summary>
        public Rectangle ActualClientRectangle { get; set; } = Rectangle.Empty;

        /// <summary>
        /// The relative client rectangle as it is rendered (including margins & layouting, clipped)
        /// </summary>
        public Rectangle RenderedClientRectangle { get; set; } = Rectangle.Empty;

        public int Height
        {
            get => ClientRectangle.Height;
            set {
                if (ClientRectangle.Height == value)
                    return;

                ClientRectangle = new Rectangle(
                    ClientRectangle.Location.X,
                    ClientRectangle.Location.Y,
                    ClientRectangle.Width,
                    value);



                Invalidate();
            }
        }

        public int Width
        {
            get => ClientRectangle.Width;
            set
            {
                if (ClientRectangle.Width == value)
                    return;

                ClientRectangle = new Rectangle(
                    ClientRectangle.Location.X,
                    ClientRectangle.Location.Y, 
                    value,
                    ClientRectangle.Height);
                Invalidate();
            }
        }

        public Border Padding { get; set; } = new Border(0);

        public Border Margin { get; set; } = new Border(0);

        public ObservableCollection<Control> Children { get; private set; } = new ObservableCollection<Control>();

        public Color BackgroundColor { get; set; } = Color.Transparent;

        public Color? HoveredBackgroundColor { get; set; } = null;

        public Color? PressedBackgroundColor { get; set; } = null;

        protected Color ActiveBackgroundColor { get; set; }

        public Texture2D Pixel { get; private set; }

        public bool MouseHovered { get; private set; } = false;

        public bool MouseDown { get; private set; } = false;

        protected bool IsInvalid { get; private set; } = false;

        protected void Invalidate() => IsInvalid = true;

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public VerticalAlignment VerticalAlignment { get; set; }

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
            {
                PerformLayout();
                IsInvalid = false;
            }

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
                var positionX = child.ClientRectangle.X;
                var positionY = child.ClientRectangle.Y;
                var height = child.ClientRectangle.Height;
                var width = child.ClientRectangle.Width;

                switch (child.HorizontalAlignment)
                {
                    case HorizontalAlignment.Right:
                        positionX = contentArea.Width - child.ClientRectangle.Width - child.Margin.Right;
                        break;
                    case HorizontalAlignment.Left:
                        positionX = 0 + child.Margin.Left;
                        break;
                    case HorizontalAlignment.Stretch:
                        positionY = 0 + child.Margin.Left;
                        width = contentArea.Width - child.Margin.Horizontal;
                        break;
                    default:
                    case HorizontalAlignment.Center:
                        positionX = (contentArea.Width - child.ClientRectangle.Width - child.Margin.Horizontal) / 2;
                        break;
                }

                switch(child.VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        positionY = 0 + child.Margin.Top;
                        break;
                    case VerticalAlignment.Bottom:
                        positionY = contentArea.Height - child.ClientRectangle.Height - child.Margin.Bottom;
                        break;
                    case VerticalAlignment.Stretch:
                        positionY = 0 + child.Margin.Top;
                        height = contentArea.Height - child.Margin.Vertical;
                        break;
                    default:
                    case VerticalAlignment.Center:
                        positionY = (contentArea.Height - child.ClientRectangle.Height - child.Margin.Vertical) / 2;
                        break;
                }

                child.ActualClientRectangle = new Rectangle(positionX, positionY, width, height);

                child.PerformLayout();
            }
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

        #region Internal Mouse Events
        internal virtual void InternalMouseEnter(Point mousePosition)
        {
            MouseHovered = true;
            OnMouseEnter(mousePosition);

            foreach (var child in Children)
            {
                var relativeMousePosition = new Point(mousePosition.X - child.ActualClientRectangle.X,
                    mousePosition.Y - child.ActualClientRectangle.Y);

                if (relativeMousePosition.X > 0 && relativeMousePosition.Y > 0 &&
                    relativeMousePosition.X < child.ActualClientRectangle.Width &&
                    relativeMousePosition.Y < child.ActualClientRectangle.Height && !child.MouseHovered)
                    child.InternalMouseEnter(relativeMousePosition);
            }
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
            foreach (var child in Children)
            {
                var relativeMousePosition = new Point(mousePosition.X - child.ActualClientRectangle.X, 
                    mousePosition.Y - child.ActualClientRectangle.Y);

                if (child.ClientRectangle.Contains(mousePosition.X, mousePosition.Y) && !child.MouseHovered)
                    child.InternalMouseEnter(mousePosition);
                else if (!child.ClientRectangle.Contains(mousePosition.X, mousePosition.Y) && child.MouseHovered)
                    child.InternalMouseLeave(mousePosition);

                if (child.MouseHovered)
                    child.InternalMouseMove(mousePosition);

            }
        }

        internal virtual void InternalMouseUp(Point mousePosition)
        {
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

        #region Protected Methods
        protected virtual void OnMouseLeave(Point mousePosition){ }

        protected virtual void OnMouseEnter(Point mousePosition){}

        protected virtual void OnMouseUp(Point mousePosition) { }

        protected virtual void OnMouseDown(Point mousePosition) { }

        protected virtual void OnDraw(SpriteBatch batch, Rectangle controlArea, float alpha)
        {
            if (MouseHovered && HoveredBackgroundColor != null && ActiveBackgroundColor != HoveredBackgroundColor)
                ActiveBackgroundColor = (Color)HoveredBackgroundColor;
            else if (MouseDown && PressedBackgroundColor != null && ActiveBackgroundColor != PressedBackgroundColor)
                ActiveBackgroundColor = (Color)PressedBackgroundColor;
            else if (ActiveBackgroundColor != BackgroundColor)
                ActiveBackgroundColor = BackgroundColor;

            batch.Draw(Pixel, controlArea,ActiveBackgroundColor);
        }

        protected virtual void OnUpdate(GameTime gameTime) { }
        #endregion
    }
}