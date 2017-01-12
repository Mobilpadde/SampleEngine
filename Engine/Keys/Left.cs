using Engine.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Keys
{
    internal class Left : Key
    {
        public Left()
        {
            this.Direction = Move.Direction.West;
        }
    }
}
