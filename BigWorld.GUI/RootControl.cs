using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;
using engenious.Input;

namespace BigWorld.GUI
{
    public class RootControl : Control
    {
        private MouseState lastMouseState;

        public RootControl()
        {
            
        }

        public override void LoadContent(Game game)
        {
            base.LoadContent(game);
            Height = game.GraphicsDevice.Viewport.Bounds.Height;
            Width = game.GraphicsDevice.Viewport.Bounds.Width;
            ActualClientRectangle = game.GraphicsDevice.Viewport.Bounds;
            RenderedClientRectangle = game.GraphicsDevice.Viewport.Bounds;
            Invalidate();
        }

        public override void PreDraw(SpriteBatch batch, Rectangle parentArea, float alpha)
        {
            batch.GraphicsDevice.ScissorRectangle = RenderedClientRectangle;
        }

        protected override void OnUpdate(GameTime time)
        {
            base.OnUpdate(time);

            var mouseState = Mouse.GetState();
            var mousePoint = new engenious.Point(mouseState.X, mouseState.Y);
            if (mouseState != lastMouseState)
            {
                InternalMouseMove(mousePoint);

                if (mouseState.IsAnyButtonDown == true && lastMouseState.IsAnyButtonDown == false)
                    InternalMouseDown(mousePoint);
                else if (mouseState.IsAnyButtonDown == false && lastMouseState.IsAnyButtonDown == true)
                    InternalMouseUp(mousePoint);

                if (mouseState.ScrollWheelValue != lastMouseState.ScrollWheelValue)
                    InternalMouseWheel(mouseState.ScrollWheelValue, mouseState.ScrollWheelValue-lastMouseState.ScrollWheelValue);

                lastMouseState = mouseState;
            }
        }
    }
}
