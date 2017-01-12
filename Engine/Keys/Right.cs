using Engine.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Keys
{
    internal class Right : Key
    {
        public Right()
        {
            this.Direction = Move.Direction.East;
        }
    }
}
