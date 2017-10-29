using BigWorld.GUI.Args;
using BigWorld.GUI.Brushes;
using BigWorld.GUI.Layout;
using engenious;
using engenious.Graphics;
using engenious.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI
{
    public class Control
    {
        public int? Width { get; set; }
        public int? Height { get; set; }

        public Border Padding { get; set; } = new Border(0);

        public Border Margin { get; set; } = new Border(0);

        public Point Position { get; set; } = Point.Zero;

        public Brush Background { get; set; } = new BorderBrush(Color.Transparent);

        public Brush HoveredBackground { get; set; } = null;

        public Brush PressedBackground { get; set; } = null;

        public Brush DisabledBrush { get; set; } = null;

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public VerticalAlignment VerticalAlignment { get; set; } 

        protected readonly List<Control> ChildCollection = new List<Control>();

        public Rectangle ClientRectangle { get; private set; }

        public Rectangle RenderedClientRectangle { get; private set; }

        public bool IsHovered { get; private set; }

        public bool IsMouseButtonDown { get; private set; }

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled == value)
                    return;
                enabled = value;
                OnEnabledChanged();
            }
        }

        private bool enabled = true;

        public virtual ControlSize GetActualSize(ControlSize controlSize)
        {
            var width = Width;
            var height = Height;

            if (!width.HasValue && !controlSize.Width.HasValue)
                width = null;
            else if (!width.HasValue && HorizontalAlignment == HorizontalAlignment.Stretch)
                width = controlSize.Width;

            if (!height.HasValue && !controlSize.Height.HasValue)
                height = null;
            else if (!height.HasValue && VerticalAlignment == VerticalAlignment.Stretch)
                height = controlSize.Height;

            return new ControlSize(width,height);
        }

        public RasterizerState RasterizerState { get; set; } = new RasterizerState()
        {
            ScissorTestEnable = true,
        };

        public void LoadContent(Game game)
        {
            foreach (var child in ChildCollection)
                child.LoadContent(game);

            OnLoadContent(game);
        }

        protected virtual void OnLoadContent(Game game)
        {

        }

        public void Draw(SpriteBatch batch, Matrix transform, Rectangle renderMask, ControlSize availableSize, GameTime gameTime)
        {

            //Calculate how big we really are
            var clientSize = GetActualSize(availableSize);

            //Add the absolute positioning to the transform
            transform *= Matrix.CreateTranslation(Position.X, Position.Y, 0);

            //Create the client-rectangle and transform it
            Rectangle clientRectangle = new Rectangle(0, 0, clientSize.Width ?? 0, clientSize.Height ?? 0);
            clientRectangle = clientRectangle.Transform(transform);

            //Calculate what the renderable area really is
            Rectangle renderRec = renderMask.Intersection(clientRectangle);
            
            ClientRectangle = clientRectangle;
            RenderedClientRectangle = renderRec;

            //Set the ScissorRectangle to the real renderable area
            batch.GraphicsDevice.ScissorRectangle = renderRec;

            batch.Begin(rasterizerState:RasterizerState,transformMatrix: transform);
            OnDraw(batch,clientSize, gameTime);
            batch.End();

            //Set the renderable area for the child
            //TODO: Add padding here
            var childRectangle = new Rectangle(clientRectangle.X + Padding.Left, clientRectangle.Y + Padding.Top, 
                clientRectangle.Width - Padding.Horizontal, clientRectangle.Height - Padding.Vertical);

            var childTransform = transform * Matrix.CreateTranslation(Padding.Left, Padding.Top, 0);

            OnDrawChildren(batch, childTransform, renderRec, new ControlSize(childRectangle.Size), gameTime);

            OnAfterDraw(batch, renderMask.Size, gameTime);
        }

        protected virtual void OnDrawBackground(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            if (IsMouseButtonDown && PressedBackground != null && Enabled)
                PressedBackground.Draw(batch, clientSize);
            else if (IsHovered && HoveredBackground != null && Enabled)
                HoveredBackground.Draw(batch, clientSize);
            else
                Background.Draw(batch, clientSize);
        }

        protected virtual void OnDrawChildren(SpriteBatch batch, Matrix transform, Rectangle renderMask, ControlSize availableSize, GameTime gameTime)
        {
            foreach(var child in ChildCollection)
            {
                child.Draw(batch, transform, renderMask, availableSize, gameTime);
            }
        }

        protected virtual void OnDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            OnDrawBackground(batch, clientSize, gameTime);
        }

        protected virtual void OnAfterDraw(SpriteBatch batch, Size clientSize, GameTime gameTime)
        {
            if (!Enabled && DisabledBrush != null)
                DisabledBrush.Draw(batch, clientSize);
        }

        protected virtual void OnEnabledChanged() { }

        public void Update(GameTime gameTime)
        {
            foreach (var child in ChildCollection)
                child.Update(gameTime);

            OnUpdate(gameTime);
        }

        protected virtual void OnUpdate(GameTime gameTime)
        {

        }

        #region Mouse-Events

        public void MouseButtonUp(MouseEventArgs eventArgs, MouseButton mouseButton)
        {
            IsMouseButtonDown = false;

            foreach(var child in ChildCollection)
            {
                if (child.IsMouseButtonDown)
                    child.MouseButtonUp(eventArgs, mouseButton);
            }

            OnMouseButtonUp(eventArgs, eventArgs.GetRelativePosition(RenderedClientRectangle), mouseButton);
        }

        public bool MouseButtonDown(MouseEventArgs mouseEventArgs, MouseButton mouseButton)
        {
            IsMouseButtonDown = true;

            var handled = false;

            foreach(var child in ChildCollection)
            {
                if (child.RenderedClientRectangle.Intersects(mouseEventArgs.Position))
                    handled = child.MouseButtonDown(mouseEventArgs, mouseButton);

                if (handled)
                    break;
            }

            if (!handled)
                handled = OnMouseButtonDown(mouseEventArgs, mouseEventArgs.GetRelativePosition(RenderedClientRectangle), mouseButton);

            return handled;
        }

        public void MouseMove(MouseEventArgs mouseEventArgs)
        {
            foreach(var child in ChildCollection)
            {
                if (child.RenderedClientRectangle.Intersects(mouseEventArgs.Position))
                {
                    child.MouseMove(mouseEventArgs);

                    if (!child.IsHovered)
                        child.MouseEnter(mouseEventArgs);
                }
                else if(child.IsHovered)
                {
                    child.MouseLeave(mouseEventArgs);
                }
            }

            OnMouseMove(mouseEventArgs, mouseEventArgs.GetRelativePosition(RenderedClientRectangle));
        }

        public bool MouseScroll(MouseEventArgs mouseEventArgs, int delta)
        {
            var handled = false;

            foreach (var child in ChildCollection)
            {
                if (child.RenderedClientRectangle.Intersects(mouseEventArgs.Position))
                    handled = child.MouseScroll(mouseEventArgs, delta);

                if (handled)
                    break;
            }

            if (!handled)
                handled = OnMouseScroll(mouseEventArgs, delta);

            return handled;
        }

        public void MouseEnter(MouseEventArgs eventArgs)
        {
            IsHovered = true;
            OnMouseEnter(eventArgs, eventArgs.GetRelativePosition(RenderedClientRectangle));
        }

        public void MouseLeave(MouseEventArgs eventArgs)
        {
            IsHovered = false;

            foreach(var child in ChildCollection)
            {
                if (child.IsHovered)
                    child.MouseLeave(eventArgs);
            }

            OnMouseLeave(eventArgs, eventArgs.GetRelativePosition(RenderedClientRectangle));
        }

        protected virtual void OnMouseMove(MouseEventArgs mouseEventArgs, Point relativePosition)
        { }

        protected virtual void OnMouseEnter(MouseEventArgs mouseEventArgs, Point relativePosition)
        {  
        }

        protected virtual void OnMouseLeave(MouseEventArgs mouseEventArgs, Point relativePosition)
        {
        }

        protected virtual bool OnMouseButtonDown(MouseEventArgs mouseEventArgs, Point relativePosition, MouseButton button)
        {
            return false;
        }

        protected virtual bool OnMouseButtonUp(MouseEventArgs mouseEventArgs, Point relativePosition, MouseButton button)
        {
            return false;
        }

        protected virtual bool OnMouseScroll(MouseEventArgs mouseEventArgs, int scrollDelta)
        {
            return false;
        }

        #endregion

        #region Keyboard-Events

        public void OnKeyDown() { }

        #endregion
    }
}
