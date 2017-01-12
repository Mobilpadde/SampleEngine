using Engine.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Keys
{
    internal class Up : Key
    {
        public Up()
        {
            this.Direction = Move.Direction.North;
        }
    }
}
