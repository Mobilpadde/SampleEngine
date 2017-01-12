using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Keys
{
    internal abstract class Key : EventArgs
    {
        internal bool IsDown { get; set; }
        internal Move.Direction Direction { get; set; }
    }
}
