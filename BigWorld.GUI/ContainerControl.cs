using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace BigWorld.GUI
{
    public class ContainerControl : Control
    {
        public List<Control> Children => ChildCollection;

    }
}
