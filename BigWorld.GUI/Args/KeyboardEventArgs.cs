using engenious.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI.Args
{
    public class KeyboardEventArgs
    {
        public KeyboardState KeyboardState { get; private set; }

        public KeyboardEventArgs(KeyboardState state)
        {
            KeyboardState = state;
        }
    }
}
