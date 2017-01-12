using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    internal class Opening
    {
        internal protected int Id;
        internal protected double X;
        internal protected double Y;

        internal Opening(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}
