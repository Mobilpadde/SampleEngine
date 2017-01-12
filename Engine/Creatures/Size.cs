using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Creatures
{
    internal sealed class Size
    {
        internal double Width { get; private set; }
        internal double Height { get; private set; }

        internal Size(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
