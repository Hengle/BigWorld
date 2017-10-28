using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Input;
using BigWorld.GUI.Args;

namespace BigWorld.GUI
{
    public class RootControl : ContentControl
    {
        private MouseState lastMouseState;

        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);

            var mouseState = Mouse.GetState();

            if(mouseState != lastMouseState)
            {
                var eventArgs = new MouseEventArgs(mouseState);

                if (mouseState.X != lastMouseState.X || mouseState.Y != lastMouseState.Y)
                    MouseMove(eventArgs);

                foreach(MouseButton button in Enum.GetValues(typeof(MouseButton)))
                {
                    if (mouseState.IsButtonDown(button) && lastMouseState.IsButtonUp(button))
                        MouseButtonDown(eventArgs, button);

                    if (mouseState.IsButtonUp(button) && lastMouseState.IsButtonDown(button))
                        MouseButtonUp(eventArgs, button);
                }

                if (lastMouseState.ScrollWheelValue != mouseState.ScrollWheelValue)
                    MouseScroll(eventArgs,  mouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue);

                lastMouseState = mouseState;

            }
        }
    }
}
