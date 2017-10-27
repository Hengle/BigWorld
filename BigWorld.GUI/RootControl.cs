using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
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
            ClientRectangle = game.GraphicsDevice.Viewport.Bounds;
            ActualClientRectangle = game.GraphicsDevice.Viewport.Bounds;
            RenderedClientRectangle = game.GraphicsDevice.Viewport.Bounds;
            Invalidate();
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

                lastMouseState = mouseState;
            }
        }
    }
}
