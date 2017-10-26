using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Input;

namespace BigWorld.GUI.Controls
{
    public class RootControl : Control
    {
        private MouseState lastMouseState;

        public override void Update()
        {
            base.Update();

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
