using engenious;
using engenious.Graphics;
using System.Collections.Generic;
using System;

namespace BigWorldGame.Controls
{
    public class Control
    {
        public Rectangle ClientRectangle= Rectangle.Empty;

        public Rectangle Padding { get; set; } = Rectangle.Empty;

        public List<Control> Children { get; private set; } = new List<Control>();

        public Color BackgroundColor { get; set; } = Color.Transparent;

        public Texture2D Pixel { get; private set; }

        public bool MouseHovered { get; private set; } = false;

        public bool MouseDown { get; private set; } = false;

        protected bool IsInvalid { get; private set; } = false;

        protected void Invalidate() => IsInvalid = true;

        public virtual void LoadContent(Game game)
        {
            Pixel = new Texture2D(game.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            foreach (var child in Children)
                child.LoadContent(game);
        }
        
        public virtual void Update()
        {
            foreach (var child in Children)
                child.Update();

            if (IsInvalid)
                IsInvalid = false;
        }


        public virtual void Draw(SpriteBatch batch, Rectangle destinationRectangle, float alpha)
        {
            batch.Draw(Pixel, destinationRectangle, BackgroundColor);

            foreach (var child in Children)
            {
                var childRect = new Rectangle(Padding.X + child.ClientRectangle.X, Padding.Y + child.ClientRectangle.Y, child.ClientRectangle.Height, child.ClientRectangle.Width);

                child.Draw(batch, childRect, alpha);
            }
        }

        internal virtual void InternalMouseEnter(Point mousePosition)
        {
            MouseHovered = true;
            OnMouseEnter(mousePosition);

            foreach (var child in Children)
            {
                var relativeMousePosition = new Point(mousePosition.X - child.ClientRectangle.X, mousePosition.Y - child.ClientRectangle.Y);

                if (relativeMousePosition.X > 0 && relativeMousePosition.Y > 0 && relativeMousePosition.X < child.ClientRectangle.Width && relativeMousePosition.Y < child.ClientRectangle.Height && !child.MouseHovered)
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

        internal virtual void InternalMouseMove(Point mousePosition)
        {
            foreach (var child in Children)
            {
                var relativeMousePosition = new Point(mousePosition.X - child.ClientRectangle.X, mousePosition.Y - child.ClientRectangle.Y);

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

        protected virtual void OnMouseLeave(Point mousePosition)
        {

        }

        protected virtual void OnMouseEnter(Point mousePosition)
        {

        }

        protected virtual void OnMouseUp(Point mousePosition) { }

        protected virtual void OnMouseDown(Point mousePosition) { }
    }
}